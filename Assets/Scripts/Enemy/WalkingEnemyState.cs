using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyState : EnemyState
{
    public override void OnStateCollisionEnter(EnemyController enemy, Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            enemy.TransitionToState(enemy._idleState);
        } else if (other.gameObject.CompareTag("Hero")) {
            enemy.TransitionToState(enemy._attackingState);
        }
    }

    public override void OnStateCollisionExit(EnemyController enemy, Collision2D other)
    {
        
    }

    public override void OnStateEnter(EnemyController enemy)
    {
        enemy.ChangeColor(Color.green);
        enemy.Walk();
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
