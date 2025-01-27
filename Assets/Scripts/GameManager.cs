using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Text countdownText;

    void Start()
    {
        Application.targetFrameRate = 60;
        playerController = GameObject.FindWithTag(Variables.tagPlayer).GetComponent<PlayerController>();
        UpdateScoreUI();
        //Time.timeScale = gamePause;
        //Invoke("SetTimeScale", 3);
        //GamePause();
        //Invoke("GamePauseRelease", 3);
        StartCoroutine(DelayCoroutine());
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

    private IEnumerator DelayCoroutine()
    {
        Time.timeScale = 0;
        isGamePause = true;

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1);

        countdownText.text = "";
        Time.timeScale = 1;
        isGamePause = false;
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
