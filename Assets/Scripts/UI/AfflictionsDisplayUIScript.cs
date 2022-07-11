using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfflictionsDisplayUIScript : MonoBehaviour
{
    #region Exposed
    [SerializeField] private GameObject _afflictionPrefab;
    [SerializeField] private GameObject _protectionPrefab;
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        _transform = transform;
    }

    void Start()
    {
        HeroController.Instance.OnEffectApplied += UpdateUI;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    #endregion

    #region Main methods
    public void UpdateUI(List<Affliction> protections, List<Affliction> afflictions) {
        
        // clean
        foreach (Transform child in _transform) {
            Destroy(child.gameObject);
        }

        foreach (Affliction protection in protections) {
            GameObject effect = Instantiate(_protectionPrefab, _transform);
            effect.GetComponent<Image>().sprite = protection._afflictionImage;
        }
        foreach (Affliction affliction in afflictions) {
            GameObject effect = Instantiate(_afflictionPrefab, _transform);
            effect.GetComponent<Image>().sprite = affliction._afflictionImage;
        }
    }
    #endregion

    #region Private & Protected
    private Transform _transform;
    #endregion
}
