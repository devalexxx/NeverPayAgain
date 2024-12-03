using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    [SerializeField] private float _percent;

    [SerializeField] private Image _bar;

    public float Percent
    {
        get => _percent;
        set 
        {
            _percent = Math.Max(Math.Min(100.0f, value), 0.0f);
            OnPercentUpdate();
        }
    }

    private void Awake()
    {
        Percent = 0.0f;
    }

    private void OnPercentUpdate()
    {
        _bar.fillAmount = _percent;
    }
}
