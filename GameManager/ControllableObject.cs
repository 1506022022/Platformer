using RPG.Input.Controller;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPG
{
    public class ControllableObject : IControllableObject
    {
        Camera mCam;
        Rigidbody mRigid;
        Func<bool> mIsControllable;
        UnityAction mOnBindControll;
        UnityAction mOnReleaseControll;
        float mMoveSpeed;
        public ControllableObject(float moveSpeed, Camera cam, Rigidbody rigid, Func<bool> isControllable, UnityAction OnBindControll, UnityAction OnReleaseControll)
        {
            mMoveSpeed = moveSpeed;
            mCam = cam;
            mRigid = rigid;
            mIsControllable = isControllable;
            mOnBindControll = OnBindControll;
            mOnReleaseControll = OnReleaseControll;
        }
        public Camera GetCamera()
        {
            return mCam;
        }
        public float GetMoveSpeed()
        {
            return mMoveSpeed;
        }
        public Rigidbody GetRigidbody()
        {
            return mRigid;
        }

        public bool IsControllable()
        {
            return mIsControllable.Invoke();
        }

        public void OnBindControll()
        {
            mOnBindControll.Invoke();
        }

        public void OnReleaseControll()
        {
            mOnReleaseControll.Invoke();
        }
    }
}
