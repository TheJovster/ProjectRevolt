using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Dialogue 
{
    [Serializable]
    public class DialogueNode
    {
        [SerializeField] private string uniqueID;
        public string text;
        public string[] children;
    }

}