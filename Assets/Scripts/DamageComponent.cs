using System;
using System.Collections;
using ScriptableObjectArchitecture;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private Vector2 _damageRange = new Vector2(1, 10);
    [SerializeField] private float _damageInterval = 1f;
    
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private GameObjectReference _player;

    private Coroutine _damageCoroutine;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.Value || other.gameObject.tag == "Player")
        {
            _damageCoroutine = StartCoroutine(DamagePlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player.Value)
        {
            StopCoroutine(_damageCoroutine);
        }
    }

    private float CalculateDamage()
    {
        return Random.Range(_damageRange.x, _damageRange.y);
    }

    private IEnumerator DamagePlayer()
    {
        while (true)
        {
            _eggHealth.Value -= CalculateDamage();
            
            yield return new WaitForSeconds(_damageInterval);
        }
    }
}
