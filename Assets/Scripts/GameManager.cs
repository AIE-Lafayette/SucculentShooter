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

	private UnityEvent _gameStarted;
	private UnityEvent _gameEnded;

	[Tooltip("Player object instance.")]
	public GameObject Player;

	[Tooltip("The current score for the game.")]
	public int Score = 0;

	private bool _isStarted = false;
	public bool IsStarted => _isStarted;

	public void AddGameStartedAction(UnityAction action) => _gameStarted.AddListener(action);
	public void AddGameEndedAction(UnityAction action) => _gameEnded.AddListener(action);

    private void RestartGame()
	{
		_gameEnded?.Invoke();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	public void StartGame()
	{
		_gameStarted?.Invoke();

		_isStarted = true;
	}

	private void Start()
	{
		if (Player != null)
		{
			HealthBehaviour healthBehaviour = Player.GetComponent<HealthBehaviour>();

			if (healthBehaviour == null)
				healthBehaviour = Player.AddComponent<HealthBehaviour>();
			
			healthBehaviour.AddOnDeathAction(RestartGame);
		}
	}
}
