using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Dialogue 
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "ProjectRevolt/Dialogue", order = 10)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] 
        List<DialogueNode> nodes = new List<DialogueNode>();

#if UNITY_EDITOR
        private void Awake()
        {
            if(nodes.Count == 0) 
            {
                nodes.Add(new DialogueNode());
            }
        }
#endif
    }
}
