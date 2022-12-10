using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeRemaining;
    private bool timerIsRunning = false;
    // Start is called before the first frame update
    void Start()
    {
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
            }
        }
    }

    public float getTimeRemaining()
    {
        return timeRemaining; 
    }
}
