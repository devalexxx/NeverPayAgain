using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabBarController : MonoBehaviour
{
    [Serializable]
    public struct Link
    {
        public Button     button;
        public GameObject panel;
    }

    [SerializeField]
    private List<Link> _links;

    private void Awake()
    {
        _links.ForEach(t_link => {
            t_link.button.onClick.AddListener(() => OnClick(t_link.panel));
        });
    }

    private void Start()
    {
        OnClick(_links[0].panel);
    }

    private void OnClick(GameObject p_target)
    {
        if (!p_target.activeSelf)
        {
            _links.ForEach(t_link => t_link.panel.SetActive(false));
            p_target.SetActive(true);
        }
    }
}
