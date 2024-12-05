using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// [Serializable]
public class ChampionLibrary
{
    private List<ChampionBehaviour> _behaviours;

    public ChampionLibrary()
    {
        // string[] guids = AssetDatabase.FindAssets("t:" + typeof(ChampionBehaviour).FullName);
        // string[] paths = guids.Select(AssetDatabase.GUIDToAssetPath).ToArray();
        // _behaviours    = paths.Select(AssetDatabase.LoadAssetAtPath<ChampionBehaviour>).ToList();

        _behaviours = Resources.LoadAll<ChampionBehaviour>("Object/Champion").ToList();
    }

    public ChampionBehaviour GetItem(int index)
    {
        if (_behaviours.Count > index)
        {
            return _behaviours[index];
        }
        else
        {
            return null;
        }
    }

    public void ForEach(Action<ChampionBehaviour> action)
    {
        _behaviours.ForEach(action);
    }
}
