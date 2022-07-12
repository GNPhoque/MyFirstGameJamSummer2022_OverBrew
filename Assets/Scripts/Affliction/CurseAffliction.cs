using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Afflictions/Curse")]
public class CurseAffliction : Affliction
{
    public override void ApplyAffliction()
    {
        HeroController.Instance._isCursed = true;
    }

    public override void RemoveAffliction()
    {
        HeroController.Instance._isCursed = false;
    }
}
