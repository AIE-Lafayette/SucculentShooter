using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    /// <summary>
    /// The name of the game object that this pool will store.
    /// </summary>
    private string _name;
    private List<GameObject> _objects;

    public int Count { get { return _objects.Count; } }

    public string Name { get => _name; private set => _name = value; }

    /// <summary>
    /// Initializes the list and sets the name to be the game object's name.
    /// </summary>
    /// <param name="gameObject"></param>
    public Pool(GameObject gameObject)
    {
        _objects = new List<GameObject>();
        Name = gameObject.name;

    }

    /// <summary>
    /// Returns whether or not this object instance is in the pool.
    /// </summary>
    /// <param name="objectInstance"></param>
    /// <returns></returns>
    public bool Contains(GameObject objectInstance)
    {
        return _objects.Contains(objectInstance);
    }

    /// <summary>
    /// Finds the last object added to the pool and removes it from the pool.
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        if (_objects.Count == 0)
        {
            return null;
        }
        GameObject obj = _objects[_objects.Count-1];
        _objects.Remove(_objects[_objects.Count-1]);

        return obj;
    }

    /// <summary>
    /// Adds an item back into the pool.
    /// </summary>
    /// <param name="gameObject"></param>
    public void ReturnObject(GameObject gameObject)
    {
        _objects.Add(gameObject);
    }
}
