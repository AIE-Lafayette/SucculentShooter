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

    [Tooltip("The time (in seconds) between spawns for an enemy.")]
    [SerializeField]
    private float _secondsBetweenSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        //_despawnTimer += Time.deltaTime;

        if (_spawnTimer >= _secondsBetweenSpawn)
        {
            _enemyInstance = ObjectPoolBehaviour.Instance.GetObject(_enemyToSpawn, transform.position, transform.rotation);
            _spawnTimer = 0;
        }

    }
}
