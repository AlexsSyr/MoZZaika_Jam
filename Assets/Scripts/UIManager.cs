using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string TextBeforeScore = "";
    [SerializeField] private string TextBeforeTime = "";
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text TimeRemainingText;
    private Timer _Timer;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = TextBeforeScore + 0;

        _Timer = GameObject.Find("Player").GetComponent<Timer>();
        if (_Timer == null)
        {
            Debug.LogError("Timer is null");
        }
        TimeRemainingText.text = TextBeforeTime + _Timer.getTimeRemaining().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemainingText.text = TextBeforeTime + _Timer.getTimeRemaining().ToString();
    }

    public void updateScore(int playerScore)
    {
        ScoreText.text = TextBeforeScore + playerScore.ToString();
    }

    public void updateTimer(float timeRemaining)
    {
        TimeRemainingText.text = TextBeforeTime + timeRemaining.ToString();
    }

    public void gameOver()
    {
        TimeRemainingText.text = "Time is over";
    }
}
