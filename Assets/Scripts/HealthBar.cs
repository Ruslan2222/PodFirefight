using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fillHealth;

    public void SetMaxHealth(int health)
    {
        _healthBar.maxValue = health;
        _healthBar.value = health;
        _fillHealth.color = _gradient.Evaluate(1f);
    }

    public void SetHealtlh(int health)
    {
        _healthBar.value = health;
        _fillHealth.color = _gradient.Evaluate(_healthBar.normalizedValue);
    }
}
