using System;
using System.Collections.Generic;
using System.Linq;
using General;
using General.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    public abstract class ItemsLifeTimeSystemAbstract : MonoBehaviour
    {
        protected Dictionary<ItemViewAbstract, ObjectsPool<ItemViewAbstract>> _prefabToPool;
        protected Dictionary<ItemViewAbstract, ObjectsPool<ItemViewAbstract>> _instanceToPool;

        protected ItemViewAbstract[] _prefabs;
        protected Material[] _materials;
        protected Sprite[] _animalsSprites;
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        protected virtual void Start()
        {
            InitializePrefabs();
            InitializeMaterialsSet();
            InitializeAnimalsSprites();
        }

        private void InitializeAnimalsSprites()
        {
            var sprites = Resources.LoadAll<SpriteSO>(ProjectPaths.ITEMS_DATA_FOLDER);
            _animalsSprites = sprites.Select(i => i.Sprite).ToArray();
        }

        private void InitializeMaterialsSet()
        {
            var colors = Resources.LoadAll<ColorSO>(ProjectPaths.ITEMS_DATA_FOLDER);
            var materialPrefab = Resources.Load<Material>(ProjectPaths.MATERIALS_FOLDER+"/ColorByMaskTexture");
            _materials = new Material[colors.Length];
            for (var i = 0; i < colors.Length; i++)
            {
                var color = colors[i].Color;
                _materials[i] = Instantiate(materialPrefab);
                _materials[i].SetColor(Color1, color);
            }
        }

        private void InitializePrefabs()
        {
            _prefabs = Resources.LoadAll<ItemViewAbstract>(ProjectPaths.ITEMS_PREFABS_FOLDER);
            if (_prefabs.Length == 0)
            {
                throw new Exception($"Items prefabs folder \"Resources\\{ProjectPaths.ITEMS_PREFABS_FOLDER}\" is empty");
            }

            _prefabToPool = new();
            _instanceToPool = new();
            foreach (var prefab in _prefabs)
            {
                _prefabToPool[prefab] = new ObjectsPool<ItemViewAbstract>(() => Instantiate(prefab));
            }
        }

        protected ItemViewAbstract GetRandom()
        {
            var objectsPool = _prefabToPool[_prefabs[Random.Range(0, _prefabs.Length)]];
            var result = objectsPool.Get();
            _instanceToPool.TryAdd(result, objectsPool);
            return result;
        }

        protected void Return(ItemViewAbstract item)
        {
            var objectsPool = _instanceToPool[item];
            objectsPool.Free(item);
        }
    }
}