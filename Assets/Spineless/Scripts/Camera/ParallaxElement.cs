namespace Spineless
{
    using UnityEngine;


    [ExecuteInEditMode]
    public class ParallaxElement : MonoBehaviour
    {
        [SerializeField]
        private float horizontalSpeed;

        [SerializeField]
        private float verticalSpeed;

        [SerializeField]
        private bool moveInOppositeDirection;

        private Vector3 previousCameraPosition;
        private Camera mainCamera;
        private Transform cameraTransform;


        public void OnEnable()
        {
            this.mainCamera = Camera.main;
            this.cameraTransform = this.mainCamera.transform;
            this.previousCameraPosition = this.cameraTransform.position;
        }


        public void Update()
        {
            this.previousCameraPosition = this.cameraTransform.position;

            Vector3 distance = this.cameraTransform.position - this.previousCameraPosition;
            float direction = (this.moveInOppositeDirection)
                                  ? -1f
                                  : 1f;
            this.transform.position +=
                Vector3.Scale(
                    distance,
                    new Vector3(this.horizontalSpeed, this.verticalSpeed)) * direction;

            this.previousCameraPosition = this.cameraTransform.position;
        }
    }
}