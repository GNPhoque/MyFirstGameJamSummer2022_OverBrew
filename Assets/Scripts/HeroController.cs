using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    #region Exposed
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private Image _filleImage;
    #endregion

    #region Singleton
    private static HeroController _instance;
    public static HeroController Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    #region Event
    public event Action<float,float> OnHealthChange;
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);

        _transform = transform;
        _maxHealth = _health;

        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
    }

    void Update()
    {
        float raycastLength = _boxCollider.size.x * 0.5f+.1f;
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.right, raycastLength, _enemyLayerMask);

        if (hit.collider != null)
        {
            if (_currentEnemyController == null)
            {
                _currentEnemyController = hit.collider.GetComponent<EnemyController>();
            }
            AttackEnemy(_currentEnemyController);
        }
        else
        {
            _currentEnemyController = null;
        }

    }

    void FixedUpdate()
    {

    }

    public void TakeDamage(float damages)
    {
        _health -= damages;
        _health = Mathf.Clamp(_health, 0f, _maxHealth);
        OnHealthChange?.Invoke(_health,_maxHealth);

    }

    public void Heal(float value)
    {
        TakeDamage(value * -1);
    }

    public void AttackEnemy(EnemyController enemyController)
    {
        if (Time.time - _lastAttackTime >= _attackDelay)
        {
            enemyController.TakeDamage(_damage);
            _lastAttackTime = Time.time;
        }
    }


    #endregion

    #region Main methods

    #endregion

    #region Private & Protected
    private float _maxHealth;
    private float _lastAttackTime;
    private Transform _transform;
    private BoxCollider2D _boxCollider;
    private EnemyController _currentEnemyController;
    #endregion
}
