namespace TakeCare
{
    using Spineless;
    using UnityEngine;
    

    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private bool isLocked;

        [SerializeField]
        private bool isMovementLocked;

        private IMovable movement;


        #region Properties
        public bool IsLocked
        {
            get { return this.isLocked; }
        }
        #endregion


        public void Awake()
        {
            this.movement = GetComponent<IMovable>();
        }


        public void Lock()
        {
            this.isLocked = true;
        }


        public void LockMovement()
        {
            this.isMovementLocked = true;
        }


        public void Move(Vector2 direction)
        {
            this.movement.MoveDirection = direction;
            this.movement.Move();
        }


        public void Unlock()
        {
            this.isLocked = false;
        }


        public void UnlockMovement()
        {
            this.isMovementLocked = false;
        }


        public void Update()
        {
            if (this.isLocked
                || GameTime.IsPaused)
                return;

            if (this.isMovementLocked)
                return;

            float horizontalInput = Input.GetAxisRaw(Controls.HorizontalAxis);
            float verticalInput = Input.GetAxisRaw(Controls.VerticalAxis);
            float mouseWheelInput = Input.GetAxisRaw(Controls.MouseWheelAxis);

            Move(new Vector2(horizontalInput, verticalInput));

            if (mouseWheelInput > 0)
            {
                // TODO
            }

            else if (mouseWheelInput < 0)
            {
                // TODO
            }
        }
    }
}