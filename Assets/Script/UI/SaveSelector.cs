using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private Button       _newButton;

    void Start()
    {
        UpdateOption();

        _newButton.onClick.AddListener(() => 
        {
            GameManager.Instance.Save();
            GameManager.Instance.Load(Guid.Empty);
        });

        _dropdown.onValueChanged.AddListener(p_value => 
        {
            GameManager.Instance.Save();
            GameManager.Instance.Load(SaveManager.AvailableSaves[p_value]);
        });

        SaveManager.onSaveCreated += UpdateOption;
    }

    private void OnDestroy()
    {
        SaveManager.onSaveCreated -= UpdateOption;
    }

    private void UpdateOption()
    {
        var t_saves = SaveManager.AvailableSaves;
        _dropdown.options = t_saves.Select(t_save => new TMP_Dropdown.OptionData(t_save.ToString())).ToList();

        var t_index = t_saves.FindIndex(t_guid => t_guid == GameManager.Instance.Player.guid);
        _dropdown.SetValueWithoutNotify(t_index);
    }
}
