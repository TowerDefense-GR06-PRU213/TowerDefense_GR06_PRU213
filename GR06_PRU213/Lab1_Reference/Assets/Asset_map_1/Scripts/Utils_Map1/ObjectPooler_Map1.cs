using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler_Map1 : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 5;
    private List<GameObject> _pool;

    void Start()
    {
        // create pool
        _pool = new List<GameObject>();
        
        // LAB 1: Skip if prefab is null
        if (prefab == null)
        {
            Debug.LogWarning($"ObjectPooler on {gameObject.name} has no prefab assigned. Skipping pool creation.");
            return;
        }
        
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        _pool.Add(obj);
        return obj;
    }

    public GameObject GetPooledObject()
    {
        // LAB 1: Return null if no prefab
        if (prefab == null || _pool == null)
        {
            return null;
        }
        
        foreach (GameObject obj in _pool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }
        return CreateNewObject();
    }

}
