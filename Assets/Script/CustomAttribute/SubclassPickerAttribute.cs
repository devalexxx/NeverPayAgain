using System;
using UnityEngine;

// @TODO: Remove from game build (#if UNITY_EDITOR or [System.Diagnostics.Conditional("UNITY_EDITOR")]) 

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class SubclassPickerAttribute : PropertyAttribute { }
