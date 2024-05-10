using PlatformGame.Character;
using PlatformGame.Character.Collision;
using PlatformGame.Character.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class HitBoxColliderPair
{
    public string Name;
    public HitBoxCollider Collider;
}

[Serializable]
public class HitBoxControll
{
    [SerializeField] List<HitBoxColliderPair> pairs;
    List<HitBoxColliderPair> Pairs
    {
        get
        {
#if UNITY_EDITOR
            Dictionary<HitBoxCollider, string> duplicates = new();
            foreach (var pair in pairs)
            {
                Debug.Assert(!duplicates.ContainsKey(pair.Collider), $"ÁßÃ¸µÈ °ª : {pair.Collider.name}");
                duplicates.Add(pair.Collider, pair.Name);
            }
#endif
            return pairs;
        }
    }
    public List<HitBoxCollider> Colliders => Pairs.Select(x => x.Collider).ToList();
    public float Delay;
    public bool UseSyncDelay;

    public void StartDelay()
    {
        if (!UseSyncDelay)
        {
            return;
        }

        foreach (var collider in Colliders)
        {
            collider.StartDelay();
        }
    }

    public void SyncDelay()
    {
        if (!UseSyncDelay)
        {
            return;
        }

        foreach (var collider in Colliders)
        {
            collider.HitDelay = Delay;
        }
    }

    public void SetActor(Character character)
    {
        foreach (var collider in Colliders)
        {
            collider.Actor = character;
        }
    }

    public List<HitBoxCollider> GetCollidersAs(string x)
    {
        return Pairs.Where(c => c.Name.Equals(x))
                    .Select(c => c.Collider)
                    .ToList();
    }

}