using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    //private Tween activeTween;
    [SerializeField]
    private Tweener tweener;

    private Animator pacStudentAnimator;
    private AudioSource movingNoPellet;
    private AudioSource movingWithPellet;

    private string lastInput;
    private string currentInput;

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

    private bool[,] pacPosition = new bool[15, 14];
    private int xPos = 1;
    private int yPos = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        pacPosition[xPos, yPos] = true;
        pacStudentAnimator = GetComponent<Animator>();
        movingNoPellet = GetComponent<AudioSource>();
        //activeTween = new Tween(gameObject.transform, new Vector3(-16.5f, -1.5f, 0.0f), new Vector3(-11.5f, -1.5f, 0.0f), Time.time, 1.5f);
        //movingNoPellet.loop = true;
        //movingNoPellet.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float leftPos = gameObject.transform.position.x - 1;
        float rightPos = gameObject.transform.position.x + 1;
        float downPos = gameObject.transform.position.y - 1;
        float upPos = gameObject.transform.position.y + 1;

        if (Input.GetKeyDown("a") || (lastInput == "a" && !tweener.TweenExists(gameObject.transform)))
        {
            if (isWalkable(yPos, xPos - 1))
            {
                currentInput = lastInput;
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0.0f), 0.5f);
                lastInput = "a";
                pacPosition[xPos, yPos] = false;
                xPos -= 1;
                pacPosition[xPos, yPos] = true;
            }
            else
            {
                
            }
        }

        if (Input.GetKeyDown("d") || (lastInput == "d" && !tweener.TweenExists(gameObject.transform)))
        {
            if (isWalkable(yPos, xPos + 1))
            {
                currentInput = lastInput;
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, 0.0f), 0.5f);
                lastInput = "d";
                pacPosition[xPos, yPos] = false;
                xPos += 1;
                pacPosition[xPos, yPos] = true;
            }
            else {

            }
        }

        if (Input.GetKeyDown("s") || (lastInput == "s" && !tweener.TweenExists(gameObject.transform)))
        {
            if (isWalkable(yPos + 1, xPos))
            {
                currentInput = lastInput;
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, 0.0f), 0.5f);
                lastInput = "s";
                pacPosition[xPos, yPos] = false;
                yPos += 1;
                pacPosition[xPos, yPos] = true;
            }
        }

        if (Input.GetKeyDown("w") || (lastInput == "w" && !tweener.TweenExists(gameObject.transform)))
        {
            if (isWalkable(yPos - 1, xPos))
            {
                currentInput = lastInput;
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0.0f), 0.5f);
                lastInput = "w";
                pacPosition[xPos, yPos] = false;
                yPos -= 1;
                pacPosition[xPos, yPos] = true;
            }
        }

        if (!tweener.TweenExists(gameObject.transform))
        {
            //Check if walkable    
        }
    }

    private bool isWalkable(int yPos, int xPos)
    {
        if (xPos < 0 || xPos == levelMap.GetLength(1) || yPos < 0 || yPos == levelMap.GetLength(0) || walls.Contains(levelMap[yPos, xPos]))
        {
            return false;
        }
        else 
        {
            return true;
        }
    }
}
