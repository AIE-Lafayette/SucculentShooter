using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    private GameObject _target;

    [Tooltip(" The damage given to a player once an explosion as occured.")]
    [SerializeField]
    private int _damage = 0;

    private
    // Start is called before the first frame update
    void Start()
    {
        //Sets the enemy's target to be the 'Player' as designated by the game manager. 
        _target = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You've taken damage from an explosion.");
        _target.GetComponent<HealthBehaviour>().TakeDamage(_damage);
    }


}
