using UnityEngine;
using ScriptableObjectArchitecture;

public class EggCracker : MonoBehaviour
{
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private AudioClip[] cracks;
    private static readonly int _cracks = Shader.PropertyToID("_cracks");

    private void Start()
    {
        // _material = _meshRenderer.sharedMaterial;
        _eggHealth.AddListener(UpdateCracks);
        UpdateCracks();
    }


    private void UpdateCracks()
    {
        var value = Mathf.Lerp(0, 2, _eggHealth.Value / 100f);
        print("Setting value: " + value);

        if (_meshRenderer != null)
            _meshRenderer.sharedMaterial.SetFloat(_cracks, value);

        if (_eggHealth.Value < 100)
        {
            GetComponent<AudioSource>().PlayOneShot(cracks[Random.Range(0, cracks.Length)]);
        }
    }
}