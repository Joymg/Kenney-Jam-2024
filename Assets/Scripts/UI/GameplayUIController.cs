using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void Awake()
    {
        GameManager.OnGameOver?.AddListener(ShowGameOverUI);
        _panel.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void ShowGameOverUI()
    {
        _panel.SetActive(true);
    }

    
}
