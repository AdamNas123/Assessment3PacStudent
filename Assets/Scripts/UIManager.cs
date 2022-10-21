using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        //exitButton = GameObject.FindGameObjectWithTag("Exit");
        exitButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1Scene");
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(exitButton);
        exitButton.gameObject.SetActive(true);
        
        //SceneManager.sceneLoaded += OnSceneLoaded;
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
    }
}
