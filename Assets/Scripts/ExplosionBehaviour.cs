using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    private GameObject _target;

    [Tooltip(" The damage given to a player once an explosion as occured.")]
    [SerializeField]
    private int _damage = 0;

    private float _explosionPSDespawnTimer;

    [Tooltip("The time (in seconds) until the particle system instantiated by an enemy despawns after it explodes itself in the scene.")]
    [SerializeField]
    private float _explosionPSDespawnWaitTime;

    public float ExplosionPSDespawnWaitTime { get => _explosionPSDespawnWaitTime; private set => _explosionPSDespawnWaitTime = value; }

    // Start is called before the first frame update
    void Start()
    {
        //Sets the enemy's target to be the 'Player' as designated by the game manager. 
        _target = GameManager.Instance.Player;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You've taken damage from an explosion.");
            _target.GetComponent<HealthBehaviour>().TakeDamage(_damage);
        }
            
    }


}
