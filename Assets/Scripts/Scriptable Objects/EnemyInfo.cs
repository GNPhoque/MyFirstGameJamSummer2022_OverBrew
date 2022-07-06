using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyInfo : ScriptableObject
{
    public string _enemyName;
    public EnemyType _enemyType;
    public Sprite _imageType;
}
