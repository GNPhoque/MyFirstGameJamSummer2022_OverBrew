using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Exposed
    [SerializeField] private SpriteRenderer _imageType;
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void SetParameters(EnemyInfo info) {
        this._name = info._enemyName;
        this._type = info._enemyType;
        if (info._imageType != null) {
            this._imageType.enabled = true;
            this._imageType.sprite = info._imageType;
        } 
    }

    #endregion

    #region Main methods
    
    #endregion

    #region Private & Protected
    private string _name;
    private EnemyType _type;
    #endregion
}
