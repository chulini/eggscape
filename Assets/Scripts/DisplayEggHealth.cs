using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using TMPro;

public class DisplayEggHealth : MonoBehaviour
{
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable()
    {
        _eggHealth.AddListener(UpdateHealth);
    }

    private void OnDisable() {
        _eggHealth.RemoveListener(UpdateHealth);
    }

    private void Update()
    {
        // For testing
        // _eggHealth.Value -= Time.deltaTime;
    }

    private void UpdateHealth() {
        healthText.text = Mathf.RoundToInt(_eggHealth.Value).ToString();
    }
}
