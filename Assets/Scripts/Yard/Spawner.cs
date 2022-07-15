using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnable;
    [SerializeField] private uint maxObjs = 20;
    private List<GameObject> objects = null;
    private bool newObject = false;
    private GameObject mostRecentSpawn = null;

    [SerializeField] private Vector3 spawnPos = Vector3.zero;
    [SerializeField] private float firstSpawnDelay = 0.0f;
    [SerializeField] private float spawnInterval = 0.5f;

    void Awake()
    {
        objects = new List<GameObject>();
        InvokeRepeating("trySpawn", firstSpawnDelay, spawnInterval);
    }

    private void trySpawn()
    {
        if (objects != null && objects.Count >= maxObjs && maxObjs > 0) return;

        GameObject spawned = Instantiate(spawnable, spawnPos, Quaternion.identity);
        mostRecentSpawn = spawned;
        newObject = true;

        if (objects != null) objects.Add(spawned);
    }

    // Interface
    public bool isNewObject() { return newObject; }
    public GameObject getNewObject()
    {
        newObject = false;
        return mostRecentSpawn;
    }

    public void removeObject(GameObject obj)
    {
        objects.Remove(obj);
    }
}
