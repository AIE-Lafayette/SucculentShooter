using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleReturnBehaviour : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectPoolBehaviour.Instance.ReturnObject(gameObject);

    }
}
