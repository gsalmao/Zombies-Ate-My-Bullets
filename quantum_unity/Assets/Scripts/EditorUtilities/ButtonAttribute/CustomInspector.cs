#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace EditorUtilities
{
    /// <summary>
    /// Custom Inspector to draw extra elements. Method buttons for example.
    /// </summary>
    [CustomEditor(typeof(Object), true), CanEditMultipleObjects]
    public class CustomInspector : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
            IEnumerable<MethodInfo> methods = target.GetType().GetMethods(flags);

            foreach (MethodInfo method in methods)
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
                if (buttonAttribute == null)
                    continue;

                ButtonDrawer.DrawButton(root, target, method, buttonAttribute);
            }

            return root;
        }
    }
}

#endif