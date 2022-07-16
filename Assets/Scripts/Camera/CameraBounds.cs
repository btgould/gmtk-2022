using UnityEngine;
using Cinemachine;

public class CameraBounds : CinemachineExtension
{
    [SerializeField] private GameObject background;

    private Vector2 minPos;
    private Vector2 maxPos;
    private SpriteRenderer bgSprite;

    void Start()
    {
        bgSprite = background.GetComponent<SpriteRenderer>();
        minPos = bgSprite.transform.position - bgSprite.bounds.extents;
        maxPos = bgSprite.transform.position + bgSprite.bounds.extents;
    }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            float aspect = Camera.main.aspect;
            float vOff = Camera.main.orthographicSize;
            float hOff = vOff * aspect;

            pos.x = Mathf.Clamp(pos.x, minPos.x + hOff, maxPos.x - hOff);
            pos.y = Mathf.Clamp(pos.y, minPos.y + vOff, maxPos.y - vOff);
            state.RawPosition = pos;
        }
    }
}
