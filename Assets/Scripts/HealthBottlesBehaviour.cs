using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBottlesBehaviour : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> _healthBottles;

    void UpdateBottles()
    {
        if (_healthBottles.Count == 0)
            return;

        for (int i = 0; i < _healthBottles.Count; i++)
        {
            _healthBottles[i].SetActive(false);

            if (i < GameManager.Instance.Player.GetComponent<HealthBehaviour>().Health)
                _healthBottles[i].SetActive(true);
        }
    }

    private void Start()
    {
        UpdateBottles();
        GameManager.Instance.Player.GetComponent<HealthBehaviour>().AddOnTakeDamageAction(UpdateBottles);
    }
}
