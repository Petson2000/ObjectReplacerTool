using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ReplaceObjects_Menu
{
    [MenuItem("Tools/Scene Tools/Replace Selected Objects")]
    public static void ReplaceSelectedObjects()
    {
        ReplaceObjects_Editor.LaunchEditor();
    }
}
