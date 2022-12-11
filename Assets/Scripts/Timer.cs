using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeRemaining;
    private bool timerIsRunning = false;
    private UIManager uIManager;
    private ScoreCounter scoreCounter; 
    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (uIManager == null)
        {
            Debug.LogError("UI manager is null");
        }

        scoreCounter = GameObject.Find("Player").GetComponent<ScoreCounter>();
        if (scoreCounter == null)
        {
            Debug.LogError("score counter is null");
        }


        timerIsRunning = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                //Debug.Log($"Remaining time is {timeRemaining}");
            }
            else
            {
                //Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                uIManager.GameOver();
            }
        }
    }

    public string getTimeRemaining()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        string time = string.Format("{0:00}:{1:00}", minutes, seconds);
        return time; 
    }
}
