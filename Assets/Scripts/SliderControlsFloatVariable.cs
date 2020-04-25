using System;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderControlsFloatVariable : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] FloatVariable _variableToControl;
#pragma warning restore 0649
    Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
        
    }

    void OnEnable()
    {
        _slider.onValueChanged.AddListener(SliderValueChanged);
        _slider.value = _variableToControl.Value;
    }

    void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SliderValueChanged);
    }
    void SliderValueChanged(float newValue)
    {
        _variableToControl.Value = newValue;
    }
}
