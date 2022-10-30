using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PacStudentController : MonoBehaviour
{
    //private Tween activeTween;
    [SerializeField]
    private Tweener tweener;

    private PlayAudio backgroundMusic;

    private Animator pacStudentAnimator;

    public Animator ghost1Animator;
    public Animator ghost2Animator;
    public Animator ghost3Animator;
    public Animator ghost4Animator;

    public GameObject powerPellet1;
    public GameObject powerPellet2;
    public GameObject powerPellet3;
    public GameObject powerPellet4;

    [SerializeField]
    private AudioSource movingNoPellet;
    [SerializeField]
    private AudioSource movingWithPellet;
    [SerializeField]
    private AudioSource wallCollision;
    [SerializeField]
    private AudioSource deathSoundEffect;
    private bool wallAudioPlayed = false;
    [SerializeField]
    private ParticleSystem dustParticleSystem;

    private string lastInput;
    private string currentInput;

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;
    private int currentScore = 0;

    private float gameTimer = 0.0f;
    public TMPro.TextMeshProUGUI gameTimerText;

    private float ghostScaredTimer = 0.0f;
    private bool isScaredTimerRunning = false;
    public Image ghostScaredTimerUI;
    public TMPro.TextMeshProUGUI ghostScaredTimerText;

    public List<Image> lives = new List<Image>();

    private List<int> walls = new List<int>{ 1, 2, 3, 4, 7 };

    int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };
    private int[,] fullLevelMap;

    [SerializeField]
    private Tilemap tileMap; 

    //private bool[,] pacPosition = new bool[15, 14];
    private int xPos = 1;
    private int yPos = 1;

    private Vector3 originalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        //Extend level map to full map
        fullLevelMap = new int[2 * levelMap.GetLength(0) - 1, 2 * levelMap.GetLength(1)];

        for (int i = 0; i < fullLevelMap.GetLength(0); ++i)
        {
            for (int j = 0; j < fullLevelMap.GetLength(1); ++j)
            {
                if (i < levelMap.GetLength(0) && j < levelMap.GetLength(1))
                {
                    fullLevelMap[i, j] = levelMap[i, j];
                }

                if (i < levelMap.GetLength(0) && j >= levelMap.GetLength(1))
                {
                    fullLevelMap[i, j] = levelMap[i, fullLevelMap.GetLength(1) - j - 1];
                }

                if (i >= levelMap.GetLength(0) && j < levelMap.GetLength(1))
                {
                    fullLevelMap[i, j] = levelMap[fullLevelMap.GetLength(0) - i - 1, j];
                }

                if (i >= levelMap.GetLength(0) && j >= levelMap.GetLength(1))
                {
                    fullLevelMap[i, j] = levelMap[fullLevelMap.GetLength(0) - i - 1, fullLevelMap.GetLength(1) - j - 1];
                }
            }
        }
        originalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        pacStudentAnimator = GetComponent<Animator>();
        pacStudentAnimator.SetFloat("Moving", 0.0f);

        backgroundMusic = GameObject.FindGameObjectWithTag("music").GetComponent<PlayAudio>();

    }

    // Update is called once per frame
    void Update()
    {
            gameTimer += Time.deltaTime;
            gameTimerText.text = FormatTime(gameTimer);
            ghostScaredTimerUI.enabled = false;
            if (isScaredTimerRunning)
            {
                ghostScaredTimerUI.enabled = true;
                if (ghostScaredTimer < 3.1)
                {
                    ghost1Animator.Play("GhostRecoveringAnim");
                    ghost2Animator.Play("GhostRecoveringAnim");
                    ghost3Animator.Play("GhostRecoveringAnim");
                    ghost4Animator.Play("GhostRecoveringAnim");
                }
                if (ghostScaredTimer > 0)
                {
                    ghostScaredTimerText.text = ghostScaredTimer.ToString("0");
                }
                else
                {
                    ghostScaredTimer = 0;
                    isScaredTimerRunning = false;
                    ghostScaredTimerUI.gameObject.SetActive(false);

                    //Set animators back to walking states - Update for 90% section
                    ghost1Animator.Play("Ghost1UpAnim");
                    ghost2Animator.Play("Ghost2UpAnim");
                    ghost3Animator.Play("Ghost3UpAnim");
                    ghost4Animator.Play("Ghost4UpAnim");

                    backgroundMusic.scaredStateSource.Stop();
                    backgroundMusic.normalStateSource.Play();
                }
            }
            if (Input.GetKeyDown("a"))
            {
                lastInput = "a";
            }
            if (Input.GetKeyDown("d"))
            {
                lastInput = "d";
            }
            if (Input.GetKeyDown("s"))
            {
                lastInput = "s";
            }
            if (Input.GetKeyDown("w"))
            {
                lastInput = "w";
            }
            if (!tweener.TweenExists(gameObject.transform))
            {
                if (lastInput == "a")
                {
                    if (isWalkable(yPos, xPos - 1))
                    {

                        currentInput = lastInput;
                        Move("a");
                    }
                    else
                    {
                        checkCurrentInput(currentInput);
                    }
                }
                else if (lastInput == "d")
                {
                    if (isWalkable(yPos, xPos + 1))
                    {
                        currentInput = lastInput;
                        Move("d");
                    }
                    else
                    {
                        checkCurrentInput(currentInput);
                    }
                }
                else if (lastInput == "s")
                {
                    if (isWalkable(yPos + 1, xPos))
                    {
                        currentInput = lastInput;
                        Move("s");
                    }
                    else
                    {
                        checkCurrentInput(currentInput);
                    }
                }
                else if (lastInput == "w")
                {
                    if (isWalkable(yPos - 1, xPos))
                    {
                        currentInput = lastInput;
                        Move("w");
                    }
                    else
                    {
                        checkCurrentInput(currentInput);
                    }
                }
            }
    }
    private bool isWalkable(int yPos, int xPos)
    {
        if ((xPos < 0 && yPos == 14) || (xPos == fullLevelMap.GetLength(1) && yPos == 14))
        {
            return true;
        }
        if (xPos < 0 || xPos == fullLevelMap.GetLength(1) || yPos < 0 || yPos == fullLevelMap.GetLength(0) || yPos == 12 && (xPos == 13 || xPos == 14) || yPos == 16 && (xPos == 13 || xPos == 14) || walls.Contains(fullLevelMap[yPos, xPos]))
        {
            return false;
        }
        else 
        {
            return true;
        }
    }

    private void checkCurrentInput(string currentInput)
    {
        if (currentInput == "a" && isWalkable(yPos, xPos - 1))
        {
            Move("a");
        }
        else if (currentInput == "d" && isWalkable(yPos, xPos + 1))
        {
            Move("d");
        }
        else if(currentInput == "s" && isWalkable(yPos + 1, xPos))
        {
            Move("s");
        }
        else if(currentInput == "w" && isWalkable(yPos - 1, xPos))
        {
            Move("w");
        }
        else
        {
            pacStudentAnimator.SetFloat("Moving", 0.0f);
            if (!wallAudioPlayed)
            {
                wallCollision.Play();
                wallAudioPlayed = true;
            }
            
        }
    }

    private void Move(string direction)
    {
       pacStudentAnimator.SetFloat("Moving", 1.0f);
       if (direction == "a")
       {
            if (xPos == 0 && yPos == 14)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + fullLevelMap.GetLength(1) - 1, gameObject.transform.position.y, 0.0f);
                xPos = fullLevelMap.GetLength(1) - 1;
            }
            else
            {
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0.0f), 0.5f);
                xPos -= 1;
                pacStudentAnimator.Play("PacStudentLeftAnim");
            }
       }
       if (direction == "d")
       {
            if (xPos == fullLevelMap.GetLength(1) - 1 && yPos == 14)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - fullLevelMap.GetLength(1) + 1, gameObject.transform.position.y, 0.0f);
                xPos = 0;
            }
            else
            {
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, 0.0f), 0.5f);
                xPos += 1;
                pacStudentAnimator.Play("PacStudentRightAnim");
            }
       }
       if (direction == "s")
       {
            tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, 0.0f), 0.5f);
            yPos += 1;
            pacStudentAnimator.Play("PacStudentDownAnim");
       }
       if (direction == "w")
       {
            tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0.0f), 0.5f);
            yPos -= 1;
            pacStudentAnimator.Play("PacStudentUpAnim");
       }

        PlayMovingAudio();
        dustParticleSystem.Play();
        wallAudioPlayed = false;
        if (fullLevelMap[yPos, xPos] == 5)
        {
            Invoke("EatPellet", 0.5f);
            fullLevelMap[yPos, xPos] = 0;
        }
    }

    private void PlayMovingAudio()
    {
        if (fullLevelMap[yPos, xPos] == 5 || fullLevelMap[yPos, xPos] == 6)
        {
            movingWithPellet.PlayDelayed(0.16f);
        }
        else if (fullLevelMap[yPos, xPos] == 0)
        {
           movingNoPellet.PlayDelayed(0.16f);
        }
    }

    private void EatPellet()
    {
        tileMap.SetTile(tileMap.WorldToCell(gameObject.transform.position), null);
        currentScore += 10;   
        scoreText.text = currentScore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("cherry"))
        {
            Destroy(GameObject.FindGameObjectWithTag("cherry"));
            currentScore += 100;
            scoreText.text = currentScore.ToString();
        }

        if (collision.gameObject.CompareTag("power"))
        {
            Destroy(collision.gameObject);
            fullLevelMap[yPos, xPos] = 0;

            //Change ghost animator
            ghost1Animator.Play("GhostScaredAnim");
            ghost2Animator.Play("GhostScaredAnim");
            ghost3Animator.Play("GhostScaredAnim");
            ghost4Animator.Play("GhostScaredAnim");

            //Change background music
            backgroundMusic.normalStateSource.Stop();
            backgroundMusic.scaredStateSource.Play();
            //Start timer?
            isScaredTimerRunning = true;
            ghostScaredTimer = 10.0f;
            ghostScaredTimerUI.gameObject.SetActive(true);
            ghostScaredTimerText.text = ghostScaredTimer.ToString("0");
        }

        if (collision.gameObject.CompareTag("ghost1"))
        {
            if (ghost1Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost1UpAnim") || ghost1Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost1DownAnim") || ghost1Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost1LeftAnim") || ghost1Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost1RightAnim"))
            {
                KillPacstudent();
            }
            else if (ghost1Animator.GetCurrentAnimatorStateInfo(0).IsName("GhostScaredAnim") || ghost1Animator.GetCurrentAnimatorStateInfo(0).IsName("GhostRecoveringAnim"))
            {
                ghost1Animator.Play("GhostDeadAnim");
                backgroundMusic.deadStateSource.Play();
                currentScore += 300;
                scoreText.text = currentScore.ToString();
                //Set ghost back to walking after 5 seconds
            }
        }
        if (collision.gameObject.CompareTag("ghost2"))
        {
            if (ghost2Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost2UpAnim") || ghost2Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost2DownAnim") || ghost2Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost2LeftAnim") || ghost2Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost2RightAnim"))
            {
                KillPacstudent();
            }
            else if (ghost2Animator.GetCurrentAnimatorStateInfo(0).IsName("GhostScaredAnim") || ghost2Animator.GetCurrentAnimatorStateInfo(0).IsName("GhostRecoveringAnim"))
            {
                ghost2Animator.Play("GhostDeadAnim");
                backgroundMusic.deadStateSource.Play();
                currentScore += 300;
                scoreText.text = currentScore.ToString();
                //Set ghost back to walking after 5 seconds
            }
        }
        if (collision.gameObject.CompareTag("ghost3"))
        {
            if (ghost3Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost3UpAnim") || ghost3Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost3DownAnim") || ghost3Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost3LeftAnim") || ghost3Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost3RightAnim"))
            {
                KillPacstudent();
            }
            else if (ghost3Animator.GetCurrentAnimatorStateInfo(0).IsName("GhostScaredAnim") || ghost3Animator.GetCurrentAnimatorStateInfo(0).IsName("GhostRecoveringAnim"))
            {
                ghost3Animator.Play("GhostDeadAnim");
                backgroundMusic.deadStateSource.Play();
                currentScore += 300;
                scoreText.text = currentScore.ToString();
                //Set ghost back to walking after 5 seconds
            }
        }
        if (collision.gameObject.CompareTag("ghost4"))
        {
            if (ghost4Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost4UpAnim") || ghost4Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost4DownAnim") || ghost4Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost4LeftAnim") || ghost4Animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost4RightAnim"))
            {
                KillPacstudent();
            }
            else if (ghost4Animator.GetCurrentAnimatorStateInfo(0).IsName("GhostScaredAnim") || ghost4Animator.GetCurrentAnimatorStateInfo(0).IsName("GhostRecoveringAnim"))
            {
                ghost4Animator.Play("GhostDeadAnim");
                backgroundMusic.deadStateSource.Play();
                currentScore += 300;
                scoreText.text = currentScore.ToString();
                //Set ghost back to walking after 5 seconds
            }
        } 
    }
    private void KillPacstudent()
    {
        //transform.position = originalPos;
        Physics2D.SyncTransforms();
        lastInput = null;
        currentInput = null;
        transform.position = new Vector3(-16.5f, -1.5f, 0);
        if (lives.Count > 0)
        {
            Destroy(lives[lives.Count - 1]);
            lives.RemoveAt(lives.Count - 1);
        }
        
        //Play Particle Death effect
        xPos = 1;
        yPos = 1;
        deathSoundEffect.Play();
        pacStudentAnimator.SetFloat("Moving", 0.0f);
    }

    private string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 100);
        string timeText = String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        return timeText;
    }
}
