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
               /* if (levelMap[i,j].Equals(2))
                {
                    if ((i-1) < 0 && (levelMap[i+1,j].Equals(0) || levelMap[i + 1, j].Equals(5) || levelMap[i + 1, j].Equals(6)))
                    {
                        rotation = 270;
                    }
                    if ((j - 1) < 0 && (levelMap[i, j + 1].Equals(0) || levelMap[i, j + 1].Equals(5) || levelMap[i, j + 1].Equals(6)))
                    {
                        rotation = 0;
                    }
                    if ((i + 1) > levelMap.GetLength(0) && (levelMap[i - 1, j].Equals(0) || levelMap[i - 1, j].Equals(5) || levelMap[i - 1, j].Equals(6)))
                    {
                        rotation = 90;
                    }
                    if ((j + 1) > levelMap.GetLength(1) && (levelMap[i , j - 1].Equals(0) || levelMap[i, j - 1].Equals(5) || levelMap[i, j - 1].Equals(6)))
                    {
                        rotation = 180;
                    }
                }
                if (levelMap[i, j].Equals(3))
                {

                }*/
                Instantiate(mapParts[fullLevelMap[i,j]], startPos + new Vector3(j, -i, 0.0f), Quaternion.Euler(0.0f, 0.0f, rotation));
                rotation = 0.0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
