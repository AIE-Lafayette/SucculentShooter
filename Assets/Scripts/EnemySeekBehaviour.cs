using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySeekBehaviour : MonoBehaviour
{

    private GameObject _target;

    private float _despawnTimer;

    [Tooltip("The time (in seconds) until the enemy despawns itself.")]
    [SerializeField]
    private float _secondsBetweenDespawn;

    private float _explosionTimer;

    [Tooltip("The time (in seconds) until the enemy explodes itself in the scene.")]
    [SerializeField]
    private float _explosionCountdown;

    [Tooltip("The prefab of the particle system you wish to instantiate, when the enemy explodes itself in the game.")]
    [SerializeField]
    private GameObject _explosionParticleSystem;

    private GameObject _explosionInstance;

    [Tooltip("The speed that you want your enemy to chase its target. Default value is 0.")]
    [SerializeField]
    private float _speed;

    private Vector3 _moveDirection;

    private bool _canMove = true;

    private bool _isExploded = false;
    private bool _preparingExplosion;

    public bool PreparingExplosion { get => _preparingExplosion; set => _preparingExplosion = value; }
    public float ExplosionCountdown { get => _explosionCountdown; set => _explosionCountdown = value; }
    public float ExplosionTimer { get => _explosionTimer; set => _explosionTimer = value; }

    private void Awake()
    {
        //Sets the enemy's target to be the 'Player' as designated by the game manager. 
        _target = GameManager.Instance.Player;
    }

    public void OnSpawn()
    {
        HealthBehaviour healthBehaviour = GetComponent<HealthBehaviour>();
        healthBehaviour.Health = healthBehaviour.MaxHealth;
        healthBehaviour.AddOnDeathAction(() =>
        {
            ObjectPoolBehaviour.Instance.ReturnObject(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = _target.transform.position - transform.position;
        _moveDirection.y = 0;

        //Has the enemy's forward set to whatever it's target is.
        transform.LookAt(transform.position + _moveDirection);

        if (_canMove)
        {
            //Updates the position of the game object to move in the direction of its target.
            transform.position += _moveDirection * _speed * Time.deltaTime;
        }

        if (!_canMove)
        {
            Debug.Log("You are supposed to stop here.");
            _moveDirection = transform.position;

            ExplodeMyself();

        }

        //Timer used to track when the enemies need to begin despawning themselves.
        _despawnTimer += Time.deltaTime;


        if (_despawnTimer >= _secondsBetweenDespawn)
        {
            ObjectPoolBehaviour.Instance.ReturnObject(gameObject);
            _despawnTimer = 0;
            _canMove = true;
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barwall"))
        {
            _canMove = false;
            PreparingExplosion = true;
        }
    }

    private void ExplodeMyself()
    {
        ExplosionTimer += Time.deltaTime;

        if (ExplosionTimer >= ExplosionCountdown)
        {
            _explosionInstance = ObjectPoolBehaviour.Instance.GetObject(_explosionParticleSystem, transform.position, transform.rotation);

            ExplosionTimer = 0;

            _isExploded = true;
        }

        if (_isExploded)
        {

            ObjectPoolBehaviour.Instance.ReturnObject(gameObject);

            _isExploded = false;
            _canMove = true;

        }

    }

    private void OnEnable()
    {
        _despawnTimer = 0;
        PreparingExplosion = false;
    }

}
