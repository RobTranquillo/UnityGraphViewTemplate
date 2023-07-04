using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow
{
    BehaviourTreeView treeView;
    InspectorView inspectorView;

    [MenuItem("vrbits/Scene Flow Editor")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("SceneFlow Editor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/GraphViewBehaviourTree/Editor/BehaviourTreeEditor.uxml");
        // Instantiate UXML
        visualTree.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/GraphViewBehaviourTree/Editor/BehaviourTreeEditor.uss");
        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/GraphViewBehaviourTree/Editor/BehaviourTreeEditor.uss"));

        treeView = root.Q<BehaviourTreeView>();
        inspectorView = root.Q<InspectorView>();
        treeView.OnNodeSelected = OnNodeSelectionChanged;
        OnSelectionChange();
    }

    private void OnSelectionChange()
    {
        BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            treeView.PopulateView(tree);
    }

    void OnNodeSelectionChanged(NodeView node)
    {
        inspectorView.UpdateSelection(node);
    }
}
