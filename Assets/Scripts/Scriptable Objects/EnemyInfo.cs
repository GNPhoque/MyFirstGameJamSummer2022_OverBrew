using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyInfo : ScriptableObject
{
    public string _enemyName;
    public EnemyType _enemyType;
    public float _enemyHealth;
    public float _enemyDamage;
    public float _enemyAttackDelay;
    public Sprite _imageType;
}
