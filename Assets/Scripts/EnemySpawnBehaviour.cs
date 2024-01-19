using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBehaviour : MonoBehaviour
{
    [Tooltip("Drag and drop the enemy prefab you wish to be spawned here.")]
    [SerializeField]
    private GameObject _enemyToSpawn;

    private GameObject _enemyInstance;

    private float _spawnTimer;
    private float _despawnTimer;

    [Tooltip("The time between spawns for an enemy.")]
    [SerializeField]
    private float _secondsBetweenSpawn;

    [Tooltip("The time between despawns for an enemy.")]
    [SerializeField]
    private float _secondsBetweenDespawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        _despawnTimer += Time.deltaTime;

        if (_spawnTimer >= _secondsBetweenSpawn)
        {
            _enemyInstance = ObjectPoolBehaviour.Instance.GetObject(_enemyToSpawn);
            _spawnTimer = 0;
        }

        if (_despawnTimer >= _secondsBetweenDespawn)
        {
           ObjectPoolBehaviour.Instance.ReturnObject(_enemyInstance);
            _despawnTimer = 0;
        }

    }
}
