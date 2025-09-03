using UnityEngine;
using UnityEditor;
using System.IO;

namespace IuvoUnity
{
    namespace Editor
    {
        public class BaseConfigScriptCreator : EditorWindow
        {
            private string className = "NewBaseConfig";

            [MenuItem("Assets/Create/IuvoUnity/Derived BaseConfig Script", false, 82)]
            public static void ShowWindow()
            {
                GetWindow<BaseConfigScriptCreator>("Create Derived BaseConfig");
            }

            private void OnGUI()
            {
                GUILayout.Label("Create New Derived BaseConfig", EditorStyles.boldLabel);
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

                if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(name))
                {
                    EditorUtility.DisplayDialog("Invalid Name", "The class name contains invalid characters.", "OK");
                    return;
                }

                string template =
    $@"using UnityEngine;
using IuvoUnity._BaseClasses;
using IuvoUnity._Interfaces;

namespace IuvoUnity {{

namespace _StateMachine {{
[CreateAssetMenu(fileName = ""{name}"", menuName = ""IuvoUnity/Configs/{name}"", order = 2)]
public class {name} : BaseConfig
{{
    // Optional: set a default config name
    private void OnEnable()
    {{
        if (string.IsNullOrEmpty(configName))
        {{
            configName = ""{{name}}"";
        }}
    }}

    public override void Configure()
    {{
        // Implement configuration logic here
    }}

    public override void OnConfigure()
    {{
        // Implement post-configuration logic here
    }}

    public override void Reconfigure()
    {{
        // Implement reconfiguration logic here
    }}

    public override void OnReconfigure()
    {{
        // Implement post-reconfiguration logic here
    }}

    public override void PrintInfo()
    {{
        base.PrintInfo();
        // Additional info if needed
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