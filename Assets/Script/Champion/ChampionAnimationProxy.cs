using UnityEngine;
using UnityEngine.Events;

public class ChampionAnimationProxy : MonoBehaviour
{
    public UnityEvent onAttackHit;
    public Animator   Animator;

    private void Awake()
    {
        Animator    = GetComponent<Animator>();
        onAttackHit = new();
    }

    private void OnAttackHit()
    {
        onAttackHit.Invoke();
    }
}
