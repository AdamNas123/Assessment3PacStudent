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

        for (int i = 0; i < levelMap.GetLength(0); ++ i)
        {
            for (int j = 0; j < levelMap.GetLength(1); ++j)
            {
                if (mapParts[levelMap[i,j]].Equals(1))
                {
                    if (levelMap[i - 1, j].Equals(null))
                    {
                        rotation = 270;
                    }
                }
                Instantiate(mapParts[levelMap[i,j]], startPos + new Vector3(j, -i, 0.0f), Quaternion.Euler(0.0f, 0.0f, rotation));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
