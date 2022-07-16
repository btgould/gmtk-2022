using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatedPathManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private Vector2[] path;
    private List<(GameObject, PathFollow)> objects = new List<(GameObject, PathFollow)>();

    void Awake()
    {
        // TODO: prespawn ants
        spawner.SetSpawnPoint(path[0]);
    }

    void Update()
    {
        // Check if new object has been spawned yet
        if (spawner.isNewObject())
        {
            GameObject newObj = spawner.getNewObject();
            PathFollow pf = newObj.GetComponent<PathFollow>();
            pf.setWaypoints(path);
            pf.start();
            objects.Add((newObj, pf));
        }


        // Check if object has reached end of path and should be deleted
        GameObject objRemove = null;
        PathFollow pfRemove = null;

        foreach ((GameObject obj, PathFollow pf) in objects)
        {
            if (pf.isPathFinished())
            {
                objRemove = obj;
                pfRemove = pf;
                break;
            }
        }

        if (objRemove != null)
        {
            spawner.removeObject(objRemove);
            objects.Remove((objRemove, pfRemove));
            Destroy(objRemove);
        }
    }
}
