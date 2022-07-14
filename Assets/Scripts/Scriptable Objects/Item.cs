using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : ScriptableObject
{
    private int id;
    public string itemName;
    public Sprite itemSprite;
    [HideInInspector] public Transform itemTransform;

    public override bool Equals(object other)
    {
        if (other is Item)
        {
            return itemName == ((Item)other).itemName;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return itemName;
    }
}
