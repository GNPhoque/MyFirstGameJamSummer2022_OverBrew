using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemyState : EnemyState
{
    public override void OnStateCollisionEnter(EnemyController enemy, Collision2D other)
    {
        
    }

    public override void OnStateCollisionExit(EnemyController enemy, Collision2D other)
    {
        
    }

    public override void OnStateEnter(EnemyController enemy)
    {
        enemy.ChangeColor(Color.red);
        enemy.Stop();
        enemy.AffectHero();
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
        enemy.AttackHero();
        if(enemy.Health <= 0f)
        {
            enemy.TransitionToState(enemy._dyingState);
        }
    }
}
