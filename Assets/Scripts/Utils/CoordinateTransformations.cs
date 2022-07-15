using UnityEngine;

namespace Utils
{
    class CoordinateTransformations
    {
        public static Vector3 screenToWorldPos(Vector3 screenPos)
        {
            float zOffset = -Camera.main.transform.position.z; // Ensures that the final 3D point has z=0
            screenPos.z = zOffset;

            return Camera.main.ScreenToWorldPoint(screenPos);
        }
    }
}
