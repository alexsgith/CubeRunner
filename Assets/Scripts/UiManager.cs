using System;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private BasePlayer playerScript;
    [SerializeField] private TMP_Text topScoreText;
    [SerializeField] private TMP_Text currentScoreText;
    int currentTopScore = 0;

    private void Start()
    {
        OpenMainScreen();
    }

    private void OnEnable()
    {
        playerScript.GameOver+= OpenGameOverScreen;
        playerScript.ScoreUpdated+= UpdateScore;
    }

    private void OnDisable()
    {
        playerScript.ScoreUpdated-= UpdateScore;
        playerScript.GameOver-= OpenGameOverScreen;
    }

    private void UpdateScore(int score)
    {
        currentScoreText.text = score.ToString();
    }

    public void StartGame()
    {
        OpenGameScreen();
        playerScript.StartPlay();
    }
    public void RestartGame()
    {
        OpenGameScreen();
        playerScript.StartPlay();
    }

    public void OnExit()
    {
        Application.Quit();
    }

    void OpenGameScreen()
    {
        mainMenuScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void OpenMainScreen()
    {
        mainMenuScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        currentTopScore = 0;
    }
    void OpenGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
        currentTopScore = currentTopScore > playerScript.GetScore() ? currentTopScore : playerScript.GetScore();
        topScoreText.text = "TopScore: " + currentTopScore;
        topScoreText.gameObject.SetActive(currentTopScore > 0);
    }
}
