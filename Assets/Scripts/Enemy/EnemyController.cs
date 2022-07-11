using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    #region Exposed
    [SerializeField] private float _health = 10f;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _attackDelay = 1f;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _afflictionImage;
    #endregion

    #region Event
    private event Action<float> OnHealthChange;
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        this._maxHealth = this._health;
        _rb = this.GetComponent<Rigidbody2D>();
        _boxCollider = this.GetComponent<BoxCollider2D>();
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

    void OnCollisionEnter2D(Collision2D other)
    {
        _currentState.OnStateCollisionEnter(this, other);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        _currentState.OnStateCollisionExit(this, other);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _currentState.OnStateTriggerEnter(this, other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _currentState.OnStateTriggerExit(this, other);
    }
    #endregion

    #region Main methods
    public void TransitionToState(EnemyState state)
    {
        if (_currentState != null) _currentState.OnStateExit(this);
        _currentState = state;
        _currentState.OnStateEnter(this);
    }

    public void Walk()
    {
        _rb.velocity = new Vector2(-1 * _speed, 0);
    }

    public void Stop()
    {
        _rb.velocity = Vector2.zero;
    }

    public void ChangeColor(Color color)
    {
        this.GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public void TakeDamage(float damages)
    {
        _health -= damages;
        OnHealthChange?.Invoke(_health);
        _fillImage.fillAmount = _health / _maxHealth;
    }

    public void AttackHero()
    {
        if (Time.time - _lastAttackTime >= _attackDelay)
        {
            HeroController.Instance.TakeDamage(_damage);
            _lastAttackTime = Time.time;
        }
    }

    public void AffectHero() {
        if (_affliction != null) {
            HeroController.Instance.TakeAffliction(_affliction);
            this._afflictionImage.enabled = false;
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void SetParameters(EnemyInfo info)
    {
        this.gameObject.name = this._name = info._enemyName;
        //this._type = info._afflictionType;
        if (info._affliction != null) {
        _affliction = info._affliction;
        this._afflictionImage.enabled = true;
        this._afflictionImage.sprite = _affliction._afflictionImage;
        }
        if (info._enemyHealth != 0f) this._maxHealth = this._health = info._enemyHealth;
        if (info._enemyDamage != 0f) this._damage = info._enemyDamage;
        if (info._enemyAttackDelay != 0f) this._attackDelay = info._enemyAttackDelay;
        _boxCollider.size = info._enemySize != 0f ? new Vector2(info._enemySize, _boxCollider.size.y) : _boxCollider.size;
    }
    #endregion

    #region Private & Protected
    private string _name;
    //private AfflictionType _type;
    private Affliction _affliction;
    private float _maxHealth;
    private float _lastAttackTime;
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private EnemyState _currentState;
    public EnemyState _idleState = new IdleEnemyState();
    public EnemyState _walkingState = new WalkingEnemyState();
    public EnemyState _attackingState = new AttackingEnemyState();
    public EnemyState _dyingState = new DyingEnemyState();
    #endregion

    #region Getters
    public float Health { get => _health; }
    #endregion
}
