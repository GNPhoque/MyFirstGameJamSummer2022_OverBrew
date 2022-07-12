using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeroHealthBarUIScript : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    void Start()
    {
        HeroController.Instance.OnHealthChange += updateUI;
    }

    public void updateUI(float health, float maxHealth)
    {
        _fillImage.fillAmount = health / maxHealth;
    }
}
