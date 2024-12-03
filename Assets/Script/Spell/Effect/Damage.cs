using System;
using System.Collections;
using UnityEngine;

[Serializable]
public abstract class DamageEffect : SpellEffect
{
    [Range(0.0f, 10.0f)]
    [SerializeField] protected float _percent;
}

public class DamageTarget : DamageEffect
{
    public override IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        bool hasBeenNotify = false;
        void Notify() 
        {
            target.Entity.NotifyTakeDamage();
            hasBeenNotify = true;
        }

        self.Entity.AnimationProxy.onAttackHit.AddListener(Notify);
        self.Entity.AnimationProxy.Animator.SetTrigger("Attack");

        target.Health -= self.Champion.Attributes.Damage * _percent;

        yield return new WaitUntil(() => !self.Entity.AnimationProxy.Animator.IsInTransition(0));
        yield return new WaitUntil(() =>  self.Entity.AnimationProxy.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        self.Entity.AnimationProxy.onAttackHit.RemoveListener(Notify);

        if (!hasBeenNotify)
        {
            Debug.LogWarning("[DamageTarget] An attack animation should have OnAttackHit event bind to ChampionAnimatorProxy.");
            Notify();
        }

        yield return true;
    }
}

public class DamageAll : DamageEffect
{
    public override IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        bool hasBeenNotify = false;
        void Notify()
        {
            enemies.ForEach(inst => inst.Entity.NotifyTakeDamage());
            hasBeenNotify = true;
        }

        self.Entity.AnimationProxy.onAttackHit.AddListener(Notify);
        self.Entity.AnimationProxy.Animator.SetTrigger("Attack");

        enemies.ForEach(inst => inst.Health -= self.Champion.Attributes.Damage * _percent);

        yield return new WaitUntil(() => !self.Entity.AnimationProxy.Animator.IsInTransition(0));
        yield return new WaitUntil(() =>  self.Entity.AnimationProxy.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        self.Entity.AnimationProxy.onAttackHit.RemoveListener(Notify);

        if (!hasBeenNotify)
        {
            Debug.LogWarning("[DamageTarget] An attack animation should have OnAttackHit event bind to ChampionAnimatorProxy.");
            Notify();
        }

        yield return true;
    }
}
