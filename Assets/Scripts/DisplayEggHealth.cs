using UnityEngine;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine.UI;

public class DisplayEggHealth : MonoBehaviour
{
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Sprite[] _healthImages;
    [SerializeField] private Sprite[] _deathImages;
    [SerializeField] private Image _healthImage;
    [SerializeField] private Image _deathImage;

    private void OnEnable()
    {
        _eggHealth.AddListener(UpdateHealth);
        UpdateHealth();
    }

    private void OnDisable()
    {
        _eggHealth.RemoveListener(UpdateHealth);
    }

    private void UpdateHealth()
    {
        healthText.text = Mathf.RoundToInt(_eggHealth.Value).ToString();

        if (_eggHealth.Value / 100 >= 0.75f)
        {
            _healthImage.gameObject.SetActive(false);

            return;
        }

        if (_eggHealth.Value / 100 >= 0.5f)
        {
            _healthImage.gameObject.SetActive(true);
            _healthImage.sprite = _healthImages[0];

            return;
        }

        if (_eggHealth.Value / 100 >= 0.25f)
        {
            _healthImage.gameObject.SetActive(true);
            _healthImage.sprite = _healthImages[1];

            return;
        }
        
        _healthImage.gameObject.SetActive(true);
        _healthImage.sprite = _healthImages[3];
    }

    private void OnHeatDeath()
    {
    }

    private void OnCrushDeath()
    {
        
    }

    private void OnSlicedDeath()
    {
        
    }

    private void OnCrushedDeath()
    {
        
    }
}