using System;
using System.Reflection;
using UnityEditor;

namespace Utiliy
{
    public static class Utilities
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
}
