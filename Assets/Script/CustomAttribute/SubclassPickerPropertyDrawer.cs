using System;
using System.Collections;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(SubclassPickerAttribute))]
public class SubclassPickerPropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property);
    }

    IEnumerable GetClasses(Type baseType)
    {
        return Assembly.GetAssembly(baseType).GetTypes().Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t));
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Type t = fieldInfo.FieldType;

        if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>))
        {
            t = t.GetGenericArguments()[0];
        }

        if (t.IsArray && t.FullName.EndsWith("[]")) 
        { 
            t = Type.GetType(t.FullName.Substring(0, t.FullName.Length - 2)); 
        } 
        string typeName = property.managedReferenceValue?.GetType().Name ?? "Not set";

        Rect dropdownRect = position;
        dropdownRect.x += EditorGUIUtility.labelWidth + 2;
        dropdownRect.width -= EditorGUIUtility.labelWidth + 2;
        dropdownRect.height = EditorGUIUtility.singleLineHeight;
        if (EditorGUI.DropdownButton(dropdownRect, new(typeName), FocusType.Keyboard))
        {
            GenericMenu menu = new GenericMenu();

            // null
            menu.AddItem(new GUIContent("None"), property.managedReferenceValue == null, () =>
            {
                property.managedReferenceValue = null;
                property.serializedObject.ApplyModifiedProperties();
            });

            // inherited types
            foreach (Type type in GetClasses(t))
            {
                menu.AddItem(new GUIContent(type.Name), typeName == type.Name, () =>
                {
                    property.managedReferenceValue = type.GetConstructor(Type.EmptyTypes).Invoke(null);
                    property.serializedObject.ApplyModifiedProperties();
                });
            }
            menu.ShowAsContext();
        }
        EditorGUI.PropertyField(position, property, label, true);
    }
}