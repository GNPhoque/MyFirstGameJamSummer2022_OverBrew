using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Afflictions/Heal")]
public class HealAffliction : Affliction
{
    public float HealValue;
    public override void ApplyAffliction()
    {
        HeroController.Instance.Heal(HealValue);
    }

    public override void RemoveAffliction()
    {
        
    }
}
