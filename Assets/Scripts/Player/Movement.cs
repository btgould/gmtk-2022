using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;

public class Movement : MonoBehaviour
{
    [SerializeField] private float launchMag = 10.0f;
    [SerializeField] private float maxLaunchDist = 2.0f;
    [SerializeField] private float launchStartLen = 1.5f;

    [SerializeField] private Rigidbody2D body;
    [SerializeField] private LineRenderer lr;

    private bool jumpDown = false;

    // Start is called before the first frame update
    void Start()
    {
        lr.positionCount = 0;
        lr.SetPositions(new Vector3[0]);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = CoordinateTransformations.screenToWorldPos(Input.mousePosition);

        // Check for jump start
        if (Input.GetMouseButtonDown(0))
        {
            float mouseDist = (body.position - mousePos).magnitude;

            if (mouseDist < launchStartLen)
            {
                jumpDown = true;
                lr.positionCount = 2;
                lr.SetPositions(new Vector3[] { mousePos, body.position });
            }
        }

        // Update linerenderer
        if (jumpDown)
        {
            Vector2 offset = mousePos - body.position;
            float offsetMag = offset.magnitude;
            offsetMag = Mathf.Min(offsetMag, maxLaunchDist);
            offset = offset.normalized * offsetMag;

            lr.SetPosition(0, body.position);
            lr.SetPosition(1, body.position + offset);
        }

        // Check for jump up
        if (Input.GetMouseButtonUp(0) && jumpDown)
        {
            // Get launch direction
            Vector2 launchDir = body.position - mousePos;

            // Clamp launch magnitude 
            float launchDist = launchDir.magnitude;
            launchDist = Mathf.Min(launchDist, maxLaunchDist);

            launch(launchDir, launchDist);
        }
    }

    private void launch(Vector2 direction, float distance = 1.0f)
    {
        body.AddForce(launchMag * distance * direction.normalized, ForceMode2D.Impulse);
        jumpDown = false;

        lr.positionCount = 0;
        lr.SetPositions(new Vector3[0]);
    }
}