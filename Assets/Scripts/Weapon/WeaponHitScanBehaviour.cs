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

    [SerializeField]
    private float _maxDistance = 10f;

    public Collider Fire(Vector3 direction)
    {
        Vector3 origin = transform.position;

        if (_firePoint != null)
            origin = _firePoint.transform.position;

        
        if (_fireSound != null)
        {
            // play audio clip
        }

        if (_fireVFX != null)
            Instantiate(_fireVFX, origin, Quaternion.identity);

        RaycastHit raycastHit;
        bool didHit = Physics.Raycast(origin, direction, out raycastHit, _maxDistance, _layerMask);

        if (!didHit)
        {
            Debug.DrawRay(origin, direction * _maxDistance, Color.green,5);
            Debug.Log("Ya missed. Try again.");
            return null;
        }

        Debug.DrawRay(origin, direction * raycastHit.distance, Color.red,5);
        Debug.Log("Ya hit somethin' partner.");

        if (_hitSound != null)
        {
            // play audio clip
        }

        if (_hitVFX != null)
            Instantiate(_hitVFX, raycastHit.point, Quaternion.identity);

        return raycastHit.collider;
    }
}
