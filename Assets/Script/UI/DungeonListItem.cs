using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonListItem : MonoBehaviour
{
    public event Action onClick;

    [SerializeField] private Image           _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _selectedColor;

    private Image  _selfImage;
    private Button _selfButton;

    private void Awake()
    {
        _selfImage  = GetComponent<Image>();
        _selfButton = GetComponent<Button>();

        _selfButton.onClick.AddListener(() => onClick?.Invoke());
    }

    public void Setup(Dungeon p_dungeon)
    {
        _image      .sprite = p_dungeon.Behaviour.DisplayIcon;
        _name       .text   = p_dungeon.Behaviour.DisplayName;
        _description.text   = p_dungeon.Behaviour.DisplayDescription;
    }

    public void SetSelected(bool p_selected)
    {
        _selfImage.color = p_selected ? _selectedColor : _defaultColor;
    }
}
