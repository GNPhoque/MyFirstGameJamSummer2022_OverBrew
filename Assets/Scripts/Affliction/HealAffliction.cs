using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Afflictions/Heal")]
public class HealAffliction : Affliction
{
    public float HealValue;
    public override void ApplyAffliction()
    {
        HeroController.Instance.TakeDamage(HealValue);
    }

    public override void RemoveAffliction()
    {
        HeroController.Instance.Heal(HealValue);
    }
}
