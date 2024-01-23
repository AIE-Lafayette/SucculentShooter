using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponHitScanBehaviour : MonoBehaviour
{

    [SerializeField, Tooltip("Key that fires the weapon.")]
    private KeyCode _fireKey = KeyCode.Mouse0;
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

    [SerializeField, Tooltip("Toggle debug logging and rays.")]
    private bool _debugMode = false;

    // initialize canfire as true, will be set to false with delay.
    private bool canFire = true;

    // time that passes between shots. 
    private float elapsedTime = 0f;

    /// <summary>
    /// Takes care of input and delay for firing the weapons.
    /// </summary>
    public void Update()
    {
        if (!canFire)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= _fireDelay)
            {
                canFire = true;
                elapsedTime = 0f;
            }
        }

        if (Input.GetKeyDown(_fireKey))
            Fire(transform.TransformDirection(Vector3.forward));
    }
    

    /// <summary>
    /// Fires the weapon.
    /// </summary>
    /// <param name="direction">The direction to send the ray in.</param>
    /// <returns>The collider hit by the ray, if any.</returns>
    public Collider Fire(Vector3 direction)
    {
        Vector3 origin = transform.position;

        if (_firePoint != null)
            origin = _firePoint.transform.position;

        
        if (_fireSound != null)
        {
            // play audio clip
        }

        // don't forget to make this use the pool behavior - bryon
        if (_fireVFX != null)
            Instantiate(_fireVFX, origin, Quaternion.identity);

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
        {
            // play audio clip
        }

        // don't forget to make this use the pool behavior - bryon
        if (_hitVFX != null)
            Instantiate(_hitVFX, raycastHit.point, Quaternion.identity);

        return raycastHit.collider;
    }
}
