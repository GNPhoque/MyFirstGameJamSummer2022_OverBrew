using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Exposed
    
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        EnemySpawner.Instance.OnVictory += SaveScore;
        EnemySpawner.Instance.OnVictory += UnlockNextLevel;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    #endregion

    #region Main methods
    /*private void UpdateScore() {
        if (_enemySavedCount >= _thirdStar)
            _currentScore = 3;
        else if (_enemySavedCount >= _secondStar)
            _currentScore = 2;
        else if (_enemySavedCount >= _firstStar)
            _currentScore = 1;
    }*/

    public void SaveScore() {
        if (HeroController.Instance._currentStar > ScoresList.Instance.starsUnlocked[_currentSceneIndex]) {
            ScoresList.Instance.starsUnlocked[_currentSceneIndex] = HeroController.Instance._currentStar;
            SaveSystem.SaveScoresList();
        }
    }

    public void UnlockNextLevel() {
        if (_currentSceneIndex+1 < SceneManager.sceneCountInBuildSettings)
            ScoresList.Instance.starsUnlocked[_currentSceneIndex+1] = 0;
    }

    public void RestartLevel() {
        SceneManager.LoadScene(_currentSceneIndex);
    }

    public void launchNextLevel() {
        if (_currentSceneIndex+1 < SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(_currentSceneIndex+1);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }
    #endregion

    #region Private & Protected
    private int _currentSceneIndex;
    #endregion
}
