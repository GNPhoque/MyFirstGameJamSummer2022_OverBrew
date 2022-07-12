using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrifiedAffliction : Affliction
{
    [SerializeField] private float _modifiedAttackDelay;

    public override void ApplyAffliction()
    {
        HeroController.Instance._currentAttackDelay = _modifiedAttackDelay;
    }

    public override void RemoveAffliction()
    {
        HeroController.Instance._currentAttackDelay = HeroController.Instance.AttackDelay;
    }
}
