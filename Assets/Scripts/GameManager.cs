using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (!_instance)
				_instance = FindObjectOfType<GameManager>();

			if (!_instance)
			{
				Debug.Log("No GameManager found in scene. Creating one.");
				_instance = new GameObject("GameManager").AddComponent<GameManager>();
			}

			return _instance;
		}
	}

	[SerializeField, Tooltip("Array of game objects that should be toggled off when the game is not started and on when it is.")]
	private GameObject[] _gameActiveObjects;

	[SerializeField, Tooltip("Called when the game starts.")]
	private UnityEvent _gameStarted;

	[SerializeField, Tooltip("Called when the game ends.")]
	private UnityEvent _gameEnded;

	[SerializeField, Tooltip("Called when the game is paused.")]
	private UnityEvent _gamePaused;

	[Tooltip("Player object instance.")]
	public GameObject Player;

	[Tooltip("The current score for the game.")]
	public int Score = 0;

    [SerializeField]
    private GameObject _leftController;

    [SerializeField]
    private GameObject _rightController;

    private bool _isStarted = false;
	private bool _isPaused = false;
	public bool IsStarted => _isStarted;
	public bool IsPaused => _isPaused;	

	public void AddGameStartedAction(UnityAction action) => _gameStarted.AddListener(action);
	public void AddGameEndedAction(UnityAction action) => _gameEnded.AddListener(action);
	public void AddGamePausedAction(UnityAction action) => _gamePaused.AddListener(action);

    public void EndGame()
	{
        for (int i = 0; i < _gameActiveObjects.Length; i++)
        {
            _gameActiveObjects[i].SetActive(false);
        }

		_isStarted = false;

        _gameEnded?.Invoke();
    }

	public void ToggleGamePaused()
	{
		if (!_isStarted)
			return;

		_isPaused = !_isPaused;

		Time.timeScale = _isPaused ? 0 : 1;

		_gamePaused?.Invoke();
	}

    public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	public void StartGame()
	{
		for (int i = 0; i < _gameActiveObjects.Length; i++)
		{
			_gameActiveObjects[i].SetActive(true);
		}

		_gameStarted?.Invoke();

		_isStarted = true;
	}

	private void Start()
	{
        for (int i = 0; i < _gameActiveObjects.Length; i++)
        {
            _gameActiveObjects[i].SetActive(false);
        }

        if (Player != null)
		{
			HealthBehaviour healthBehaviour = Player.GetComponent<HealthBehaviour>();

			if (healthBehaviour == null)
				healthBehaviour = Player.AddComponent<HealthBehaviour>();
			
			healthBehaviour.AddOnDeathAction(EndGame);
		}
	}

	public void Quit()
	{
		Application.Quit();
	}
}
