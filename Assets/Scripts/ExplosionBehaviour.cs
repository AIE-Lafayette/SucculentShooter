using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    [Tooltip(" The damage given to a player once an explosion as occured.")]
    [SerializeField]
    private int _damage = 1;

    private float _explosionPSDespawnTimer;

    [Tooltip("The time (in seconds) until the particle system instantiated by an enemy despawns after it explodes itself in the scene.")]
    [SerializeField]
    private float _explosionPSDespawnWaitTime;

    public float ExplosionPSDespawnWaitTime { get => _explosionPSDespawnWaitTime; private set => _explosionPSDespawnWaitTime = value; }

    private void OnEnable()
    {
        HealthBehaviour healthBehaviour = GameManager.Instance.Player.GetComponent<HealthBehaviour>();

        Debug.Assert(healthBehaviour != null, "Player must have a health behaviour.");

        healthBehaviour.TakeDamage(_damage);
    }

    // Update is called once per frame
    void Update()
    {

        _explosionPSDespawnTimer += Time.deltaTime;

        if (_explosionPSDespawnTimer >= ExplosionPSDespawnWaitTime)
        {
            ObjectPoolBehaviour.Instance.ReturnObject(gameObject);
            _explosionPSDespawnTimer = 0;
        }
    }


}
