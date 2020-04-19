using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class EggCracker : MonoBehaviour
{
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private MeshRenderer _meshRenderer;

    void Start()
    {
        // _material = _meshRenderer.sharedMaterial;
        _eggHealth.AddListener(UpdateCracks);
        UpdateCracks();
    }


    private void UpdateCracks() {
        float value = Mathf.Lerp(2, 0, _eggHealth.Value / 100f);
        print("Setting value: " + value);
        if(_meshRenderer != null)
            _meshRenderer.sharedMaterial.SetFloat("_cracks", value);
    }
}
