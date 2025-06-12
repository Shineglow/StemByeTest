using System;
using UnityEngine;

namespace Items
{
    public abstract class ItemViewAbstract : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private SpriteRenderer foreground;
    
        public abstract event Action<ItemViewAbstract> OnPointerDownEvent;
    
        public void SetBackground(Sprite sprite)
        {
            background.sprite = sprite;
        }

        public void SetForeground(Sprite sprite)
        {
            foreground.sprite = sprite;
        }

        public void SetMaterial(Material material)
        {
            background.material = material;
        }
    }
}
