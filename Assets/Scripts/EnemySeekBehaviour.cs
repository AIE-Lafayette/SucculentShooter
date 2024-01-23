using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeekBehaviour : MonoBehaviour
{

    [Tooltip("The game object that you want the enemy to look at and chase.")]
    [SerializeField]
    private GameObject _target;

    [Tooltip("The speed that you want your enemy to chase its target. Default value is 0.")]
    [SerializeField]
    private float _speed;

    //[SerializeField]
    //private float _distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //_distance = Vector3.Distance(transform.position, _target.transform.position);
        //Vector3 direction = _target.transform.position - transform.position;

        //direction.Normalize();

        transform.LookAt(_target.transform.position);

        transform.position = Vector3.MoveTowards(this.transform.position, _target.transform.position, _speed * Time.deltaTime);

    }
}
