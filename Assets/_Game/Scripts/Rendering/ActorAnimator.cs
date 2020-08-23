namespace TakeCare
{
    using Spineless;
    using UnityEngine;


    public partial class ActorAnimator : MonoBehaviour
    {
        public const string IsBlessing = "IsBlessing";

        public const string IsHappy = "IsHappy";
        public const string IsIncapacitated = "IsIncapacitated";

        public const string HorizontalVelocity = "HorizontalVelocity";
        public const string HorizontalSpeed = "HorizontalSpeed";
        public const string VerticalSpeed = "VerticalSpeed";
        public const string VerticalVelocity = "VerticalVelocity";

        public Animator Animator;
        public SpriteRenderer SpriteRenderer;

        private IMovable movable;


        public void Awake()
        {
            this.movable = GetComponent<IMovable>();
        }


        public void Update()
        {
            UpdateMovementParameters();
        }


        private void UpdateMovementParameters()
        {
            this.Animator.SetFloat(HorizontalSpeed, this.movable.CurrentSpeed.x);
            this.Animator.SetFloat(HorizontalVelocity, this.movable.CurrentVelocity.x);
            this.Animator.SetFloat(VerticalSpeed, this.movable.CurrentSpeed.y);
            this.Animator.SetFloat(VerticalVelocity, this.movable.CurrentVelocity.y);

            if (this.movable.CurrentVelocity.x < 0)
                this.SpriteRenderer.flipX = true;
            else if (this.movable.CurrentVelocity.x > 0)
                this.SpriteRenderer.flipX = false;
        }
    }
}

#if UNITY_EDITOR
namespace TakeCare
{
    using System.Linq;
    using UnityEditor;
    using UnityEditor.Animations;
    using UnityEngine;


    public partial class ActorAnimator
    {
        [CustomEditor(typeof(ActorAnimator))]
        public class ActorAnimatorEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("Populate Animation Parameters"))
                {
                    ActorAnimator actorAnimator = this.target as ActorAnimator;
                    AnimatorController controller =
                        (AnimatorController)actorAnimator.GetComponentInChildren<Animator>(true).runtimeAnimatorController;

                    // Movement
                    AddFloatParameter(controller, HorizontalSpeed);
                    AddFloatParameter(controller, HorizontalVelocity);
                    AddFloatParameter(controller, VerticalSpeed);
                    AddFloatParameter(controller, VerticalVelocity);
                }
            }


            private void AddBoolParameter(AnimatorController controller, string parameterName)
            {
                if (HasParameter(controller, parameterName))
                    return;

                AnimatorControllerParameter parameter = new AnimatorControllerParameter();
                parameter.type = AnimatorControllerParameterType.Bool;
                parameter.name = parameterName;
                controller.AddParameter(parameter);
            }


            private void AddFloatParameter(AnimatorController controller, string parameterName)
            {
                if (HasParameter(controller, parameterName))
                    return;

                AnimatorControllerParameter parameter = new AnimatorControllerParameter();
                parameter.type = AnimatorControllerParameterType.Float;
                parameter.name = parameterName;
                controller.AddParameter(parameter);
            }


            private bool HasParameter(AnimatorController controller, string parameterName)
            {
                return controller.parameters.Any(parameter => parameter.name == parameterName);
            }
        }
    }
}
#endif