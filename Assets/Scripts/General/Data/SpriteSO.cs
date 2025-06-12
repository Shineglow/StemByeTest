using UnityEngine;

namespace General.Data
{
    [CreateAssetMenu(menuName = "StemByeTest/Data/Sprite", fileName = "SpriteSO")]
    public class SpriteSO : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}