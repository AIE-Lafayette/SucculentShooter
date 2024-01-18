using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField, Tooltip("The maximum health of the entity.")]
    private int _maxHealth = 3;

    [SerializeField, Tooltip("The health of the entity, this will change with damage.")]
    private int _health = 3;

    public int Health => _health;
    public int MaxHealth => _maxHealth;


    public bool TakeDamage(int damage)
    {
        if (damage <= 0)
            return false;

        if (damage >= _health)
        {
            _health = 0;
            return true;
        }

        _health -= damage;

        return false;
    }
}
