using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface IReferencableAsset
{
    string GetAssetPath();
}

[Serializable]
public class AssetReference<T> : IJsonSerializable where T: UnityEngine.Object, IReferencableAsset
{
    [SerializeField] private string _path;
    [NonSerialized]  public  T      handle;

    public bool loaded => handle.IsUnityNull();

    public AssetReference(T p_handle)
    {
        _path  = p_handle.GetAssetPath();
        handle = p_handle;
    }

    public void Load()
    {
        handle = Resources.Load<T>(_path);
    }

    public IEnumerator LoadAsync()
    {
        var t_request = Resources.LoadAsync<T>(_path);
        yield return t_request;
        handle = t_request.asset as T;
    }

    public void OnAfterDeserialization()
    {
        Load();
    }
}
