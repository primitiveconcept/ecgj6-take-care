namespace Spineless
{
    using System;
    using UnityEngine;


    public interface IMovable
    {
        event Action StartedMoving;
        event Action StoppedMoving;


        #region Properties
        Vector2 AdditionalVelocity { get; set; }
        Vector2 CurrentSpeed { get; }
        Vector2 CurrentVelocity { get; }
        Vector2 MoveDirection { get; set; }

        Rigidbody2D Rigidbody2D { get; }
        float Speed { get; set; }
        #endregion


        void Move();
    }


    public static class MovableExtensions
    {
        public static Vector2 ClampToDirection(this Vector2 vector2)
        {
            return new Vector2(
                Mathf.Clamp(vector2.x, -1, 1),
                Mathf.Clamp(vector2.y, -1, 1));
        }


        public static Vector2 ClampToIntegerDirection(this Vector2 vector2)
        {
            return new Vector2(
                Mathf.Clamp((int)vector2.x, -1, 1),
                Mathf.Clamp((int)vector2.y, -1, 1));
        }


        public static Rigidbody2D SetupRigidbody(this GameObject gameObject, bool overwrite = false)
        {
            Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            bool lackedRigidbody = rigidbody2D == null;

            if (lackedRigidbody)
                rigidbody2D = gameObject.AddComponent<Rigidbody2D>();

            if (lackedRigidbody
                || overwrite)
            {
                rigidbody2D.gravityScale = 0;
                rigidbody2D.isKinematic = false;
                rigidbody2D.mass = 5;
                rigidbody2D.angularDrag = 0;
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
			
            return rigidbody2D;
        }
    }
}