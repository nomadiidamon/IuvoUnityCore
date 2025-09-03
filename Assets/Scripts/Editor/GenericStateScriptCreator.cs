using UnityEngine;
using UnityEditor;
using System.IO;

namespace IuvoUnity
{
    namespace Editor
    {
        public class GenericStateScriptCreator : EditorWindow
        {
            private string className = "NewGenericState";

            [MenuItem("Assets/Create/IuvoUnity/ScriptTemplates/Derived GenericState Script", false, 81)]
            public static void ShowWindow()
            {
                GetWindow<GenericStateScriptCreator>("Create Derived State");
            }

            private void OnGUI()
            {
                GUILayout.Label("Create New Derived GenericState", EditorStyles.boldLabel);
                className = EditorGUILayout.TextField("Class Name", className);

                if (GUILayout.Button("Create Script"))
                {
                    CreateDerivedScript(className);
                }
            }

            private void CreateDerivedScript(string name)
            {
                string folderPath = "Assets";

                if (Selection.activeObject != null)
                {
                    string path = AssetDatabase.GetAssetPath(Selection.activeObject);
                    folderPath = Directory.Exists(path) ? path : Path.GetDirectoryName(path);
                }

                string filePath = Path.Combine(folderPath, name + ".cs");

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
public class {name} : GenericState
{{
    // Optional: initialize stateName and updateMode
    public new string stateName = ""{name}"";
    public new UpdateMode updateMode = UpdateMode.None;

    // Override condition checks if needed
    public override bool InterruptConditionsMet(GenericStateMachine stateMachine)
    {{
        return base.AreConditionsMet(stateInterruptConditions);
    }}

    public override bool ExitConditionsMet(GenericStateMachine stateMachine)
    {{
        return base.AreConditionsMet(stateExitConditions);
    }}

    public override bool EnterConditionsMet(GenericStateMachine stateMachine)
    {{
        return base.AreConditionsMet(stateEnterConditions);
    }}

    public override bool ContinueConditionsMet(GenericStateMachine stateMachine)
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