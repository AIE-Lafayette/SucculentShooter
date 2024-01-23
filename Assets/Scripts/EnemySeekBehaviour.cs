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

    [Tooltip("The speed that you want your enemy to chase its target. Default value is 0.")]
    [SerializeField]
    private float _speed;

    private Vector3 _moveDirection;

    private void Awake()
    {
        //Sets the enemy's target to be anything with the 'Player' tag in the scene. 
        _target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
         _moveDirection = _target.transform.position - transform.position;
        _moveDirection.y = 0;

        //Has the enemy's forward set to whatever it's target is.
        transform.LookAt(transform.position + _moveDirection);

        //Updates the position of the game object to move in the direction of its target.
        transform.position += _moveDirection * _speed * Time.deltaTime;

        //Timer used to track when the enemies need to begin despawning themselves.
        _despawnTimer += Time.deltaTime;

        if (_despawnTimer >= _secondsBetweenDespawn)
        {
           ObjectPoolBehaviour.Instance.ReturnObject(gameObject);
            _despawnTimer = 0;
        }




    }
}
