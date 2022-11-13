using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace ProjectRevolt.Dialogue.Editor 
{
    
    public class DialogueEditor : EditorWindow
    {
        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow() 
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OpenDialogue(int instanceID, int line) 
        {
            Debug.Log("OpenDialogue");
            return false;
        }
    }
}
