using PlatformGame.Character.Collision;
using UnityEngine;

namespace PlatformGame.Character
{
    public class StickyComponent : MonoBehaviour
    {
        static readonly Vector3[] mDirs =
        {
            Vector3.up,
            Vector3.forward,
            Vector3.right,
            Vector3.back,
            Vector3.left,
            Vector3.down
        };
        StickyComponent mRoot;
        StickyComponent mPrev;
        StickyComponent mNext;
        bool mbStuck;
        public bool IsStuck => mbStuck;
        int mCurrentDir;
        int CurrentDir
        {
            get => mCurrentDir;
            set => mCurrentDir = Mathf.Clamp(value < mDirs.Length ? value : 0, 0, mDirs.Length - 1);
        }
        Character mCharacter;

        public void DetachFromOther()
        {
            if (!mbStuck)
            {
                return;
            }

            if (mPrev == mRoot)
            {
                mRoot.DetachFromOther();
            }

            mPrev = null;
            mNext = null;
            mRoot = null;
            mbStuck = false;

            mCharacter.Rigid.isKinematic = !mCharacter.Attribute.IsInclude(AttributeFlags.NonStatic);
        }

        public void StickAround()
        {
            Vector3 pos = Vector3.zero;
            for (int i = 0; i < mDirs.Length; i++)
            {
                if (pos != Vector3.zero)
                {
                    break;
                }

                CurrentDir++;
                pos = FindStickyPos(mDirs[CurrentDir]);
            }

            if (pos == Vector3.zero)
            {
                return;
            }

            mPrev.mRoot = mPrev.mRoot ? mPrev.mRoot : mPrev;
            mPrev.mNext = this;
            mPrev.mbStuck = true;

            mRoot = mPrev.mRoot;
            mbStuck = true;

            transform.position = pos;
            mCharacter.Rigid.isKinematic = true;
            mPrev.mCharacter.Rigid.isKinematic = true;
        }

        Vector3 FindStickyPos(Vector3 dir)
        {
            Vector3 near = Vector3.zero;
            Vector3 one, three;

            var hitTarget = FindHandShakeTarget(transform.position, transform, dir);
            if (hitTarget == null)
            {
                return near;
            }

            var hitMe = FindHandShakeTarget(hitTarget.Value.point, hitTarget.Value.transform, -dir);
            if (hitMe == null)
            {
                return near;
            }

            mPrev = hitTarget.Value.transform.GetComponent<StickyComponent>();
            if (mPrev.mNext != null)
            {
                mPrev = null;
                return near;
            }

#if DEVELOPMENT
            line.SetPosition(0, hitMe.Value.point);
            line.SetPosition(1, hitTarget.Value.point);
#endif

            one = mPrev.transform.position - transform.position;
            three = mPrev.transform.position - hitMe.Value.point;
            near = hitTarget.Value.point - (one - three);
            return near;
        }

        RaycastHit? FindHandShakeTarget(Vector3 offset, Transform A, Vector3 dir)
        {
            RaycastHit hit;
            if (!Physics.Raycast(offset, dir, out hit, GameData.BLOCK_SIZE))
            {
                return null;
            }

            var character = hit.transform.GetComponent<Character>();
            if (character == null || character.transform == A)
            {
                return null;
            }

            if (!character.Attribute.IsInclude(AttributeFlags.Stickiness))
            {
                return null;
            }
            return hit;
        }

        void Awake()
        {
            mCharacter = GetComponent<Character>();

#if DEVELOPMENT
            if (line == null)
            {
                line = gameObject.AddComponent<LineRenderer>();
                line.positionCount = 2;

                line.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
                line.widthMultiplier = 0.1f;
                line.startColor = Color.yellow;
                line.endColor = Color.yellow;
            }
#endif
        }

#if DEVELOPMENT
        LineRenderer line;
        void Update()
        {
            line.enabled = false;

            if (IsStuck)
            {
                return;
            }

            int d = CurrentDir;
            for (int i = 0; i < mDirs.Length; i++)
            {
                var pos = FindStickyPos(mDirs[d]);
                if (pos != Vector3.zero)
                {
                    line.enabled = true;
                    return;
                }
                d = d + 1 < mDirs.Length ? d + 1 : 0;
            }

        }
#endif

    }

}
