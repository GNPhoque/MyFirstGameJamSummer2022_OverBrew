using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Exposed
    [SerializeField] private SpriteRenderer _imageType;
    [SerializeField] private float _speed = 0.5f;
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        this.TransitionToState(_walkingState);
    }

    void Start()
    {
        
    }

    void Update()
    {
        _currentState.OnStateUpdate(this);
    }

    void FixedUpdate()
    {
        _currentState.OnStateFixedUpdate(this);
    }

    void OnCollisionEnter2D(Collision2D other) {
        _currentState.OnStateCollisionEnter(this, other);
    }

    void OnCollisionExit2D(Collision2D other) {
        _currentState.OnStateCollisionExit(this, other);
    }

    void OnTriggerEnter2D(Collider2D other) {
        _currentState.OnStateTriggerEnter(this, other);
    }

    void OnTriggerExit2D(Collider2D other) {
        _currentState.OnStateTriggerExit(this, other);
    }
    #endregion

    #region Main methods
    public void TransitionToState(EnemyState state) {
        if (_currentState != null) _currentState.OnStateExit(this);
        _currentState = state;
        _currentState.OnStateEnter(this);
    }

    public void Walk() {
        _rb.velocity = new Vector2(-1*_speed,0);
    }

    public void Stop() {
        _rb.velocity = Vector2.zero;
    }

    public void ChangeColor(Color color) {
        this.GetComponentInChildren<SpriteRenderer>().color = color;
    } 

    public void SetParameters(EnemyInfo info) {
        this._name = info._enemyName;
        this._type = info._enemyType;
        this._health = info._enemyHealth;
        this._damage = info._enemyDamage;
        this._attackDelay = info._enemyAttackDelay;
        
        if (info._imageType != null) {
            this._imageType.enabled = true;
            this._imageType.sprite = info._imageType;
        } 
    }
    #endregion

    #region Private & Protected
    private string _name;
    private EnemyType _type;
    private float _health;
    private float _damage;
    private float _attackDelay;
    private Rigidbody2D _rb;
    private EnemyState _currentState;
    public EnemyState _idleState = new IdleEnemyState();
    public EnemyState _walkingState = new WalkingEnemyState();
    public EnemyState _attackingState = new AttackingEnemyState();
    public EnemyState _dyingState = new DyingEnemyState();
    #endregion
}
