namespace Spineless
{
    using System;
    using UnityEngine;


    public class SineMovement2D : MonoBehaviour,
                                  IMovable
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private Vector2 moveDirection;

        [SerializeField]
        private Vector2 additionalVelocity;

        [SerializeField]
        private Vector2 sineFrequency;

        [SerializeField]
        private Vector2 sineAmplitude;

        [SerializeField]
        private bool autoMove;

        private new Rigidbody2D rigidbody2D;
        private bool shouldMove;
        public event Action StartedMoving;
        public event Action StoppedMoving;


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


        public Vector2 CurrentSpeed { get; set; }
        public Vector2 CurrentVelocity { get; set; }


        public Vector2 MoveDirection
        {
            get { return this.moveDirection; }
            set { this.moveDirection = value; }
        }


        public Rigidbody2D Rigidbody2D
        {
            get { return this.rigidbody2D; }
        }


        public Vector2 SineAmplitude
        {
            get { return this.sineAmplitude; }
            set { this.sineAmplitude = value; }
        }


        public Vector2 SineFrequency
        {
            get { return this.sineFrequency; }
            set { this.sineFrequency = value; }
        }


        public float Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
        #endregion


        public void Awake()
        {
            this.rigidbody2D = GetComponent<Rigidbody2D>();
        }


        public void FixedUpdate()
        {
            this.rigidbody2D.velocity = Vector2.zero;

            if (!this.shouldMove)
                return;

            this.shouldMove = false;

            if (GameTime.IsPaused)
                return;

            // Actually using cosine to center the wave on current axis.
            Vector2 sineMovement = new Vector2(
                Mathf.Cos((float)GameTime.TimeElapsed * this.sineFrequency.x) * this.sineAmplitude.x,
                Mathf.Cos((float)GameTime.TimeElapsed * this.sineFrequency.y) * this.sineAmplitude.y);

            Vector2 velocity = new Vector2(
                                   this.moveDirection.x + sineMovement.x,
                                   this.moveDirection.y + sineMovement.y)
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
            this.moveDirection = this.moveDirection.ClampToIntegerDirection();
        }


        public void Update()
        {
            if (this.autoMove)
                Move();
        }
    }
}