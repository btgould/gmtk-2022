using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatedPathManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    // [SerializeField] private Path path; // TODO: figure out how to handle paths
    private List<GameObject> objects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.isNewObject())
        {
            GameObject newObj = spawner.getNewObject();
            objects.Add(newObj);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject obj in objects)
            {
                spawner.removeObject(obj);
                Destroy(obj);
            }

            objects.Clear();
        }
    }
}
