using ProjectRevolt.Core;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ProjectRevolt.Dialogue 
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] private bool isPlayerSpeaking = false;
        [SerializeField] private string text;
        [SerializeField] private List<string> children = new List<string>();
        [SerializeField] private Rect rect = new Rect(5, 5, 200, 100);
        [SerializeField] private string onEnterAction;
        [SerializeField] private string onExitAction;
        [SerializeField] Condition condition;

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

        public bool IsPlayerSpeaking() 
        {
            return isPlayerSpeaking;
        }

        public string GetEnterAction() 
        {
            return onEnterAction;
        }
        public string GetExitAction()
        {
            return onExitAction;
        }

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators) 
        {
            return condition.Check(evaluators);
        }


#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition) 
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if(newText != text) 
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
                //remember to SetDirty(this) if you add any more setters
            }
        }

        public void SetPlayerSpeaking(bool newIsPlayerSpeaking) 
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this); 
        }
    }
# endif
}