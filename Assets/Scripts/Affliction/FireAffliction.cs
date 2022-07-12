using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Afflictions/Fire")]
public class FireAffliction : Affliction
{
    [SerializeField] private float _fireDamage;
    [SerializeField] private float _fireDelay;
    [SerializeField] private float _damageTakenModifier;

    public override void ApplyAffliction()
    {
        _lastTickTime = Time.time;
        HeroController.Instance._currentDamageTakenModifier = _damageTakenModifier;
        HeroController.Instance.OnHeroUpdate += FireAfflictionEffect;
    }

    public override void RemoveAffliction()
    {
        HeroController.Instance._currentDamageTakenModifier = 1;
        HeroController.Instance.OnHeroUpdate -= FireAfflictionEffect;
    }

    private void FireAfflictionEffect()
    {
        if (Time.time - _lastTickTime >= _fireDelay) {
            HeroController.Instance.TakeDamage(_fireDamage);
            _lastTickTime = Time.time;
        }
    }

    private float _lastTickTime;
}
