using System.Reflection;
using UnityEngine.UIElements;

namespace EditorUtilities
{
    /// <summary>
    /// Responsible for drawing button attributes in the inspector, for debugging purposes.
    /// </summary>
    public static class ButtonDrawer
    {

        public static void DrawButton(VisualElement root, object methodOwner, MethodInfo method, ButtonAttribute buttonAttribute)
        {
            string buttonName = string.IsNullOrEmpty(buttonAttribute.Name) ? method.Name : buttonAttribute.Name;
            ParameterInfo[] parameters = method.GetParameters();

            Button _button = new Button(() => method.Invoke(methodOwner, method.GetParameters())) {text = buttonName };
            
            if (parameters.Length == 0)
                root.Add(_button);
        }
    }
}
