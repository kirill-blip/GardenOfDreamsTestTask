#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;

public static class Tools
{
    public static void RefreshProjectView()
    {
        AssetDatabase.Refresh();
    }

    public static void ClearConsole()
    {
        Type logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        MethodInfo clearMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }
}
#endif