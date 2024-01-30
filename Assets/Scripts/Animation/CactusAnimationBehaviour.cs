using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusAnimationBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField, Tooltip("The amount of time that has to be left in the explosion countdown in order for the anmimation to play.")]
    private float _explodeAnimationTimeStamp;
    private HealthBehaviour _healthBehaviour;
    private EnemySeekBehaviour _enemySeekBehaviour;

    // Start is called before the first frame update
    void Awake()
    {
        _healthBehaviour = GetComponent<HealthBehaviour>();
        _enemySeekBehaviour = GetComponent<EnemySeekBehaviour>();

        _healthBehaviour.AddOnTakeDamageAction(() => _animator.SetTrigger("Hit"));
        
        _healthBehaviour.AddOnDeathAction(() => _animator.SetTrigger("Dead"));
    }

    private void OnEnable()
    {
        _animator.SetTrigger("Reset");
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemySeekBehaviour.ExplosionCountdown - _enemySeekBehaviour.ExplosionTimer <= _explodeAnimationTimeStamp)
        {
            _animator.SetTrigger("Explode");
        }

        _animator.SetBool("Shake", _enemySeekBehaviour.PreparingExplosion);
    }
}
