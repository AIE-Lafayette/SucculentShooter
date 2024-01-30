using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    private static UserInterfaceManager _instance;

    [SerializeField]
    private GameObject _startMenu;
    public GameObject StartMenu => _startMenu;

    [SerializeField]
    private GameObject _gameOverMenu;

    [SerializeField]
    private GameObject _pauseMenu;

    // pause menu stuff
    [SerializeField]
    private GameObject _pauseMenuButtons;
    [SerializeField]
    private GameObject _settingsMenu;

    public static UserInterfaceManager Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<UserInterfaceManager>();

            if (!_instance)
            {
                Debug.Log("No UserInterfaceManager found in scene. Creating one.");
                _instance = new GameObject("UserInterfaceManager").AddComponent<UserInterfaceManager>();

            }

            return _instance;
        }
    }

    private void Start()
    {
        if (_gameOverMenu)
        {
            _gameOverMenu.SetActive(false);
            GameManager.Instance.AddGameEndedAction(() =>
            {
                _gameOverMenu.SetActive(true);

                if (_startMenu)
                    _startMenu.SetActive(false);
            });
        }

        if (_pauseMenu)
        {
            GameManager.Instance.AddGamePausedAction(() =>
            {
                if (_gameOverMenu && _gameOverMenu.activeInHierarchy)
                    return;

                if (!_pauseMenu)
                    return;

                _pauseMenu.SetActive(GameManager.Instance.IsPaused);

                if (_settingsMenu)
                    _settingsMenu.SetActive(false);

                if (_pauseMenuButtons)
                    _pauseMenuButtons.SetActive(true);
            });
        }
    }

    public void StartButtonPressed()
    {
        if (_startMenu != null)
            _startMenu.SetActive(false);

        GameManager.Instance.StartGame();
    }

    public void RestartButtonPressed()
    {
        GameManager.Instance.RestartGame();
    }

    public void SettingsMenuButtonPressed()
    {
        if (!_settingsMenu)
            return;

        if (_pauseMenuButtons)
            _pauseMenuButtons.SetActive(false); 

        _settingsMenu.SetActive(true);
    }

    public void SettingsMenuBackButtonPressed()
    {
        if (!_pauseMenuButtons)
            return;

        if (!_settingsMenu)
            return;

        _settingsMenu.SetActive(false);
        _pauseMenuButtons.SetActive(true);
    }
}
