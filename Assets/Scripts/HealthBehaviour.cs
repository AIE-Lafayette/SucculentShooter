using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField, Tooltip("The maximum health of the entity.")]
    private int _maxHealth = 3;

    [SerializeField, Tooltip("The health of the entity, this will change with damage.")]
    private int _health = 3;

    [SerializeField, Tooltip("Called when the GameObject is damaged.")]
    private UnityEvent _onTakeDamage;

    [SerializeField, Tooltip("Called when the GameObject is damaged, but only once.")]
    private UnityEvent _onTakeDamageTemp;

    [SerializeField, Tooltip("Called when the GameObject's health drops at or below zero.")]
    private UnityEvent _onDeath;

    [SerializeField, Tooltip("Whether or not debug elements should show.")]
    private bool _debugMode = false;

    [SerializeField, Tooltip("Debug text element to show health if Debug Mode is on.")]
    private Text _debugText;

    public int Health { set => _health = value; get => _health; }
    public int MaxHealth => _maxHealth;

    /// <summary>
    /// Add a listener to the OnTakeDamage Event.
    /// </summary>
    public void AddOnTakeDamageAction(UnityAction action) => _onTakeDamage.AddListener(action);
    
    /// <summary>
    /// Add a temporary listener to OnDamageTakenTemp that is only called once.
    /// </summary>
    public void AddOnTakeDamageTempAction(UnityAction action) => _onTakeDamageTemp.AddListener(action);

    /// <summary>
    /// Causes the GameObject to take damage. If the damage is zero or less, damage events will not be called.
    /// </summary>
    /// <param name="damage">The amount of damage taken.</param>
    /// <returns>A boolean stating whether the GameObject has died.</returns>
    public bool TakeDamage(int damage)
    {
        bool didDie = false;
        if (damage <= 0)
            return false;

        if (damage >= _health)
        {
            _health = 0;
            _onDeath?.Invoke();
            didDie = true;
        }

        _health -= damage;

        _onTakeDamage?.Invoke();
        _onTakeDamageTemp?.Invoke();

        _onTakeDamageTemp.RemoveAllListeners();

        if (_debugMode && _debugText)
            _debugText.text = _health.ToString() + " / " + _maxHealth.ToString();

        return didDie;
    }
}
