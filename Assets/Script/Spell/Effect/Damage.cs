using System;
using System.Collections;
using UnityEngine;

// Abstract class for damage effects. Represents a damage effect applied to targets.
[Serializable]
public abstract class DamageEffect : SpellEffect
{
    // The percentage of the champion's damage to deal. Ranges from 0 to 10.
    [Range(0.0f, 10.0f)]
    [SerializeField] protected float _percent;
}

// A damage effect that targets a single enemy.
public class DamageTarget : DamageEffect
{
    // Deals damage to the specified target. The amount of damage is based on the caster's damage and the _percent modifier.
    public override IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        bool hasBeenNotify = false;

        // Notify method to trigger when an attack hits the target.
        void Notify() 
        {
            target.Entity.NotifyTakeDamage();
            hasBeenNotify = true;
        }

        // Add the listener to the onAttackHit event to notify when the attack animation triggers.
        self.Entity.AnimationProxy.onAttackHit.AddListener(Notify);
        self.Entity.AnimationProxy.Animator.SetTrigger("Attack");

        // Deal the damage to the target.
        target.Health -= self.Champion.Attributes.Damage * _percent;

        // Wait for the animation to finish.
        yield return new WaitUntil(() => !self.Entity.AnimationProxy.Animator.IsInTransition(0));
        yield return new WaitUntil(() =>  self.Entity.AnimationProxy.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        // Remove the listener after the attack.
        self.Entity.AnimationProxy.onAttackHit.RemoveListener(Notify);

        // If the attack animation never notified, force the notification and log a warning.
        if (!hasBeenNotify)
        {
            Debug.LogWarning("[DamageTarget] An attack animation should have OnAttackHit event bind to ChampionAnimatorProxy.");
            Notify();
        }

        yield return true;
    }
}

// A damage effect that targets all enemies in the crew.
public class DamageAll : DamageEffect
{
    // Deals damage to all enemies in the enemy crew, based on the caster's damage and the _percent modifier.
    public override IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        bool hasBeenNotify = false;

        // Notify method to trigger when an attack hits any enemy.
        void Notify()
        {
            enemies.ForEach(inst => inst.Entity.NotifyTakeDamage());
            hasBeenNotify = true;
        }

        // Add the listener to the onAttackHit event to notify when the attack animation triggers.
        self.Entity.AnimationProxy.onAttackHit.AddListener(Notify);
        self.Entity.AnimationProxy.Animator.SetTrigger("Attack");

        // Deal the damage to all enemies.
        enemies.ForEach(inst => inst.Health -= self.Champion.Attributes.Damage * _percent);

        // Wait for the animation to finish.
        yield return new WaitUntil(() => !self.Entity.AnimationProxy.Animator.IsInTransition(0));
        yield return new WaitUntil(() =>  self.Entity.AnimationProxy.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        // Remove the listener after the attack.
        self.Entity.AnimationProxy.onAttackHit.RemoveListener(Notify);

        // If the attack animation never notified, force the notification and log a warning.
        if (!hasBeenNotify)
        {
            Debug.LogWarning("[DamageTarget] An attack animation should have OnAttackHit event bind to ChampionAnimatorProxy.");
            Notify();
        }

        yield return true;
    }
}
