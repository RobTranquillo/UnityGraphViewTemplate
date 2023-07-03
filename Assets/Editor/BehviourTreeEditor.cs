using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehviourTreeEditor : EditorWindow
{
    BehaviourTreeView treeView;
    InspectorView inspectorView;

    [MenuItem("vrbits/Scene Flow Editor")]
    public static void OpenWindow()
    {
        BehviourTreeEditor wnd = GetWindow<BehviourTreeEditor>();
        wnd.titleContent = new GUIContent("SceneFlow Editor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehviourTreeEditor.uxml");
        // Instantiate UXML
        visualTree.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehviourTreeEditor.uss");
        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehviourTreeEditor.uss"));

        treeView = root.Q<BehaviourTreeView>();
        inspectorView = root.Q<InspectorView>();

        OnSelectionChange();
    }

    private void OnSelectionChange()
    {
        BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (tree)
        {
            treeView.PopulateView(tree);
        }
        
    }
}
