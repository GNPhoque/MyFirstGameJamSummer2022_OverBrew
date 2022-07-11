using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Affliction : ScriptableObject
{
    public Sprite _afflictionImage;
    public abstract void ApplyAffliction();
    public abstract void RemoveAffliction();
    /*public void ApplyAffliction() {
        HeroController.Instance.AfflictionEffects += AfflictionEffect;
    }*/

    public override bool Equals(object other)
    {
        return this.GetType() == other.GetType();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
