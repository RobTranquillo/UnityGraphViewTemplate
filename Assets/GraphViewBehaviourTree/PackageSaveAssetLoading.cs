using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public static class PackageSaveAssetLoading
{
    public static VisualTreeAsset GetUXMLAsset()
    {
        //not package save way of loading the UXML file
        //AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/GraphViewBehaviourTree/Editor/BehaviourTreeEditor.uxml");

        //package save way of loading the UXML file
        string uxml_guid = "f203b4b62974b364abdb10172c54c2a3";
        string uxml_path = AssetDatabase.GUIDToAssetPath(uxml_guid);
        var foo = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxml_path);
        return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxml_path);
    }

    public static StyleSheet GetUSSAsset()
    {
        //not package save way of loading the USS file
        //AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/GraphViewBehaviourTree/Editor/BehaviourTreeEditor.uss");

        //package save way of loading the USS file
        string uss_guid = "460e681c420b1394a9dc4a1f58f40e11";
        string uss_path = AssetDatabase.GUIDToAssetPath(uss_guid);
        return AssetDatabase.LoadAssetAtPath<StyleSheet>(uss_path);
    }
}
