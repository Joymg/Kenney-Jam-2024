using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnVictory = new UnityEvent();

    private void Awake()
    {
        OnGameOver?.AddListener(BackToMainSceneTimer);
        OnVictory?.AddListener(BackToMainSceneTimer);
    }

    private void BackToMainSceneTimer()
    {
        StartCoroutine(GoBackToMainScene());
    }
    
    private IEnumerator GoBackToMainScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Menu");
    }
}
