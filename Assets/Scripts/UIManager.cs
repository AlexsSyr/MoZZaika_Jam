using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string TextBeforeScore = "Final score: ";
    [SerializeField] private string TextBeforeFinalScore = "";
    [SerializeField] private string TextBeforeTime = "";
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text TimeRemainingText;
    [SerializeField] private TMP_Text FinalScoreText;
    private Timer timer;
    private GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen = GameObject.Find("GameOverScreen");
        gameOverScreen.SetActive(false);
        ScoreText.text = TextBeforeScore + 0;

        timer = GameObject.Find("Player").GetComponent<Timer>();
        if (timer == null)
        {
            Debug.LogError("Timer is null");
        }
        TimeRemainingText.text = TextBeforeTime + timer.getTimeRemaining().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemainingText.text = TextBeforeTime + timer.getTimeRemaining().ToString();
    }

    public void updateScore(int playerScore)
    {
        ScoreText.text = TextBeforeScore + playerScore.ToString();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerMovement>().ToLock();
        FinalScoreText.text = TextBeforeFinalScore + ScoreText.text;
    }

    public void Restart()
    {
        SceneManager.LoadScene("scene_final");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
