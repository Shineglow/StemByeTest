using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemViewAbstract : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image foreground;
    
    public abstract event Action OnPointerDownEvent;
    
    public void SetBackground(Sprite sprite)
    {
        background.sprite = sprite;
    }

    public void SetForeground(Sprite sprite)
    {
        foreground.sprite = sprite;
    }

    public void SetColor(Color color)
    {
        background.color = color;
    }
}
