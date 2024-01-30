using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponHitScanBehaviour : MonoBehaviour
{
    [SerializeField, Tooltip("Layers that the bullets can collide with.")]
    private LayerMask _layerMask;

    [SerializeField, Tooltip("Sound that plays when the weapon fires.")]
    private AudioClip _fireSound;
    [SerializeField, Tooltip("Sound that plays when the weapon hits.")]
    private AudioClip _hitSound;

    [SerializeField, Tooltip("VFX created when firing.")]
    private GameObject _fireVFX;
    [SerializeField, Tooltip("VFX created at the position of a hit.")]
    private GameObject _hitVFX;

    [SerializeField, Tooltip("Optional object to fire from, will otherwise use this object's transform.")]
    private GameObject _firePoint;

    [SerializeField, Tooltip("Maximum distance the weapon can fire.")]
    private float _maxDistance = 10f;

    [SerializeField, Tooltip("Delay between shots.")]
    private float _fireDelay = 0.1f;

    [SerializeField, Tooltip("How much damage the weapon should do per hit.")]
    private int _damage = 1;

    [SerializeField, Tooltip("Toggle debug logging and rays.")]
    private bool _debugMode = false;

    /// <summary>
    /// Fires the weapon.
    /// </summary>
    /// <param name="direction">The direction to send the ray in.</param>
    /// <returns>The collider hit by the ray, if any.</returns>
    public Collider Fire(Vector3 direction)
    {
        if (GameManager.Instance.IsPaused || !GameManager.Instance.IsStarted)
            return null;

        Vector3 origin = transform.position;

        if (_firePoint != null)
            origin = _firePoint.transform.position;

        
        if (_fireSound != null)
            SoundManager.Instance.PlaySoundAtPosition(origin, _fireSound, 1, 0);


        // don't forget to make this use the pool behavior - bryon
        if (_fireVFX != null)
            ObjectPoolBehaviour.Instance.GetObject(_fireVFX, origin, Quaternion.identity);

        RaycastHit raycastHit;
        bool didHit = Physics.Raycast(origin, direction, out raycastHit, _maxDistance, _layerMask);

        if (!didHit)
        {
            if (_debugMode)
            {
                Debug.DrawRay(origin, direction * _maxDistance, Color.green, _fireDelay);
                Debug.Log("Ya missed. Try again.");
            }

            return null;
        }

        if(_debugMode)
        {
            Debug.DrawRay(origin, direction * raycastHit.distance, Color.red, _fireDelay);
            Debug.Log("Ya hit somethin' partner.");
        }

        if (_hitSound != null)
            SoundManager.Instance.PlaySoundAtPosition(raycastHit.point, _fireSound, 1, 0);


        // don't forget to make this use the pool behavior - bryon
        if (_hitVFX != null)
            ObjectPoolBehaviour.Instance.GetObject(_hitVFX, raycastHit.point, Quaternion.identity);

        Collider hit = raycastHit.collider;

        if (hit == null)
            return hit;

        // make sure they have a health behaviour before trying to damage them
        HealthBehaviour healthBehaviour = hit.attachedRigidbody.GetComponent<HealthBehaviour>();
        if (!healthBehaviour)
            return hit;

        healthBehaviour.TakeDamage(_damage);

        return hit;
    }
}
