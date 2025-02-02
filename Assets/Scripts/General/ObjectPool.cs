using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace General
{
    public class ObjectPool : MonoBehaviour
    {
        public GameObject Prefab;
        private readonly List<IPooledObject> _inUse = new();
        private readonly List<IPooledObject> _available = new();
        [HideInInspector] public bool NewObjectAdded;
        
        public IPooledObject GetPooledObject()
        {
            NewObjectAdded = false;
            // Check if there are any available objects, if yes return the first:
            if (_available.Count > 0)
            {
                var newObject0 = _available.First();
                newObject0.Instantiate();
                _available.Remove(newObject0);
                _inUse.Add(newObject0);
                return newObject0;
            }
            
            // If no, instantiate a new object:
            var newObject = Instantiate(Prefab, transform);
            var poolComponent = newObject.GetComponent<IPooledObject>();
            poolComponent.Instantiate();
            _inUse.Add(poolComponent);
            NewObjectAdded = true;
            return poolComponent;
        }

        public void ReleasePooledObject(IPooledObject pooledObject)
        {
            if (_available.Contains(pooledObject)) return;
            
            // Remove from in use list to the available list:
            _inUse.Remove(pooledObject);
            pooledObject.Release();
            _available.Add(pooledObject);
        }   
    }

    public interface IPooledObject
    {
        public void Instantiate();
        public void Release();
    }
}