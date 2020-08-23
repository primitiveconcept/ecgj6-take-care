namespace Spineless
{
    using UnityEngine;


    public class CameraFollow : MonoBehaviour
    {
        public Transform FollowTarget;
        public float SmoothTime = 0.3F;
        
        private Vector3 velocity = Vector3.zero;


        public void Update()
        {
            Vector3 targetPosition = this.FollowTarget.TransformPoint(new Vector3(0, 0, -10));
            this.transform.position = Vector3.SmoothDamp(
                current: this.transform.position, 
                target: targetPosition, 
                currentVelocity: ref velocity, 
                smoothTime: this.SmoothTime);
        }
    }
}