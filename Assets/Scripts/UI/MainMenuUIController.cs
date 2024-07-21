using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _controlsPanel;

    private void Start()
    {
        ActivateMainMenuPanel();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ActivateControlsPanel()
    {
        _controlsPanel.SetActive(true);
        _mainMenuPanel.SetActive(false);
    }

    public void ActivateMainMenuPanel()
    {
        _controlsPanel.SetActive(false);
        _mainMenuPanel.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
