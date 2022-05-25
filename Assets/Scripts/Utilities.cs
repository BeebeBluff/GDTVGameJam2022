using UnityEngine;

namespace Assets.Scripts
{
    public static class Utilities
    {
        /// <summary>
        /// Get the <c>Quaternion</c> for the rotation needed to look at the specified
        /// target position from the specified current position.
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public static Quaternion LookAt(Vector3 currentPosition, Vector3 targetPosition)
        {
            Vector3 dir = (targetPosition - currentPosition).normalized;
            return Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        }

        public static Quaternion SlerpLookAt(Vector3 currentPosition, Vector3 targetPosition,
            Quaternion currentRotation, float slerpRatio)
        {
            Vector3 dir = (targetPosition - currentPosition).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            return Quaternion.Slerp(currentRotation, lookRotation, slerpRatio);
        }
    }
}
