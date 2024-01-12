using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private string _name;
    private List<GameObject> _objects;

    public string Name;
    public int Count { get { return _objects.Count; } }

    Pool(GameObject gameObject)
    {
        _objects = new List<GameObject>();
        _name = gameObject.name;

    }

    public bool Contains(GameObject objectInstance)
    {
        return _objects.Contains(objectInstance);
    }

    public GameObject GetObject()
    {
        GameObject obj = _objects[_objects.Count];
        _objects.Remove(_objects[_objects.Count]);

        return obj;
    }

    public void ReturnObject(GameObject gameObject)
    {
        _objects.Add(gameObject);
    }
}
