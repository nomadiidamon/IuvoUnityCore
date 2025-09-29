using UnityEngine;
using UnityEditor;
using System.IO;

namespace IuvoUnity
{
    namespace Editor
    {
        public class ConditionalStateScriptCreator : EditorWindow
        {
            private string className = "NewGenericState";
            private string parentFolder = "Assets";

            [MenuItem("Assets/Create/IuvoUnity/ScriptTemplates/Derived ConditionalState Script", false, 81)]
            public static void ShowWindow()
            {
                GetWindow<ConditionalStateScriptCreator>("Create Derived State");
            }

            private void OnGUI()
            {
                GUILayout.Label("Create New Derived ConditionalState", EditorStyles.boldLabel);
                className = EditorGUILayout.TextField("Class Name", className);
                parentFolder = EditorGUILayout.TextField("Parent Folder", parentFolder);


                if (GUILayout.Button("Create Script"))
                {
                    CreateDerivedScript(className, parentFolder);
                }
            }

            private void CreateDerivedScript(string name, string parentFolder)
            {
                string folderPath = "Assets";

                if (Selection.activeObject != null)
                {
                    string path = AssetDatabase.GetAssetPath(Selection.activeObject);
                    folderPath = Directory.Exists(path) ? path : Path.GetDirectoryName(path);
                }
                if (!Directory.Exists(parentFolder))
                {
                    EditorUtility.DisplayDialog("Error", "Parent folder does not exist!", "OK");
                    return;
                }
                string filePath = Path.Combine(parentFolder, folderPath, name + ".cs");

                if (File.Exists(filePath))
                {
                    EditorUtility.DisplayDialog("Error", "File already exists!", "OK");
                    return;
                }

                string template =
    $@"using IuvoUnity.StateMachine;
using UnityEngine;


namespace IuvoUnity{{

namespace StateMachine {{

[CreateAssetMenu(fileName = ""{name}"", menuName = ""IuvoUnity/StateMachine/{name}"", order = 2)]
public class {name} : ConditionalState
{{
    public new string stateName = ""{name}"";
    public new UpdateMode updateMode = UpdateMode.None;

    // Override condition checks if needed
    public override bool InterruptConditionsMet(ConditionalStateMachine stateMachine)
    {{
        return base.AreConditionsMet(stateInterruptConditions);
    }}

    public override bool ExitConditionsMet(ConditionalStateMachine stateMachine)
    {{
        return base.AreConditionsMet(stateExitConditions);
    }}

    public override bool EnterConditionsMet(ConditionalStateMachine stateMachine)
    {{
        return base.AreConditionsMet(stateEnterConditions);
    }}

    public override bool ContinueConditionsMet(ConditionalStateMachine stateMachine)
    {{
        return base.AreConditionsMet(stateContinueConditions);
    }}
}}

}}

}}";

                File.WriteAllText(filePath, template);
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(filePath);
            }
        }
    }
}