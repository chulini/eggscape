using ScriptableObjectArchitecture;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private Vector2 _damageRange = new Vector2(1, 3);
    
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private GameObjectReference _player;
    [SerializeField] private Rigidbody _rb;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.Value || other.gameObject.tag == "Player")
        {
            _eggHealth.Value -= CalculateDamage();
        }
    }

    private float CalculateDamage()
    {
        return Random.Range(_damageRange.x, _damageRange.y);
    }
}
