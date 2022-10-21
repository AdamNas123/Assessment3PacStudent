using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject exitButton;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(exitButton, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFirstLevel()
    {
        
        SceneManager.LoadScene("Level1Scene");
        
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(exitButton);
        
        //newButton.transform.SetParent(GameObject.Find("HUD").transform, false);
        //exitButton.gameObject.SetActive(true);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /*public void LoadSecondLevel()
    {
        SceneManager.LoadScene("Level2Scene");
        DontDestroyOnLoad(gameObject);
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }*/

    public void LoadStartScreen() 
    {
        SceneManager.LoadScene("StartScene");
        //DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == SceneManager.GetSceneByName("Level1Scene").buildIndex)
        {
            GameObject newButton = Instantiate(exitButton.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            newButton.transform.SetParent(GameObject.Find("HUD").transform, false);
            newButton.GetComponent<Button>().onClick.AddListener(this.LoadStartScreen);
        }
    }
}
