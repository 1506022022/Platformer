using System.Collections.Generic;
using UnityEngine;

namespace RPG.Input.Controller
{
    public class CharacterSwappingSystem : Controller
    {
        int mIndex = 0;
        List<IControllableObject> mControllables = new List<IControllableObject>();

        public void SetControllables()
        {
            Debug.Assert(mControlledTarget != null);
            mControllables.Clear();
            MonoBehaviour obj;
            // TODO : IControllableObject 검색 부분 최적화 필요
            var objs = GameObject.FindObjectsOfType<MonoBehaviour>();
            // TODOEND
            for (int i = 0, selectedIndex = 0; i < objs.Length; i++)
            {
                obj = objs[i];
                if (obj is IControllableObject)
                {
                    mControllables.Add(obj as IControllableObject);
                    if (mControlledTarget.Equals(obj))
                    {
                        mIndex = selectedIndex;
                    }
                    selectedIndex++;
                }
            }
        }
        public override void Update()
        {
            base.Update();
            if (!Active) return;
            if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
            {
                mControllables[mIndex++].ReleaseControlledTarget();

                // 루프 타일링
                mIndex = mIndex == mControllables.Count ? 0 :
                                                          mIndex;
                //

                SetControlledTarget(mControllables[mIndex]);
            }
        }
    }
}

