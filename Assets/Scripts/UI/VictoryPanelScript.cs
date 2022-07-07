using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPanelScript : MonoBehaviour
{
    #region Exposed
    
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        
    }

    void Start()
    {
        EnemySpawner.Instance.OnVictory += DisplayUI;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void DisplayUI() {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    #endregion

    #region Main methods
    
    #endregion

    #region Private & Protected
    
    #endregion
}
