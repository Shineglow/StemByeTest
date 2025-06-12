using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
	public class ItemsLifeTimeSystemBase : ItemsLifeTimeSystemAbstract
	{
		[SerializeField] private int itemsInSetCount;
		[SerializeField] private int setsCount;
		[SerializeField] private int inGameItemsCount;
		[SerializeField] private Transform spawnPoint;
		[SerializeField] private float spawnInterval;

		private CancellationTokenSource _cts;
		private Queue<(ItemViewAbstract, Material, Sprite)> itemsQueue;
		private HashSet<ItemViewAbstract> itemsInGame = new();

		protected override void Start()
		{
			base.Start();
			FillQueue();
			_cts = new CancellationTokenSource();
			SpawnOverTime(_cts.Token).Forget();
		}

		private async UniTask SpawnOverTime(CancellationToken token)
		{
			Vector3 left, right;
			left = right = spawnPoint.position;
			left.x += 1;
			right.x -= 1;
			
			while (true)
			{
				if(token.IsCancellationRequested)
					break;
				if (itemsQueue.Count > 0 && itemsInGame.Count < inGameItemsCount)
				{
					var (prefab, material, sprite) = itemsQueue.Dequeue();
					var objectsPool = _prefabToPool[prefab];
					var item = objectsPool.Get();
					item.gameObject.SetActive(true);
					_instanceToPool.TryAdd(item, objectsPool);
					item.SetForeground(sprite);
					item.SetMaterial(material);
					item.transform.position = itemsInGame.Count % 2 != 0 ? left : right;
					item.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180, 180));
					item.OnPointerDownEvent += OnItemPointerDown;
					itemsInGame.Add(item);
					await UniTask.Delay(Convert.ToInt32(spawnInterval*1000), cancellationToken: token);
				}
				else
				{
					await UniTask.Yield();
				}
			}
		}

		private void OnItemPointerDown(ItemViewAbstract item)
		{
			itemsInGame.Remove(item);
			item.OnPointerDownEvent -= OnItemPointerDown;
			item.gameObject.SetActive(false);
			Return(item);
			Debug.Log(itemsInGame.Count);
			Debug.Log(itemsQueue.Count);
		}

		private void FillQueue()
		{
			itemsQueue = new (setsCount * itemsInSetCount);
			List<(ItemViewAbstract prefab, Material material, Sprite sprite)> items = new(setsCount * itemsInSetCount);
			for (int i = 0; i < setsCount * itemsInSetCount; i+=3)
			{
				var prefab = _prefabs[Random.Range(0, _prefabs.Length)];
				var material = _materials[Random.Range(0, _materials.Length)];
				var sprite = _animalsSprites[Random.Range(0, _animalsSprites.Length)];
				items.Add((prefab, material, sprite));
				items.Add((prefab, material, sprite));
				items.Add((prefab, material, sprite));
			}
			
			// Shafling
			while (items.Count > 0)
			{
				var randomIndex = Random.Range(0, items.Count);
				(items[randomIndex], items[^1]) = (items[^1], items[randomIndex]);
				itemsQueue.Enqueue(items[^1]);
				items.RemoveAt(items.Count - 1);
			}
		}

		private void OnDestroy()
		{
			_cts.Cancel();
			_cts.Dispose();
		}
	}
}