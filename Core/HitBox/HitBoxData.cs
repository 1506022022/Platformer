using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlatformGame.Character.Collision
{
    [Serializable]
    public class HitBoxData
    {
        [SerializeField] List<string> mHitBoxNames;
        [SerializeField] HitBoxFlags mFlags;
        public List<string> Filter => mHitBoxNames.ToList();
        public HitBoxFlags Flags => mFlags;
        public bool UseHitBox => Filter.Count > 0;
    }
}