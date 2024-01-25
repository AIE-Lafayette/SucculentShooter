using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBehaviour : MonoBehaviour
{
	[SerializeField, Tooltip("Action used for firing the player's left weapon.")]
	private InputActionReference _leftShootAction;
	[SerializeField, Tooltip("Action used for firing the player's right weapon.")]
	private InputActionReference _rightShootAction;

	[SerializeField, Tooltip("Player's left weapon.")]
	private WeaponHitScanBehaviour _leftWeapon;

	[SerializeField, Tooltip("Player's right weapon.")]
	private WeaponHitScanBehaviour _rightWeapon;


	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;

		if (_leftWeapon)
			 _leftShootAction.action.performed += context => _leftWeapon.Fire(_leftWeapon.transform.TransformDirection(Vector3.forward));

		if(_rightWeapon)
			_rightShootAction.action.performed += context => _rightWeapon.Fire(_rightWeapon.transform.TransformDirection(Vector3.forward));
  
	}
}
