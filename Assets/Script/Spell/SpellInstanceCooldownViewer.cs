using UnityEngine.UI;
using UnityEngine;

// Handles displaying and updating the cooldown of a spell instance visually.
public class SpellInstanceCooldownViewer : MonoBehaviour
{
    // The spell instance whose cooldown we are displaying.
    [SerializeReference] private SpellInstance _instance;

    // The UI Image component that represents the cooldown.
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
        // If the spell is on cooldown (turns since enable > 0), update the image alpha to indicate cooldown.
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
