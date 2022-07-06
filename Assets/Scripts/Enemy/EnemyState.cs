using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void OnStateEnter(EnemyController enemy);
    public abstract void OnStateExit(EnemyController enemy);
    public abstract void OnStateUpdate(EnemyController enemy);
    public abstract void OnStateFixedUpdate(EnemyController enemy);
    public abstract void OnStateCollisionEnter(EnemyController enemy, Collision2D other);
    public abstract void OnStateTriggerEnter(EnemyController enemy, Collider2D other);
    public abstract void OnStateCollisionExit(EnemyController enemy, Collision2D other);
    public abstract void OnStateTriggerExit(EnemyController enemy, Collider2D other);
}
