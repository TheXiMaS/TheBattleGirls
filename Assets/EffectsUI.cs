using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsUI : MonoBehaviour
{
    [SerializeField] private HealthAI healthAI;
    [SerializeField] private GameObject emptyImage;

    [SerializeField] private List<Effect> effects;

    private void Update()
    {
        if (effects == null) effects = healthAI.GetActiveEffects();
    }

    public void UpdateUI()
    {
        if (gameObject.transform.childCount != 0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }

        foreach (Effect effect in effects)
        {
            if (effect == null) return;

            var drawedEffect = Instantiate(emptyImage, transform);

            var itemSpite = effect.GetSprite();
            drawedEffect.GetComponent<Image>().sprite = itemSpite;
        }
    }
}
