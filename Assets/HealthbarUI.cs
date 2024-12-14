using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    private Entity entity;
    private RectTransform myTransform;
    private Slider slider;
    private CharacterStats myStats;

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        myStats = GetComponentInParent<CharacterStats>();

        entity.onFlipped += FlipUI;
        myStats.onHealthChanged += UpdateHealthUI;
        UpdateHealthUI();
    }
    private void Update()
    {
        UpdateHealthUI();
    }
    private void UpdateHealthUI()
    {

        slider.maxValue = myStats.GetMaxHP();
        slider.value = myStats.currentHealth;
    }

    private void FlipUI()
    {
        myTransform.Rotate(0, 180, 0);
    }

  

    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        myStats.onHealthChanged -= UpdateHealthUI;
    }
}
