using UnityEngine;
using UnityEditor;

public class ReplaceObjects_Editor : EditorWindow
{
    int CurrentSelectionCount = 0;
    string SelectedTag = "";
    GameObject WantedObject;
    Vector3 Scale;

    public static void LaunchEditor()
    {
        ReplaceObjects_Editor Window = GetWindow<ReplaceObjects_Editor>("Replace Objects");
        Window.Show();
    }

    private void OnGUI()
    {
        GetSelection();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();

        SelectedTag = EditorGUI.TagField(new Rect(3, 3, position.width / 2 - 6, 20), "New Tag: ", SelectedTag);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Selection Count: " + CurrentSelectionCount.ToString(), EditorStyles.boldLabel);
        EditorGUILayout.Space();

        Scale = EditorGUILayout.Vector3Field("Scale: ", Scale);

        EditorGUILayout.Space();
        WantedObject = (GameObject)EditorGUILayout.ObjectField("Replace Object: ", WantedObject, typeof(GameObject), true);
        EditorGUILayout.Space(); 

        if (GUILayout.Button("Apply Changes", GUILayout.ExpandWidth(true), GUILayout.Height(40)))
        {
            ApplyObjectChanges();
            Repaint();
        }

        EditorGUILayout.Space();
        
        EditorGUILayout.EndVertical();

        Repaint();
    }

    void GetSelection()
    {
        CurrentSelectionCount = 0;
        CurrentSelectionCount = Selection.gameObjects.Length;
    }

    void ApplyObjectChanges()
    {
        if(CurrentSelectionCount == 0)
        {
            HandleInvalidInput("At least one object needs to be selected to replace!");
            return;
        }

        if(!WantedObject)
        {
            HandleInvalidInput("At least one object needs to be selected to replace selected objects with");
            return;
        }

        if(SelectedTag == "")
        {
            HandleInvalidInput("A tag has to be chosen!");
            return;
        }

        if(Scale == Vector3.zero)
        {
            HandleInvalidInput("A Scale has to be chosen!");
            return;
        }

        GameObject[] SelectedObjects = Selection.gameObjects;
        for (int i = 0; i < SelectedObjects.Length; i++)
        {
            Transform SelectTransform = SelectedObjects[i].transform;
            GameObject NewObject = Instantiate(WantedObject, SelectTransform.position, SelectTransform.rotation);
            NewObject.transform.localScale = SelectTransform.localScale;
            NewObject.tag = SelectedTag;
            NewObject.transform.localScale = Scale;

            DestroyImmediate(SelectedObjects[i]);
        }
    }

    void HandleInvalidInput(string Message)
    {
        EditorUtility.DisplayDialog("Replace Objects Warning!", Message, "OK");
    }
}
