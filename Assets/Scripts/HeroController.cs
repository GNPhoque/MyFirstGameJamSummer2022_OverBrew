using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour,IInteractable
{
    #region Exposed
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private LayerMask _enemyLayerMask;
    #endregion

    #region Getters
    public float AttackDelay { get => _attackDelay; }
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
    public event Action OnHeroUpdate;
    public event Action<List<Affliction>, List<Affliction>> OnEffectApplied;
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);

        _transform = transform;
        _maxHealth = _health;

        _boxCollider = GetComponent<BoxCollider2D>();

        // initalise values
        _currentAttackDelay = _attackDelay;
        _currentDamage = _damage;
    }

    void Start()
    {
    }

    void Update()
    {
        OnHeroUpdate?.Invoke();

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

    public void TakeDamage(float damage)
    {
        HealthModification(damage * _currentDamageTakenModifier);
    }
    

    public void Heal(float value)
    {
        HealthModification(-value /** _currentHealModifier*/);
    }

    private void HealthModification(float value) {
        _health -= value;
         _health = Mathf.Clamp(_health, 0f, _maxHealth);
        OnHealthChange?.Invoke(_health,_maxHealth);
    }

    public void TakeAffliction(Affliction affliction) {
        if (affliction is HealAffliction)
        {
            affliction.ApplyAffliction();
            return;
        }
        if (_protectionList.Contains(affliction)) {
            _protectionList.Remove(affliction);

        } else if (!_afflictionList.Contains(affliction)) {
            _afflictionList.Add(affliction);
            affliction.ApplyAffliction();
        }
        OnEffectApplied?.Invoke(_protectionList, _afflictionList);
    }

    public void TakeProtection(Affliction protection) {
        if (protection is HealAffliction)
        {
            protection.RemoveAffliction();
            return;
        }
        if (_afflictionList.Contains(protection)) {
            _afflictionList.Remove(protection);
            protection.RemoveAffliction();

        } else if (!_protectionList.Contains(protection)) {
            _protectionList.Add(protection);
        }
        OnEffectApplied?.Invoke(_protectionList, _afflictionList);
    }

    public void AttackEnemy(EnemyController enemyController)
    {
        if (Time.time - _lastAttackTime >= _currentAttackDelay)
        {
            enemyController.TakeDamage(_currentDamage);
            _lastAttackTime = Time.time;
        }
    }

    public void Use()
    {
        Item potion = HealerController.instance.CarriedItem;
        if (potion !=null && potion is Potion)
        {
            Affliction protection = ((Potion)potion).protection;
            if (_isCursed && !(protection is CurseAffliction)) TakeAffliction(protection);
            else TakeProtection(protection);

            HealerController.instance.DropItem();
        }
    }

    public void Release(){}
    public void Clean(){}

    #endregion

    #region Main methods

    #endregion

    #region Private & Protected
    private List<Affliction> _afflictionList = new List<Affliction>();
    private List<Affliction> _protectionList = new List<Affliction>();
    private float _maxHealth;
    private float _lastAttackTime;
    [HideInInspector] public bool _isCursed = false;
    private Transform _transform;
    private BoxCollider2D _boxCollider;
    private EnemyController _currentEnemyController;
    [HideInInspector] public float _currentAttackDelay;
    [HideInInspector] public float _currentDamage;
    //[HideInInspector] public float _currentHealModifier = -1;
    [HideInInspector] public float _currentDamageTakenModifier = 1;
    #endregion
}
