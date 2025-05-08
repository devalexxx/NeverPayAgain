using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// Manages a collection of ChampionBehaviour objects, providing methods to access and iterate over them.
public class ChampionLibrary
{
    private List<ChampionBehaviour> _behaviours;

    public ChampionLibrary()
    {
        // string[] guids = AssetDatabase.FindAssets("t:" + typeof(ChampionBehaviour).FullName);
        // string[] paths = guids.Select(AssetDatabase.GUIDToAssetPath).ToArray();
        // _behaviours    = paths.Select(AssetDatabase.LoadAssetAtPath<ChampionBehaviour>).ToList();

        // The following line loads ChampionBehaviour assets directly from the Resources folder.
        _behaviours = Resources.LoadAll<ChampionBehaviour>("Champion/Object").ToList();
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
