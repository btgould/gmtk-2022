using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    [SerializeField] private Vector2[] waypoints;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float moveSpeed = 0.2f;
    [SerializeField] private float eps = 1e-4f;

    private int nextPoint = 0;
    private bool pathFinished = false;
    private bool started = false;

    void FixedUpdate()
    {
        if (!started) return;

        // Check if path is finished
        if (nextPoint >= waypoints.Length)
        {
            pathFinished = true;
            return;
        }

        // Look and move towards next waypoint
        Vector2 waypoint = waypoints[nextPoint];
        // body.transform.LookAt(waypoint); // TODO: fix rotation
        // transform.Rotate(Vector3.right * 90);

        Vector2 offset = waypoint - body.position;
        float offMag = offset.magnitude;
        Vector2 toMove = offMag < eps ? Vector2.zero : offset / offMag * moveSpeed;

        // make sure we don't overshoot
        if (offMag < toMove.magnitude) toMove = offset;
        body.transform.Translate(toMove);


        // Check if we have reached current waypoint
        float dist = offset.sqrMagnitude;
        if (dist < eps)
        {
            nextPoint++;
        }
    }

    // Interface
    public void setWaypoints(Vector2[] wp)
    {
        waypoints = wp;
        nextPoint = 0;
    }

    public bool isPathFinished()
    {
        return pathFinished;
    }

    public void start()
    {
        started = true;
    }
}
