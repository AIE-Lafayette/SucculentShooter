using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Tooltip("Player object instance.")]
    public GameObject Player;

    [Tooltip("The current score for the game.")]
    public int Score = 0;

    private void Start()
    {
        if (Player != null)
        {
            HealthBehaviour healthBehaviour = Player.GetComponent<HealthBehaviour>();

            if (healthBehaviour == null)
                healthBehaviour = Player.AddComponent<HealthBehaviour>();

            healthBehaviour.AddOnTakeDamageAction(delegate
            {

            });

            healthBehaviour.AddOnDeathAction(delegate
            {

            });
        }
    }
}
