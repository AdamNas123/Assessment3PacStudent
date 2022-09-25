using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private GameObject tilemap;
    private GameObject powerPellet1;
    private GameObject powerPellet2;
    private GameObject powerPellet3;
    private GameObject powerPellet4;
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


    public GameObject[] mapParts = new GameObject[8];
    private Vector3 startPos = new Vector3(-17.5f, -0.5f, 0.0f);
    private float rotation;
    private float xScale;
    private float yScale;

    private int[,] fullLevelMap;
    private float[,] mapRotation;
    // Start is called before the first frame update
    void Start()
    {
        //Delete manual map
        tilemap = GameObject.Find("Grid");
        
        powerPellet1 = GameObject.Find("Power Pellet (1)");
        powerPellet2 = GameObject.Find("Power Pellet (2)");
        powerPellet3 = GameObject.Find("Power Pellet (3)");
        powerPellet4 = GameObject.Find("Power Pellet (4)");

        Destroy(tilemap);
        Destroy(powerPellet1);
        Destroy(powerPellet2);
        Destroy(powerPellet3);
        Destroy(powerPellet4);

        //Extend given level map to get whole map
        fullLevelMap = new int[2 * levelMap.GetLength(0) - 1, 2 *  levelMap.GetLength(1)];

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

        //Create Rotation Array to store piece rotations
        mapRotation = new float[fullLevelMap.GetLength(0), fullLevelMap.GetLength(1)];
        
        //Create procedural map
                
        //Loop through row by row, and column by column
        for (int i = 0; i < fullLevelMap.GetLength(0); ++i)
        {
            for (int j = 0; j < fullLevelMap.GetLength(1); ++j)
            {
                //Outside Wall Piece
                if (fullLevelMap[i,j].Equals(2))
                {
                    if (((i - 1) < 0 || fullLevelMap[i - 1, j].Equals(0)) && (fullLevelMap[i+1,j].Equals(0) || fullLevelMap[i + 1, j].Equals(5) || fullLevelMap[i + 1, j].Equals(6)))
                    {
                        rotation = 270;
                    }
                    if ((j - 1) < 0 && (fullLevelMap[i, j + 1].Equals(0) || fullLevelMap[i, j + 1].Equals(5) || fullLevelMap[i, j + 1].Equals(6)))
                    {
                        rotation = 0;
                    }
                    //if (((i + 1) == fullLevelMap.GetLength(0) || fullLevelMap[i + 1, j].Equals(0)) && (fullLevelMap[i - 1, j].Equals(0) || fullLevelMap[i - 1, j].Equals(5) || fullLevelMap[i - 1, j].Equals(6)))
                    if ((i + 1) == fullLevelMap.GetLength(0) || (i - 1) >= 0 && (fullLevelMap[i - 1, j].Equals(5) || fullLevelMap[i - 1, j].Equals(6)))
                    {
                        rotation = 90;
                    } 
                    if((i + 2) < fullLevelMap.GetLength(0) && fullLevelMap[i + 1, j].Equals(0) && fullLevelMap[i + 2, j].Equals(0))
                    {
                        rotation = 90;
                    }
                    if ((j + 1) == fullLevelMap.GetLength(1) && (fullLevelMap[i , j - 1].Equals(0) || fullLevelMap[i, j - 1].Equals(5) || fullLevelMap[i, j - 1].Equals(6)))
                    {
                        rotation = 180;
                    }
                }

                //Inside Corner Piece
                if (fullLevelMap[i, j].Equals(3))
                {
                    //Checks if there is a correctly rotated wall or corner on top
                    if ((fullLevelMap[i-1,j].Equals(4) && mapRotation[i-1,j].Equals(0)) || (fullLevelMap[i - 1, j].Equals(3) && (mapRotation[i - 1, j].Equals(180) || mapRotation[i - 1, j].Equals(270))))
                    {
                        //Checks if there is a correctly rotated wall or corner to the left
                        if ((fullLevelMap[i, j - 1].Equals(4) && mapRotation[i, j - 1].Equals(90)) || (fullLevelMap[i, j - 1].Equals(3) && (mapRotation[i, j - 1].Equals(0) || mapRotation[i - 1, j].Equals(270)))) 
                        {
                            rotation = 90;
                        }

                        //Checks if there is a wall or corner to the right
                        else if ((fullLevelMap[i, j + 1].Equals(4)) || (fullLevelMap[i, j + 1].Equals(3)))
                        {
                            rotation = 0;
                        }
                    }

                    //Checks if there is a wall or corner on the bottom
                    else if ((fullLevelMap[i + 1, j].Equals(4)) || (fullLevelMap[i + 1, j].Equals(3)))
                    {
                        //Checks if there is a correctly rotated wall or corner to the left
                        if ((fullLevelMap[i, j - 1].Equals(4) && mapRotation[i, j - 1].Equals(90)) || (fullLevelMap[i, j - 1].Equals(3) && (mapRotation[i, j - 1].Equals(0) || mapRotation[i, j - 1].Equals(270))))
                        {
                            rotation = 180;
                        }

                        //Checks if there is a wall or corner to the right
                        else if ((fullLevelMap[i, j + 1].Equals(4)) || (fullLevelMap[i, j + 1].Equals(3)))
                        {
                            rotation = 270;
                        }
                    }
                }

                //Inside Wall Piece
                if (fullLevelMap[i, j].Equals(4))
                {
                    if ((fullLevelMap[i - 1, j].Equals(4) || fullLevelMap[i - 1, j].Equals(3) || fullLevelMap[i - 1, j].Equals(7)) && (fullLevelMap[i + 1, j].Equals(4) || fullLevelMap[i + 1, j].Equals(3) || fullLevelMap[i + 1, j].Equals(7)) && (!fullLevelMap[i, j - 1].Equals(4) || !fullLevelMap[i, j + 1].Equals(3)))
                    {
                        rotation = 0;
                    }
                    else
                    {
                        rotation = 90;
                    }
                }

                //Standard Pellet Piece
                if (fullLevelMap[i, j].Equals(5) || fullLevelMap[i, j].Equals(6))
                {
                    if (i < levelMap.GetLength(0)) 
                    {
                        yScale = 1;
                        if (j < levelMap.GetLength(1))
                        {
                            xScale = 1;
                        }
                        else
                        {
                            xScale = -1;
                        }
                    }
                    else
                    {
                        yScale = -1;
                        if (j >= levelMap.GetLength(1))
                        {
                            xScale = -1;
                        }
                        else
                        {
                            xScale = 1;
                        }
                    }
                }

                //T Juncture Piece
                if (fullLevelMap[i, j].Equals(7))
                {
                    //Checks if there is an outside wall or t juncture at the top and bottom
                    //if (((i > 0 && fullLevelMap[i - 1, j].Equals(2) || fullLevelMap[i - 1, j].Equals(7))) && (i < fullLevelMap.GetLength(0) - 1 && (fullLevelMap[i + 1, j].Equals(2) || fullLevelMap[i + 1, j].Equals(7))))
                    if (i < fullLevelMap.GetLength(0) - 1 && (fullLevelMap[i + 1, j].Equals(2) || fullLevelMap[i + 1, j].Equals(7)))
                    {

                        //Checks if there is an inside wall to the left
                        if (fullLevelMap[i, j - 1].Equals(4) && mapRotation[i, j - 1].Equals(90))
                        {
                            rotation = 270;
                        }
                        else
                        {
                            rotation = 90;
                        }   
                    }
                    //Checks if there is an outside wall or t juncture to the left and right
                    else //if ((fullLevelMap[i, j - 1].Equals(2) || fullLevelMap[i, j - 1].Equals(7)) && (fullLevelMap[i, j + 1].Equals(2) || fullLevelMap[i, j + 1].Equals(7)))
                    {
                        if (i < fullLevelMap.GetLength(0) - 1 && fullLevelMap[i + 1, j].Equals(4) && mapRotation[i + 1, j].Equals(0))
                        {
                            rotation = 0;
                        }
                        else
                        {
                            rotation = 180;
                        }
                    }
                }
                GameObject piece = Instantiate(mapParts[fullLevelMap[i,j]], startPos + new Vector3(j, -i, 0.0f), Quaternion.Euler(0.0f, 0.0f, rotation));
                if (fullLevelMap[i, j].Equals(5) || fullLevelMap[i, j].Equals(6))
                {
                    piece.transform.localScale = new Vector3(piece.transform.localScale.x * xScale, piece.transform.localScale.y * yScale, 0.0f);

                }
                mapRotation[i, j] = rotation;
                rotation = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
