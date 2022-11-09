using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Dialogue 
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "ProjectRevolt/Dialogue", order = 10)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] DialogueNode[] nodes;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
