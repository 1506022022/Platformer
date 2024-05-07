using PlatformGame.Character.Movement;
using PlatformGame.Contents.Loader;
using PlatformGame.Input;

namespace PlatformGame.Character.Controller
{
    public class CubeController : InputEvent
    {
        public WorkState State { get; private set; }
        RotateCube mRotateAbility;

        public CubeController(RotateCube rotateAbility) : base()
        {
            mRotateAbility = rotateAbility;
            //AddInputInteraction(mRotateAbility);
        }

        public override void Update()
        {
            base.Update();
            //if (Input.GetAxisRaw(GUARD) == 1)
            //{
            //    State = WorkState.Ready;
            //}
        }

        public void Start()
        {
            State = WorkState.Action;
        }
    }
}