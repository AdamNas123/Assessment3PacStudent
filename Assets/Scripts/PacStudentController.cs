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

    private Animator pacStudentAnimator;
    [SerializeField]

    private AudioSource movingNoPellet;
    [SerializeField]
    private AudioSource movingWithPellet;
    [SerializeField]
    private AudioSource wallCollision;
    private bool wallAudioPlayed = false;
    [SerializeField]
    private ParticleSystem dustParticleSystem;

    private string lastInput;
    private string currentInput;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;
    private int currentScore = 0;

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

        pacStudentAnimator = GetComponent<Animator>();

        pacStudentAnimator.SetFloat("Moving", 0.0f);

        //scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        //movingNoPellet.Play();
    }

    // Update is called once per frame
    void Update()
    {
        /*float leftPos = gameObject.transform.position.x - 1;
        float rightPos = gameObject.transform.position.x + 1;
        float downPos = gameObject.transform.position.y - 1;
        float upPos = gameObject.transform.position.y + 1; */

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
            EatPellet();
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
        //Destroy(tileMap.GetTile(tileMap.WorldToCell(gameObject.transform.position)));
        currentScore += 10;
        fullLevelMap[yPos, xPos] = 0;
        scoreText.text = currentScore.ToString();
    }
}
