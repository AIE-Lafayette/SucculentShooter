using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusAnimationBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private HealthBehaviour _healthBehaviour;
    private EnemySeekBehaviour _enemySeekBehaviour;
    private ExplosionBehaviour _explosionBehaviour;

    // Start is called before the first frame update
    void Awake()
    {
        _healthBehaviour = GetComponent<HealthBehaviour>();
        _enemySeekBehaviour = GetComponent<EnemySeekBehaviour>();
        _explosionBehaviour = GetComponent<ExplosionBehaviour>();

        _healthBehaviour.AddOnTakeDamageAction(() => _animator.SetTrigger("Hit"));
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (_enemySeekBehaviour.ExplosionCountdown / _enemySeekBehaviour.ExplosionTimer <= 0.2f)
        //{
        //    _animator.SetTrigger("Explode");
        //}

        //_animator.SetBool("Shake", _enemySeekBehaviour.PreparingExplosion);
    }
}
