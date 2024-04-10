using System.Collections.Generic;
using UnityEngine;

namespace RPG.Input.Controller
{
    public class ControllerChanger : MonoBehaviour
    {
        int mIndex = 0;
        Controller mController;
        public Controller Controller
        {
            private get
            {
                return mController;
            }
            set
            {
                mController = value;
            }
        }
        List<IControllableObject> mControllables = new List<IControllableObject>();

        public void SetControllables(IControllableObject selected = null)
        {
            mControllables.Clear();
            MonoBehaviour obj;
            var objs = FindObjectsOfType<MonoBehaviour>();
            for (int i = 0, selectedIndex = 0; i < objs.Length; i++)
            {
                obj = objs[i];
                if (obj is IControllableObject)
                {
                    mControllables.Add(obj as IControllableObject);
                    if (selected.Equals(obj))
                    {
                        mIndex = selectedIndex;
                    }
                    selectedIndex++;
                }
            }
        }
        void Update()
        {
            if (mController==null || !mController.Active) return;
            if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
            {
                mControllables[mIndex++].ReleaseControlledTarget();

                // 루프 타일링
                mIndex = mIndex == mControllables.Count ? 0 :
                                                          mIndex;
                //

                mController.SetControlledTarget(mControllables[mIndex]);
            }
        }
    }
}

