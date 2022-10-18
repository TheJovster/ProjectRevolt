using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProjectRevolt.Saving 
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {

        [SerializeField] string uniqueIdentifier = "";

        public string GetUniqueIdentifier() 
        {
            return uniqueIdentifier;
        }

        public object CaptureState() 
        {
            Debug.Log("Capturing state");
            return null;
        }

        public void RestoreState(object state) 
        {
            Debug.Log("Restoring state for " + GetUniqueIdentifier());
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

            if(string.IsNullOrEmpty(property.stringValue)) 
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif 
    }
}
