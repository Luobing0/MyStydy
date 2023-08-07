using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;
using System;

public class BehavirWindow : EditorWindow
{
    BehavirTreeView behavirTreeView;
    InspectorView inspectorView;

    [MenuItem("Window/UI Toolkit/BehavirWindow")]
    public static void ShowExample()
    {
        BehavirWindow wnd = GetWindow<BehavirWindow>();
        wnd.titleContent = new GUIContent("BehavirWindow");
    }
    [OnOpenAsset]
    public static bool OnOpenAsset(int instaceID, int line)
    {
        if (Selection.activeObject is MyBehaviorTree)
        {
            ShowExample();
            return true;
        }
        return false;
    }
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehavirWindow.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehavirWindow.uss");
        root.styleSheets.Add(styleSheet);

        behavirTreeView = root.Q<BehavirTreeView>();
        inspectorView = root.Q<InspectorView>();
        behavirTreeView.OnNodeSelected = OnNodeSelectionChanged;
        OnSelectionChange();
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange change)
    {
        switch (change)
        {
            case PlayModeStateChange.EnteredEditMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.EnteredPlayMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingEditMode:
                break;
            case PlayModeStateChange.ExitingPlayMode:
                break;
        }
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }


    private void OnSelectionChange()
    {
        MyBehaviorTree tree = Selection.activeObject as MyBehaviorTree;
        if (!tree)
        {
            if (Selection.activeGameObject)
            {
                BehaviorTreeRunner runner = Selection.activeGameObject.GetComponent<BehaviorTreeRunner>();
                if (runner)
                {
                    tree = runner.tree;
                }
            }
        }
        if (Application.isPlaying)
        {
            if (tree)
            {
                behavirTreeView.PopulateView(tree);
            }
        }
        else
        {
            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                behavirTreeView.PopulateView(tree);
            }
        }

    }

    void OnNodeSelectionChanged(NodeView node)
    {
        inspectorView.UpdateSlection(node);
    }
}