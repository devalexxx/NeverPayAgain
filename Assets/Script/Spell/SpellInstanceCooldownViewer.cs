using UnityEngine.UI;
using UnityEngine;

public class SpellInstanceCooldownViewer : MonoBehaviour
{
    [SerializeReference] private SpellInstance _instance;

    [SerializeField] private Image _image;

    public SpellInstance Instance
    {
        get => _instance;
        set => _instance = value;
    }

    private void Awake()
    {
        _image = transform.Find("Mask").Find("Image").GetComponent<Image>();
    }

    private void Update()
    {
        if (_instance.TurnSinceEnable > 0)
        {
            if (_image.color.a > 0.75)
            {
                var color    = _image.color;
                color.a      = 0.75f;
                _image.color = color;
            }
        }
        else
        {
            if (_image.color.a < 1f)
            {
                var color    = _image.color;
                color.a      = 1f;
                _image.color = color;
            }
        }
    }   
}
