using System;
using General;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    public class ItemsFactory
    {
        private ObjectsPool<ItemViewAbstract> _itemsPool;
        private ItemViewAbstract[] _prefabs;

        public ItemsFactory()
        {
            _prefabs = Resources.LoadAll<ItemViewAbstract>(ProjectPaths.ITEMS_PREFABS_FOLDER);
            _itemsPool = new ObjectsPool<ItemViewAbstract>(GetPrefab);
            if (_prefabs.Length == 0)
            {
                throw new Exception($"Items prefabs folder \"Resources\\{ProjectPaths.ITEMS_PREFABS_FOLDER}\" is empty");
            }
        }

        private ItemViewAbstract GetPrefab()
        {
            var result = _prefabs[Random.Range(0, _prefabs.Length)];
            return result;
        }
        
        ItemViewAbstract GetRandom()
        {
            var result = _itemsPool.Get();
            return result;
        }

        void Return(ItemViewAbstract item)
        {
            _itemsPool.Free(item);
        }
    }
}