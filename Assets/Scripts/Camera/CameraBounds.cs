using UnityEngine;
using Cinemachine;

public class CameraBounds : CinemachineExtension
{
    [SerializeField] private Vector2 minPos = new Vector2(-10, -10);
    [SerializeField] private Vector2 maxPos = new Vector2(10, 10);

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            pos.x = Mathf.Clamp(pos.x, minPos.x, maxPos.x);
            pos.y = Mathf.Clamp(pos.y, minPos.y, maxPos.y);
            state.RawPosition = pos;
        }
    }
}
