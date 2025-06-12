using System;
using UnityEngine.EventSystems;

namespace Items
{
    public class ItemViewBase : ItemViewAbstract, IPointerDownHandler
    {
        public override event Action<ItemViewAbstract> OnPointerDownEvent;
    
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke(this);
        }
    }
}