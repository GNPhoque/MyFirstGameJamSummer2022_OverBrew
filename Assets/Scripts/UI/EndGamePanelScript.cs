using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanelScript : MonoBehaviour
{
    #region Exposed
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private Transform _victoryStarsPanel;
    [SerializeField] private GameObject _defeatPanel;
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        
    }

    void Start()
    {
        EnemySpawner.Instance.OnVictory += DisplayVictoryUI;
        HeroController.Instance.OnDefeat += DisplayDefeatUI;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void DisplayVictoryUI() {
        for (int i = 0; i < HeroController.Instance._currentStar; i++) {
            _victoryStarsPanel.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
        _victoryPanel.SetActive(true);
    }

    public void DisplayDefeatUI() {
        _defeatPanel.SetActive(true);
    }

    #endregion

    #region Main methods
    
    #endregion

    #region Private & Protected
    
    #endregion
}
