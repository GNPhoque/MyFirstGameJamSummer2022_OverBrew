using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemyState : EnemyState
{
    public override void OnStateCollisionEnter(EnemyController enemy, Collision2D other)
    {
        
    }

    public override void OnStateCollisionExit(EnemyController enemy, Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            enemy.TransitionToState(enemy._walkingState);
        }
    }

    public override void OnStateEnter(EnemyController enemy)
    {
        enemy.ChangeColor(Color.yellow);
    }

    public override void OnStateExit(EnemyController enemy)
    {
        
    }

    public override void OnStateFixedUpdate(EnemyController enemy)
    {
        enemy.Stop();
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
