using UnityEngine;

namespace ProjectRevolt.Stats 
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] CharacterClass characterClass;
            [SerializeField][Tooltip("The array element each correspond to one player level. Element 0 corresponds to Level 1, Element 1 corresponds to Level 2, etc.")] float[] health;
            
        }
    }
}
