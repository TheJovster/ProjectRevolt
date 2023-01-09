using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ProjectRevolt.Dialogue 
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] private string text;
        [SerializeField] private List<string> children = new List<string>();
        [SerializeField] private Rect rect = new Rect(5, 5, 200, 100);

        public string GetText() 
        {
            return text;
        }


        public List<string> GetChildren() 
        {
            return children;
        }

        public Rect GetRect()
        {
            return rect;
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition) 
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
        }

        public void SetText(string newText)
        {
            if(newText != text) 
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
            }
        }
    }
# endif
}