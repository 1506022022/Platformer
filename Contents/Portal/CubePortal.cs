using Platformer;
using Platformer.Contents;
using RPG;
using RPG.Contents;
using UnityEngine;
using static RPG.Input.ActionKey;

public class CubePortal : Portal
{
    [SerializeField] CubeLoader mLoader;
    float mLastStoped;
    protected override void RunPortal()
    {
        base.RunPortal();
        EnterCharacters();
        RunCube();
    }
    void Update()
    {
        if (State == Platformer.AbilityState.Action)
        {
            if (IsCubeActionComplete())
            {
                StopPortal();
            }
        }
        if(State == Platformer.AbilityState.Colltime)
        {
            if (mLastStoped + 3f < Time.time)
            {
                State = Platformer.AbilityState.Ready;
            }
        }
    }
    void StopPortal()
    {
        ExitsCharacters();
        State = Platformer.AbilityState.Colltime;
        mLastStoped = Time.time;
    }
    void RunCube()
    {
        Contents.Instance.SetLoader(mLoader);
        GameManager.Instance.LoadGame();
    }
    void EnterCharacters()
    {
        var characters = mGoalCheck.Keys;
        foreach (var character in characters)
        {
            character.gameObject.SetActive(false);
        }
    }
    void ExitsCharacters()
    {
        var characters = mGoalCheck.Keys;
        foreach (var character in characters)
        {
            character.gameObject.SetActive(true);
        }
    }
    bool IsCubeActionComplete()
    {
        return mLoader.State == Platformer.AbilityState.Colltime;
    }
}