using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGameWithDelay);
    }

    void StartGameWithDelay()
    {
        StartCoroutine(LoadGameSceneAfterDelay());
    }

    IEnumerator LoadGameSceneAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }
}
