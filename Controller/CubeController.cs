using Platformer;
using RPG.Input.Controller;
using UnityEngine;
using static RPG.Input.ActionKey;

public class CubeController : Controller
{
    public AbilityState State { get; private set; }
    RotateAbility mRotateAbility;
    public CubeController(RotateAbility rotateAbility) : base()
    {
        mRotateAbility = rotateAbility;
        AddInputInteraction(mRotateAbility);
    }
    public override void Update()
    {
        base.Update();
        if(Input.GetAxisRaw(GUARD) == 1)
        {
            State = AbilityState.Ready;
        }
    }
    public void Start()
    {
        State = AbilityState.Action;
    }
}

