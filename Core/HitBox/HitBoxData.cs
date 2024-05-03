using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer.Core
{
    [Serializable]
    public class HitBoxData
    {
        [SerializeField] List<string> mHitBoxNames;
        [SerializeField] HitBoxFlags mFlags;
        [SerializeField] bool mUseSelfHitBox;
        public List<string> HitBoxNames => mHitBoxNames.ToList();
        public HitBoxFlags Flags => mFlags;
        public bool UseSelfHitBox => mUseSelfHitBox;
    }
}
