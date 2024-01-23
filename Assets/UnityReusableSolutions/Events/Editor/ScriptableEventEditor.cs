using UnityEngine;
using UnityEditor;

namespace Devolvist.UnityReusableSolutions.Events.Editor
{
    [CustomEditor(typeof(ScriptableEvent))]
    public class ScriptableEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ScriptableEvent scriptableEvent = (ScriptableEvent)target;

            if (GUILayout.Button("Log subscribers info"))
            {
                scriptableEvent.LogInfo();
            }
        }
    }
}