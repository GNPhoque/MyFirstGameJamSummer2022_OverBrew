using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Afflictions/Freez")]
public class FreezAfflication : Affliction
{
    [SerializeField] private float _damageTakenModifier;

    public override void ApplyAffliction()
    {
        HeroController.Instance._currentDamageTakenModifier = _damageTakenModifier;
    }

    public override void RemoveAffliction()
    {
        HeroController.Instance._currentDamageTakenModifier = 1;
    }
}
