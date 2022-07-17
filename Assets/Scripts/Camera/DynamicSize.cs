using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DynamicSize : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private CinemachineVirtualCamera vCam;

    private uint framesWaited = 0;

    [SerializeField] private float minCamSize;
    [SerializeField] private float maxCamSize;
    [SerializeField] private float minSizeSpeed, maxSizeSpeed;
    [SerializeField] private float easing;
    [SerializeField] private uint delayFrames;
    private int numAvg = 20;

    private int idx = 0;
    private float[] pastSpeeds;

    // Start is called before the first frame update
    void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        if (!vCam) Debug.LogError("No virtual camera component found!");

        GameObject playerObj = vCam.Follow.gameObject;
        playerRB = playerObj.GetComponent<Rigidbody2D>();

        vCam.m_Lens.OrthographicSize = minCamSize;

        pastSpeeds = new float[numAvg];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check if player exists
        if (playerRB == null)
        {
            return;
        }

        // Add player speed to list
        pastSpeeds[idx] = playerRB.velocity.magnitude;
        idx++;
        idx %= numAvg;


        // Calculate avg speed relative to min / max
        float vel = 0;
        for (int i = 0; i < numAvg; i++)
        {
            vel += pastSpeeds[i];
        }
        vel /= numAvg;
        float t = (vel - minSizeSpeed) / (maxSizeSpeed - minSizeSpeed);
        t = Mathf.Clamp(t, 0, 1);

        // Wait to change zoom (so that sudden changes aren't so jarring)
        if (t > 0)
        {
            framesWaited++;
        }
        else
        {
            framesWaited = 0;
        }

        // Set camera size based on speed thresholds
        float newSize = Mathf.Lerp(minCamSize, maxCamSize, t);
        // newSize = Mathf.Lerp(minCamSize, newSize, t);
        float oldSize = vCam.m_Lens.OrthographicSize;
        // if (!player.checkGrounded())
        // {
        //     newSize = Mathf.Max(newSize, oldSize); // don't shrink camera until player lands
        // }
        if (framesWaited < delayFrames)
        {
            newSize = Mathf.Min(newSize, oldSize); // don't grow camera too suddenly
        }
        vCam.m_Lens.OrthographicSize = Mathf.Lerp(oldSize, newSize, easing);

    }
}
