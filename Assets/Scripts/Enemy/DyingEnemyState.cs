using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingEnemyState : EnemyState
{
    public override void OnStateCollisionEnter(EnemyController enemy, Collision2D other)
    {

    }

    public override void OnStateCollisionExit(EnemyController enemy, Collision2D other)
    {

    }

    public override void OnStateEnter(EnemyController enemy)
    {
        enemy.Die();
    }

    public override void OnStateExit(EnemyController enemy)
    {

    }

    public override void OnStateFixedUpdate(EnemyController enemy)
    {

    }

    public override void OnStateTriggerEnter(EnemyController enemy, Collider2D other)
    {

    }

    public override void OnStateTriggerExit(EnemyController enemy, Collider2D other)
    {

    }

    public override void OnStateUpdate(EnemyController enemy)
    {

    }
}
