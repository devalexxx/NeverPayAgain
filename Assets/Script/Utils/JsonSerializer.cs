using System;
using UnityEngine;

public interface IJsonSerializable
{
    void OnBeforeSerialization() {}

    void OnAfterSerialization  () {}
    void OnAfterDeserialization() {}
}

public static class JsonSerializer
{
    public static string ToJson<T>(T p_handle) where T: IJsonSerializable
    {
        Caller(p_handle, p_ser => p_ser.OnBeforeSerialization());
        var t_json = JsonUtility.ToJson(p_handle, true);
        Caller(p_handle, p_ser => p_ser.OnAfterSerialization());
        return t_json;
    }

    public static T FromJson<T>(string p_json) where T: IJsonSerializable
    {
        T t_handle = JsonUtility.FromJson<T>(p_json);
        Caller(t_handle, t_ser => t_ser.OnAfterDeserialization());
        return t_handle;
    }

    private static void Caller(object p_object, Action<IJsonSerializable> p_action)
    {
        if (p_object == null)
            return;

        if (p_object is IJsonSerializable t_ser)
        {
            p_action(t_ser);
        }

        if (p_object is System.Collections.IEnumerable t_enumerable && p_object is not string)
        {
            foreach (var t_item in t_enumerable)
            {
                Caller(t_item, p_action);
            }
        }
        else
        {
            var t_type = p_object.GetType();
            if (t_type.IsClass && p_object is not string)
            {
                var fields = t_type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                foreach (var field in fields)
                {
                    var value = field.GetValue(p_object);
                    if (value != null)
                    {
                        Caller(value, p_action);
                    }
                }
            }
        }
    }
}
