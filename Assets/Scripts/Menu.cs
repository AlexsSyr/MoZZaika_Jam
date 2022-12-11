using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private GameObject creditsScreen;
    private GameObject mainScreen;
    // Start is called before the first frame update
    void Start()
    {
        mainScreen = GameObject.Find("MainScreen");
        creditsScreen = GameObject.Find("CreditsScreen");
        creditsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("scene_final");
    }

    public void ShowCredits()
    {
        mainScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }

    public void ShowMainScreen()
    {
        mainScreen.SetActive(true);
        creditsScreen.SetActive(false);
    }
}
