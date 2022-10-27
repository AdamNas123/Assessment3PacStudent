using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField]
    private Tweener tweener;
    [SerializeField]
    private GameObject cherry;
    private GameObject currentCherry;
    private string randomPos;
    private float xPos;
    private float yPos;
    // Start is called before the first frame update
    void Start()
    {
        randomPos = "x";    
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Time.time > 0 && (int)Time.time % 10 == 0 && currentCherry == null)
        {
            SpawnCherry();
        }
        if (currentCherry != null && !tweener.TweenExists(currentCherry.transform))
        {
            Destroy(currentCherry);
        }
    }

    private void SpawnCherry()
    {
        Vector3 endPos;
        if (randomPos == "x")
        {
            xPos = Random.Range(0.0f, 1.0f);
            yPos = Random.Range(0, 2);
            if (yPos == 0)
            {
                endPos = new Vector3(1.0f - xPos, 1.0f, 1.0f);
            }
            else
            {
                endPos = new Vector3(1.0f - xPos, 0.0f, 1.0f);
            }
            randomPos = "y";
        }
        else 
        {
            yPos = Random.Range(0.0f, 1.0f);
            xPos = Random.Range(0, 2);
            if (xPos == 0)
            {
                endPos = new Vector3(1.0f, 1.0f - yPos, 1.0f);
            }
            else
            {
                endPos = new Vector3(0.0f, 1.0f - yPos, 1.0f);
            }
            randomPos = "x";
        }
        //Vector3 spawnPos = new Vector3(xPos, yPos, 0.0f);
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(xPos, yPos, 1.0f));
        currentCherry = Instantiate(cherry, spawnPos, Quaternion.identity);
        Vector3 worldEndPos = Camera.main.ViewportToWorldPoint(endPos);
        tweener.AddTween(currentCherry.transform, currentCherry.transform.position, worldEndPos, 5.0f);
    }
}
