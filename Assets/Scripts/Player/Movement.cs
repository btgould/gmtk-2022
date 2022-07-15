using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;

public class Movement : MonoBehaviour
{
    [SerializeField] private float jumpMag = 10.0f;
    [SerializeField] private float maxLaunchDist = 1.5f;

    [SerializeField] private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // Get launch direction
            Vector2 mousePos = CoordinateTransformations.screenToWorldPos(Input.mousePosition);
            Vector2 launchDir = body.position - mousePos;

            // Clamp launch magnitude 
            float launchDist = launchDir.magnitude;
            launchDist = Mathf.Min(launchDist, maxLaunchDist);

            launch(launchDir, launchDist);
        }
    }

    private void launch(Vector2 direction, float distance = 1.0f)
    {
        body.AddForce(jumpMag * distance * direction.normalized, ForceMode2D.Impulse);
    }
}
