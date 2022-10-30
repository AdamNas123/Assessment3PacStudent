using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField]
    public Tweener tweener;
    [SerializeField]
    private GameObject cherry;
    private GameObject currentCherry;
    private string[] positions = { "x", "y" }; 
    private float xPos;
    private float yPos;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Time.time > 0 && (int)Time.time % 10 == 0 && currentCherry == null)
        {
            SpawnCherry();
        }
        if (currentCherry != null)
        {
            if (!tweener.TweenExists(currentCherry.transform))
            {
                Destroy(currentCherry);
            }
        }   
    }

    private void SpawnCherry()
    {
        Vector3 endPos;
        string randomPos = positions[Random.Range(0, 2)];
        float[] borderPos = { -0.1f, 1.1f };
        if (randomPos == "x")
        {
            xPos = Random.Range(0.0f, 1.0f);
            yPos = borderPos[Random.Range(0, 2)];
            if (yPos == -0.1f)
            {
                endPos = new Vector3(1.0f - xPos, 1.1f, 1.0f);
            }
            else
            {
                endPos = new Vector3(1.0f - xPos, -0.1f, 1.0f);
            }
        }
        else 
        {
            yPos = Random.Range(0.0f, 1.0f);
            xPos = borderPos[Random.Range(0, 2)];
            if (xPos == -0.1f)
            {
                endPos = new Vector3(1.1f, 1.0f - yPos, 1.0f);
            }
            else
            {
                endPos = new Vector3(-0.1f, 1.0f - yPos, 1.0f);
            }
        }

        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(xPos, yPos, 1.0f));
        currentCherry = Instantiate(cherry, spawnPos, Quaternion.identity);
        //currentCherry = Instantiate(cherry, new Vector3(-14.5f, -1.5f, 0.0f), Quaternion.identity);
        Vector3 worldEndPos = Camera.main.ViewportToWorldPoint(endPos);
        tweener.AddTween(currentCherry.transform, currentCherry.transform.position, worldEndPos, 8.0f);
    }
}
