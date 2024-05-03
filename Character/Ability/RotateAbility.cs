using Platformer;
using RPG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static RPG.Input.ActionKey;

public class RotateAbility : Ability
{
    Vector3 forward;
    Vector3 right;
    Vector3 up;
    Vector3 FixedRightAxis;
    Vector3 FixedUpAxis;
    Vector3[] mDirArr = new Vector3[3];
    bool mbRotating = false;
    float mRotationAmount = 90f;
    float mRotationSpeed = 1f;

    protected override void UpdateAbilityState()
    {
        mAbilityState = mbRotating ? AbilityState.Action : AbilityState.Ready;
    }
    protected override void MappingInputEvent()
    {
        InputEventMap = new Dictionary<string, UnityAction<float>>()
        {
            { VERTICAL, RotateVertical },
            { HORIZONTAL, RotateHorizontal }
        };
    }
    void RotateHorizontal(float dir)
    {
        UpdateFixedAxis();
        mRotationAmount = Mathf.Abs(mRotationAmount) * dir;
        StartCoroutine(RotateObject(FixedUpAxis));
    }
    void RotateVertical(float dir)
    {
        UpdateFixedAxis();
        mRotationAmount = Mathf.Abs(mRotationAmount) * dir;
        StartCoroutine(RotateObject(FixedRightAxis));
    }
    void UpdateFixedAxis()
    {
        up = Temping(transform.up);
        right = Temping(transform.right);
        forward = Temping(transform.forward);

        mDirArr[0] = up;
        mDirArr[1] = right;
        mDirArr[2] = forward;

        foreach (var dir in mDirArr)
        {
            if (dir.x != 0)
            {
                FixedRightAxis = dir;
                break;
            }
            if (dir.y != 0)
            {
                FixedUpAxis = dir;
            }
        }
    }
    Vector3 Temping(Vector3 vector)
    {
        var max = Mathf.Max(vector.x * vector.x, vector.y * vector.y, vector.z * vector.z);

        if (max == vector.x * vector.x)
        {
            vector = Vector3.right * (vector.x > 0 ? 1 : -1);
        }
        else if (max == vector.y * vector.y)
        {
            vector = Vector3.up * (vector.y > 0 ? 1 : -1);
        }
        else
        {
            vector = Vector3.forward * (vector.z > 0 ? 1 : -1); ;
        }
        return vector;
    }
    IEnumerator RotateObject(Vector3 axis)
    {
        float time = 0f;
        mbRotating = true;
        var startRotation = transform.rotation;
        var endRotation = Quaternion.AngleAxis(mRotationAmount, axis) * startRotation;

        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            time += Time.deltaTime * mRotationSpeed;
            yield return null;
        }
        transform.rotation = endRotation;
        mbRotating = false;
    }
}

