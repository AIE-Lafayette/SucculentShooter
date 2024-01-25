using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    private static UserInterfaceManager _instance;

    [SerializeField]
    private GameObject _startMenu;

    public GameObject StartMenu => _startMenu;

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

    public void StartButtonPressed()
    {
        if (_startMenu != null)
            _startMenu.SetActive(false);

        GameManager.Instance.StartGame();
    }

}
