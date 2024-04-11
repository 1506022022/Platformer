using RPG.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractalbeObject : MonoBehaviour, IInputInteraction
{
    public bool IsAble
    {
        get => mAble;
        set => mAble = value;
    }
    bool mAble;

    public Dictionary<string, UnityAction<float>> InputEventMap => throw new System.NotImplementedException();
}
