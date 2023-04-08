using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StateTemplateModifier : UnityEditor.AssetModificationProcessor
{
    const string TemplateString =
        "using UnityEngine;\n" +
        "\n" +
        "namespace Review.StateMachine.States" +
        "{\n"+
        "   public class #CLASSNAME# : BaseStateObject\n" +
        "   {\n" +
        "       public override string stateName { get; protected set; } = \"#STATENAME#\";\n" +
        "       public override BaseState state { get; protected set; } = new #STATENAME#();\n" +
        "   }\n" +
        "}\n"+
        "";

    const string StateObjectFilePath = "Assets/Scripts/Review/StateMachines/States/StataObjects/";
    const string StateFilePath = "Assets/Scripts/Review/StateMachines/States/";
    const string StateScriptableObjectFilePath = "Assets/Scripts/Review/StateMachines/States/StataObjects/ScriptableObjects";

    private static void OnWillCreateAsset(string path)
    {
        //作成されたファイルが.csファイルかどうかを調べる
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        if (index < 0) return;
        string fileExtension = path.Substring(index);
        if (fileExtension != ".cs") return;

        //クラス名の末尾にStateObjectが含まれているか確認する
        index = path.LastIndexOf("/");
        string className = path.Substring(index + 1).Replace(".cs", "");
        if (!className.EndsWith("StateObject")) return;

        //ファイルの作成場所を確認する
        if (!path.StartsWith(StateObjectFilePath)) return;

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        //string fileContent = File.ReadAllText(path);
        index = className.LastIndexOf("Object");
        string stateName = className.Substring(0,index);
        string content = TemplateString.Replace("#CLASSNAME#", className).Replace("#STATENAME#", stateName);
        File.WriteAllText(path, content);
        AssetDatabase.Refresh();
    }
}
