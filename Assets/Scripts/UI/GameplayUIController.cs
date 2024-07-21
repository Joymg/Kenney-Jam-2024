using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _victoryPanel;

    private void Awake()
    {
        GameManager.OnGameOver?.AddListener(ShowGameOverUI);
        GameManager.OnVictory?.AddListener(ShowVictoryUI);
        _gameOverPanel.SetActive(false);
        _victoryPanel.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void ShowGameOverUI()
    {
        _gameOverPanel.SetActive(true);
    }

    private void ShowVictoryUI()
    {
        _victoryPanel.SetActive(true);
    }
}
