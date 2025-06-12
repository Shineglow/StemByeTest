using UnityEngine;

namespace General.Data
{
    [CreateAssetMenu(menuName = "StemByeTest/Data/Color", fileName = "ColorSO")]
    public class ColorSO : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
    }
}