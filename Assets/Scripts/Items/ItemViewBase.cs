using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemViewBase : ItemViewAbstract, IPointerDownHandler
{
    public override event Action OnPointerDownEvent;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();
    }

    
}