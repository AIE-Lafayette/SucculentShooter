using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerTest : MonoBehaviour
{
    public GameObject _thingToSpawn;
    private GameObject _thingInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Mouse Button was pressed");
            _thingInstance = ObjectPoolBehaviour.Instance.GetObject(_thingToSpawn);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Mouse Button was pressed");
            ObjectPoolBehaviour.Instance.ReturnObject(_thingInstance);
        }

    }
}
