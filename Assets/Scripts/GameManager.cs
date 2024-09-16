using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region UI
    [Header("GameOverUI")]
    [SerializeField] GameObject UIGameOver;

    [Header("GamePauseUI")]
    [SerializeField] GameObject UIGamePause;

    #endregion

    #region Score
    [Header("ScoreUI")]
    [SerializeField] TMP_Text UIScoreNum;
    #endregion

    #region Internal
    PlayerController playerController;

    int score = 0;

    public static bool isGamePause = false;
    public static bool isGameOver = false;

    public const float gamePause = 0;
    public const float gamePauseRelease = 1;
    #endregion

    void Start()
    {
        Application.targetFrameRate = 60;
        playerController = GameObject.FindWithTag(Variables.tagPlayer).GetComponent<PlayerController>();
        UpdateScoreUI();
    }
    void Update()
    {
        if (isGameOver)
        {
            GameOver();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameRetry();
            }
        }
        else
        {
            if (!isGamePause)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GamePause();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GamePauseRelease();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameQuit();
        }
    }
    public void GamePause()
    {
        isGamePause = true;
        Time.timeScale = gamePause;
        VisibleUIGamePause();
    }
    public void GamePauseRelease()
    {
        isGamePause = false;
        Time.timeScale = gamePauseRelease;
        HiddenUIGamePause();
    }
    void VisibleUIGamePause()
    {
        UIGamePause.SetActive(true);
    }
    void HiddenUIGamePause()
    {
        UIGamePause.SetActive(false);
    }
    void GameOver()
    {
        Time.timeScale = gamePause;
        VisibleUIGameOver();
    }
    void VisibleUIGameOver()
    {
       UIGameOver.SetActive(true);
    }
    public void GameRetry()
    {
        isGameOver=false;
        playerController.PlayerOperable();
        Time.timeScale = gamePauseRelease;
        UIGameOver.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    public void AddScore()
    {
        score++;
        UpdateScoreUI();
    }
    void UpdateScoreUI()
    {
        UIScoreNum.SetText(score.ToString());
    }
}
