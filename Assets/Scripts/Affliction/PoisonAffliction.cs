using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Afflictions/Poison")]
public class PoisonAffliction : Affliction
{
    [SerializeField] private float _poisonDamage;
    [SerializeField] private float _poisonDelay;

    public override void ApplyAffliction()
    {
        _lastTickTime = Time.time;
        HeroController.Instance.OnHeroUpdate += PoisonAfflictionEffect;
    }

    public override void RemoveAffliction()
    {
        HeroController.Instance.OnHeroUpdate -= PoisonAfflictionEffect;
    }

    private void PoisonAfflictionEffect()
    {
        if (Time.time - _lastTickTime >= _poisonDelay) {
            HeroController.Instance.TakeDamage(_poisonDamage);
            _lastTickTime = Time.time;
        }
    }

    private float _lastTickTime;
}
