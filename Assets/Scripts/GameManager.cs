using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent OnGameOver = new UnityEvent();

    private void Awake()
    {
        OnGameOver.AddListener(GameOver);
    }

    private void GameOver()
    {
        StartCoroutine(GoBackToMainScene());
    }
    private IEnumerator GoBackToMainScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Menu");
    }
}
