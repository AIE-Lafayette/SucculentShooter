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

    public GameObject GetObject(GameObject objectReference)
    {
        GameObject obj = null;

        for (int i = 0; i < _objectPools.Count; i++)
        {
            if (!_objectPools[i].Contains(gameObject))
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

        return obj;
    }

    public GameObject GetObject(GameObject objectReference, Vector3 position, Quaternion rotation)
    {
        GameObject obj = null;

        for (int i = 0; i < _objectPools.Count; i++)
        {
            if (!_objectPools[i].Contains(gameObject))
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

        return obj;
    }

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

    private GameObject CreateNewObject(GameObject objectReference)
    {
        GameObject obj = Instantiate(objectReference);
        obj.name = objectReference.name;

        Pool pool = new Pool(obj);
        _objectPools.Add(pool);

        return obj;
    }

    private GameObject CreateNewObject(GameObject objectReference, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Instantiate(objectReference, position, rotation);
        obj.name = objectReference.name;

        Pool pool = new Pool(obj);
        _objectPools.Add(pool);

        return obj;
    }

}
