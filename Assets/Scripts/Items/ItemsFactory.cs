using General;
using UnityEngine;

namespace Items
{
	public class ItemsFactory
	{
		private ObjectsPool<ItemViewAbstract> _objectsPool;

		public ItemsFactory(ItemViewAbstract prefab)
		{
			_objectsPool = new ObjectsPool<ItemViewAbstract>(() => Object.Instantiate(prefab));
		}

		public ItemViewAbstract Get()
		{
			var result = _objectsPool.Get();
			return result;
		}

		public void Free(ItemViewAbstract item)
		{
			_objectsPool.Free(item);
		}
	}
}