using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private GameObject tilemap;
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
    // Start is called before the first frame update
    void Start()
    {
        //Delete manual map
        tilemap = GameObject.Find("Grid");
        Destroy(tilemap);

        //Extend given level map to get whole map
        int[,] fullLevelMap = new int[2 * levelMap.GetLength(0) - 1, 2 *  levelMap.GetLength(1)];

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
        
        
        //Create procedural map
                
        //Loop through row by row, and column by column
        for (int i = 0; i < fullLevelMap.GetLength(0); ++i)
        {
            for (int j = 0; j < fullLevelMap.GetLength(1); ++j)
            {
                if (fullLevelMap[i,j].Equals(2))
                {
                    if (((i-1) < 0 || fullLevelMap[i - 1, j].Equals(0)) && (fullLevelMap[i+1,j].Equals(0) || fullLevelMap[i + 1, j].Equals(5) || fullLevelMap[i + 1, j].Equals(6)))
                    {
                        rotation = 270;
                    }
                    if ((j - 1) < 0 && (fullLevelMap[i, j + 1].Equals(0) || fullLevelMap[i, j + 1].Equals(5) || fullLevelMap[i, j + 1].Equals(6)))
                    {
                        rotation = 0;
                    }
                    if (((i + 1) == fullLevelMap.GetLength(0) || fullLevelMap[i + 1, j].Equals(0)) && (fullLevelMap[i - 1, j].Equals(0) || fullLevelMap[i - 1, j].Equals(5) || fullLevelMap[i - 1, j].Equals(6)))
                    {
                        rotation = 90;
                    }
                    if ((j + 1) == fullLevelMap.GetLength(1) && (fullLevelMap[i , j - 1].Equals(0) || fullLevelMap[i, j - 1].Equals(5) || fullLevelMap[i, j - 1].Equals(6)))
                    {
                        rotation = 180;
                    }
                }
                if (fullLevelMap[i, j].Equals(3))
                {
                    if (fullLevelMap[i-1,j].Equals(4) || fullLevelMap[i - 1, j].Equals(3))
                    {
                        if (fullLevelMap[i, j - 1].Equals(4) || fullLevelMap[i, j - 1].Equals(3)) //And rotation is vertical
                        {
                            rotation = 90;
                        }
                        else if (fullLevelMap[i, j + 1].Equals(4) || fullLevelMap[i, j + 1].Equals(3))
                        {
                            rotation = 0;
                        }
                    }

                    if (fullLevelMap[i + 1, j].Equals(4) || fullLevelMap[i + 1, j].Equals(3))
                    {
                        if (fullLevelMap[i, j - 1].Equals(4) || fullLevelMap[i, j - 1].Equals(3))
                        {
                            rotation = 180;
                        }
                        else if (fullLevelMap[i, j + 1].Equals(4) || fullLevelMap[i, j + 1].Equals(3))
                        {
                            rotation = 270;
                        }
                    }
                }

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

                if (fullLevelMap[i, j].Equals(7))
                {

                }
                Instantiate(mapParts[fullLevelMap[i,j]], startPos + new Vector3(j, -i, 0.0f), Quaternion.Euler(0.0f, 0.0f, rotation));
                rotation = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
