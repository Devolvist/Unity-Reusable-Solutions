using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityObject = UnityEngine.Object;
using UnityEditor.SceneManagement;

namespace Devolvist.UnityReusableSolutions.InspectorFieldsNullValidation
{
    public class InspectorFieldsNullValidationWindow : EditorWindow
    {
        private bool _validationPerformed;
        private Vector2 _scrollPosition = Vector2.zero;

        private List<(string, UnityObject)> _nullReferences = new List<(string, UnityObject)>();

        [MenuItem("Devolvist/Inspector Fields Null Validation")]
        private static void InitializeAndOpen()
        {
            var thisWindow =
                GetWindow<InspectorFieldsNullValidationWindow>(title: "Inspector Fields Null Validation");

            thisWindow.minSize = new Vector2(500, 500);
        }

        private void OnGUI()
        {
            #region CHECK_BUTTON
            GUILayout.Space(30);

            GUILayout.BeginArea(new Rect(position.width / 2 - 150, position.height / 15, 300, 50));
     
            if (GUILayout.Button("Check inspectors for null-fields", GUILayout.Height(50))) 
            {
                ValidateInspectors();
                _validationPerformed = true;
            }

            GUILayout.EndArea();
            #endregion

            if (_validationPerformed)
            {
                if (_nullReferences.Count > 0)
                {
                    DrawFindedNullFieldsLabel();
                    DrawSelectionTip();
                    DrawNullFieldsButtons();
                }
                else
                {
                    DrawNotFindedFieldsLabel();
                }
            }
        }

        private void DrawFindedNullFieldsLabel()
        {
            GUILayout.BeginArea(new Rect(position.width / 2 - 110, position.height / 5, 300, 30));

            GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel);
            labelStyle.normal.textColor = Color.red;
            GUILayout.Label("Finded inspectors with null-fields:", labelStyle);

            GUILayout.EndArea();
        }

        private void DrawNotFindedFieldsLabel()
        {
            GUILayout.BeginArea(new Rect(position.width / 2 - 70, position.height / 5, 300, 30));

            GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel);
            labelStyle.normal.textColor = Color.green;
            GUILayout.Label("Null-fields not finded", labelStyle);

            GUILayout.EndArea();
        }

        private void DrawSelectionTip()
        {
            GUILayout.BeginArea(new Rect(position.width / 2 - 140, position.height / 3.5f, 300, 30));

            var style = new GUIStyle();
            style.fontStyle = FontStyle.Italic;
            style.fontSize = 11;
            style.normal.textColor = Color.white;

            GUILayout.Label("Click on the button to select an object in the editor", style);

            GUILayout.EndArea();
        }

        private void DrawNullFieldsButtons()
        {
            GUILayout.BeginArea(
                screenRect: new Rect(position.width / 2 - 150, position.height / 3, 
                width: 300,
                height: 300));

            _scrollPosition = GUILayout.BeginScrollView(
                scrollPosition: _scrollPosition);

            foreach (var reference in _nullReferences)
            {
                if (GUILayout.Button(
                    text: $"Script: {reference.Item2.GetType().Name}\nField: {reference.Item1}",
                    options: GUILayout.Height(40)))
                    EditorGUIUtility.PingObject(reference.Item2);
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        private void ValidateInspectors()
        {
            _nullReferences.Clear();

            ValidateScriptableObjects();

            if (PrefabStageUtility.GetCurrentPrefabStage() != null)
            {
                ValidateCurrentScene(PrefabStageUtility.GetCurrentPrefabStage().scene);
            }
            else if (SceneManager.GetActiveScene() != null)
            {
                ValidateCurrentScene(SceneManager.GetActiveScene());
            }
        }

        private void ValidateCurrentScene(Scene scene)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();

            foreach (GameObject rootObject in rootObjects)
            {
                MonoBehaviour[] components = rootObject.GetComponentsInChildren<MonoBehaviour>(true);

                foreach (var component in components)
                {
                    if (Attribute.IsDefined(component.GetType(), typeof(InspectorFieldsNullValidationAttribute)))
                        CheckFields(component);
                }
            }
        }

        private void ValidateScriptableObjects()
        {
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);


                if (scriptableObject != null & Attribute.IsDefined(scriptableObject.GetType(), typeof(InspectorFieldsNullValidationAttribute)))
                    CheckFields(scriptableObject);
            }
        }

        private void CheckFields(UnityObject obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (Attribute.IsDefined(field, typeof(HideInInspector)))
                    continue;

                if (field.IsPrivate & !Attribute.IsDefined(field, typeof(SerializeField)))
                    continue;

                object value = field.GetValue(obj);

                if (value == null || value.ToString().Equals("null"))
                    _nullReferences.Add(($"{field.Name}", obj));
            }
        }
    }
}