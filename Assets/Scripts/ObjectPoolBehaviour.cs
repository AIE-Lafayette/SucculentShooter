using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolBehaviour : MonoBehaviour
{

    private List<Pool> _objectPools = new List<Pool>();

    private static ObjectPoolBehaviour _instance;
    public static ObjectPoolBehaviour Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            _instance = FindObjectOfType<ObjectPoolBehaviour>();
            if (_instance != null)
            {
                return _instance;
            }
            GameObject objectPool = new GameObject("ObjectPool");
            _instance = objectPool.AddComponent<ObjectPoolBehaviour>();
            return _instance;
        }
    }


    /// <summary>
    /// Returns the pool that contains the given game object,
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public Pool GetPool(GameObject gameObject)
    {
        for (int i = 0; i < _objectPools.Count; i++)
        {
            if(_objectPools[i].Name == gameObject.name)
            {
                return _objectPools[i];
            }
        }

        return null;
    }

    /// <summary>
    /// Gets an object that matches the reference from the pool and makes it active in the scene.
    /// If an instance of that game object does not exist in the scene, it instantiates one.
    /// </summary>
    /// <param name="objectReference"></param>
    /// <returns></returns>
    public GameObject GetObject(GameObject objectReference)
    {
        GameObject obj = null;

        for (int i = 0; i < _objectPools.Count; i++)
        {
            if (_objectPools[i].Name != (objectReference.name))
            {
                continue;
            }
            obj = _objectPools[i].GetObject();
            break;
        }
        if (obj == null)
        {
            obj = CreateNewObject(objectReference);
        }
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// Gets an object that matches the reference from the pool and makes it active in the scene.
    /// If an instance of that game object does not exist in the scene, it instantiates one.
    /// Moves the object to the given position and sets its rotation to match the given rotation.
    /// </summary>
    /// <param name="objectReference"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject GetObject(GameObject objectReference, Vector3 position, Quaternion rotation)
    {
        GameObject obj = null;

        for (int i = 0; i < _objectPools.Count; i++)
        {
            if (_objectPools[i].Name != (objectReference.name))
            {
                continue;
            }
            obj = _objectPools[i].GetObject();
            break;
        }
        if (obj == null)
        {
            obj = CreateNewObject(objectReference, position, rotation);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;

        obj.SetActive(true);

        return obj;
    }

    /// <summary>
    /// Places an object back into the pool and deactivates it in the scene.
    /// </summary>
    /// <param name="objectInstance"></param>
    public void ReturnObject(GameObject objectInstance)
    {
        Pool temp;

        temp = GetPool(objectInstance);
        if(temp == null)
        {
            return;
        }

        temp.ReturnObject(objectInstance);
        objectInstance.SetActive(false);
    }

    /// <summary>
    /// Instantiates the given object and changes its name to match the name of the objectReference.
    /// </summary>
    /// <param name="objectReference"></param>
    /// <returns></returns>
    private GameObject CreateNewObject(GameObject objectReference)
    {
        GameObject obj = Instantiate(objectReference);
        obj.name = objectReference.name;

        Pool pool = new Pool(obj);
        _objectPools.Add(pool);

        return obj;
    }

    /// <summary>
    /// Instantiates the given object with the given position and rotation. It also changes its name to match the name of the objectReference.
    /// </summary>
    /// <param name="objectReference"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    private GameObject CreateNewObject(GameObject objectReference, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Instantiate(objectReference, position, rotation);
        obj.name = objectReference.name;

        Pool pool = new Pool(obj);
        _objectPools.Add(pool);

        return obj;
    }

}
