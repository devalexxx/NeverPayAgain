using UnityEngine;
using UnityEngine.Events;

// Acts as a proxy to handle animation-related events for a Champion.
public class ChampionAnimationProxy : MonoBehaviour
{
    public UnityEvent onAttackHit;  // Event triggered when an attack animation hits its target.
    public Animator   Animator;     // Reference to the Animator component controlling the champion's animations.

    private void Awake()
    {
        Animator    = GetComponent<Animator>();
        onAttackHit = new();
    }

    // Invokes the onAttackHit event. This method can be called by animation event callbacks.
    private void OnAttackHit()
    {
        onAttackHit.Invoke();
    }
}
