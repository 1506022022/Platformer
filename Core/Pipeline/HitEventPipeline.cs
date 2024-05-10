using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame.Character.Collision
{
    public class HitEventPipeline
    {
        List<HitEvent> Layers = new List<HitEvent>();

        public HitEventPipeline(int layerCapacity)
        {
            Debug.Assert(0 <= layerCapacity);
            for (int i = 0; i < layerCapacity; i++)
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
            Debug.Assert(layer < Layers.Count, $"layer : {layer} Capacity : {Layers.Count}");

            if (Layers[layer] == null)
            {
                Layers[layer] = process;
            }
            else
            {
                Layers[layer] += process;
            }
        }
        public void RemoveProcessTo(int layer, HitEvent process)
        {
            Debug.Assert(0 <= layer);
            Debug.Assert(layer < Layers.Count, $"layer : {layer} Capacity : {Layers.Count}");

            Layers[layer] -= process;
        }

        public void RemoveAllProcessTo(int layer)
        {
            Debug.Assert(0 <= layer);
            Debug.Assert(layer < Layers.Count);

            Layers[layer] = null;
        }

        public void AddLayer()
        {
            Layers.Add(null);
        }

        public void Invoke(CollisionData collision)
        {
            foreach (HitEvent process in Layers)
            {
                process?.Invoke(collision);
            }
        }

    }

}
