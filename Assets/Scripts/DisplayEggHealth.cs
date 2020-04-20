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
    [SerializeField] private IntGameEvent _onDiedFromDamageType;
    [SerializeField] private GameEvent _onRespawnPlayer;

    private void OnEnable()
    {
        _eggHealth.AddListener(UpdateHealth);
        _onDiedFromDamageType.AddListener(OnDiedFromDamageType);
        _onRespawnPlayer.AddListener(OnPlayerSpawned);
        UpdateHealth();
    }

    private void OnDisable()
    {
        _eggHealth.RemoveListener(UpdateHealth);
        _onRespawnPlayer.RemoveListener(OnPlayerSpawned);
        _onDiedFromDamageType.RemoveListener(OnDiedFromDamageType);
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

    private void OnDiedFromDamageType(int damageType)
    {
        if (damageType == -1)
        {
            _deathImage.gameObject.SetActive(false);
            
            return;
        }
        
        _deathImage.gameObject.SetActive(true);
        _deathImage.sprite = _deathImages[damageType];
    }

    private void OnPlayerSpawned()
    {
        _deathImage.gameObject.SetActive(false);
    }
}