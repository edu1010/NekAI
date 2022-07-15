using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int poolSize;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private bool expandPool;

    private readonly List<GameObject> _objectPool = new List<GameObject>();

    public void Start()
    {
        foreach (var i in Enumerable.Range(0, poolSize)) AddToPool();
    }

    public GameObject GetFromPool()
    {
        var obj = _objectPool.Find(o => !o.activeInHierarchy);
        return obj ? obj : expandPool ? AddToPool() : null;
    }

    private GameObject AddToPool()
    {
        var obj = Instantiate(objectToPool);
        obj.SetActive(false);
        _objectPool.Add(obj);
        return obj;
    }
}