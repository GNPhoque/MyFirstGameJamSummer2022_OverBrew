using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeroHealthBarUIScript : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Transform _indicatorsContainer;

    void Start()
    {
        HeroController.Instance.OnHealthChange += updateUI;

        // position for indicators
        float fillHeight = _fillImage.GetComponent<RectTransform>().rect.height;
        float[] starPositionsY = new float[3];
        starPositionsY[0] = HeroController.Instance._firstStarPercent * fillHeight;
        starPositionsY[1] = HeroController.Instance._secondStarPercent * fillHeight;
        starPositionsY[2] = HeroController.Instance._thirdStarPercent * fillHeight;

        for (int i = 0; i < 3; i++) {
            Transform indicatorTransform = _indicatorsContainer.GetChild(i);
            Vector3 newPosition = new Vector3(indicatorTransform.localPosition.x, indicatorTransform.localPosition.y+starPositionsY[i], indicatorTransform.localPosition.z);
            indicatorTransform.localPosition = newPosition;
        }
    }

    public void updateUI(float health, float maxHealth)
    {
        _fillImage.fillAmount = health / maxHealth;
    }
}
