using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Review.StateMachines.Editor
{
    [InitializeOnLoad]
    public class StatePostProcessor //: AssetPostprocessor
    {
        private const string StateObjectFilePath = "Assets/Scripts/Review/StateMachines/States/StataObjects/";
        private const string StateScriptableObjectFilePath = "Assets/Scripts/Review/StateMachines/States/StataObjects/ScriptableObjects/";

        private static HashSet<string> compiledPaths = new HashSet<string>();

        private static bool HadCreated = false;

        static StatePostProcessor()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            if (EditorApplication.isCompiling)
            {
                HadCreated = false;
            }
            else if (!HadCreated)
            {
                HadCreated = true;

                // 新しい.csファイルを検出
                var currentScriptPaths = GetStateObjectPaths();

                if (currentScriptPaths.Except(compiledPaths).Any())
                {
                    foreach (var path in currentScriptPaths.Except(compiledPaths))
                    {
                        CreateStateScriptableObject(path);
                    }
                    compiledPaths = currentScriptPaths.ToHashSet();
                }
            }
        }

        private static IEnumerable<String> GetStateObjectPaths()
        {
            return AssetDatabase.GetAllAssetPaths()
                .Where(path => path.StartsWith(StateObjectFilePath));
        }

        private static void CreateStateScriptableObject(string path)
        {
            //作成されたファイルが.csファイルかどうかを調べる
            int index = path.LastIndexOf(".");
            if (index < 0) return;
            string fileExtension = path.Substring(index);
            if (fileExtension != ".cs") return;

            //クラス名の末尾にStateObjectが含まれているか確認する
            index = path.LastIndexOf("/");
            string className = path.Substring(index + 1).Replace(".cs", "");
            if (!className.EndsWith("StateObject")) return;

            Assembly assembly = Assembly.Load("Assembly-CSharp");
            Debug.Log(className);
            var type = assembly.GetType($"Review.StateMachines.States.StateObjects.{className}");
            Debug.Log(type);
            var obj = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(obj, $"{StateScriptableObjectFilePath}{className}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

