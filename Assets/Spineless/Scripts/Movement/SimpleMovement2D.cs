namespace Spineless
{
    using UnityEngine;


    public class SimpleMovement2D : MonoBehaviour,
                                    IMovable
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private Vector2 moveDirection;

        [SerializeField]
        private Vector2 additionalVelocity;

        [SerializeField]
        private bool autoMove;

        private new Rigidbody2D rigidbody2D;
        private bool shouldMove;


        #region Properties
        public Vector2 AdditionalVelocity
        {
            get { return this.additionalVelocity; }
            set { this.additionalVelocity = value; }
        }


        public bool AutoMove
        {
            get { return this.autoMove; }
            set { this.autoMove = value; }
        }


        public Vector2 MoveDirection
        {
            get { return this.moveDirection; }
            set { this.moveDirection = value; }
        }


        public Rigidbody2D Rigidbody2D
        {
            get { return this.rigidbody2D; }
        }


        public float Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
        #endregion


        public void Awake()
        {
            this.rigidbody2D = this.gameObject.SetupRigidbody();
        }


        public void FixedUpdate()
        {
            this.rigidbody2D.velocity = Vector2.zero;

            if (GameTime.IsPaused)
                return;

            if (!this.shouldMove)
                return;

            this.shouldMove = false;

            Vector2 velocity = new Vector2(
                                   this.moveDirection.x,
                                   this.moveDirection.y)
                               * this.speed
                               + this.additionalVelocity;
            this.rigidbody2D.velocity = velocity;
        }


        public void Move()
        {
            this.shouldMove = true;
        }


        public void OnValidate()
        {
            this.moveDirection = this.moveDirection.ClampToDirection();
        }


        public void Update()
        {
            if (this.autoMove)
                Move();
        }
    }
}