using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent OnGameOver;

    private void Awake()
    {
        OnGameOver.AddListener(GameOver);
    }

    private void GameOver()
    {

    }
}
