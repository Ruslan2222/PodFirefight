using UnityEngine;
using UnityEngine.UI;

public class UltraBar : MonoBehaviour
{
    [SerializeField] private Slider _ultraBar;
    [SerializeField] private Image _fillHealth;

    public void SetUltra(int ultra)
    {
        _ultraBar.value = ultra;
    }
}
