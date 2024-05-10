using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame.Character.Collision
{
    public class HitEventPipeline
    {
        readonly List<HitEvent> mLayers = new List<HitEvent>();

        public HitEventPipeline(int layerCapacity)
        {
            Debug.Assert(0 <= layerCapacity);
            for (var i = 0; i < layerCapacity; i++)
            {
                AddLayer();
            }
        }

        public HitEventPipeline()
        {

        }

        public void AddProcessTo(int layer, HitEvent process)
        {
            Debug.Assert(0 <= layer);
            Debug.Assert(layer < mLayers.Count, $"layer : {layer} Capacity : {mLayers.Count}");

            if (mLayers[layer] == null)
            {
                mLayers[layer] = process;
            }
            else
            {
                mLayers[layer] += process;
            }
        }
        public void RemoveProcessTo(int layer, HitEvent process)
        {
            Debug.Assert(0 <= layer);
            Debug.Assert(layer < mLayers.Count, $"layer : {layer} Capacity : {mLayers.Count}");

            mLayers[layer] -= process;
        }

        public void RemoveAllProcessTo(int layer)
        {
            Debug.Assert(0 <= layer);
            Debug.Assert(layer < mLayers.Count);

            mLayers[layer] = null;
        }

        public void AddLayer()
        {
            mLayers.Add(null);
        }

        public void Invoke(CollisionData collision)
        {
            foreach (HitEvent process in mLayers)
            {
                process?.Invoke(collision);
            }
        }

    }

}
