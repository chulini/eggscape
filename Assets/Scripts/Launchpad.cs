using System.Collections;
using System.Collections.Generic;
using Rewired.Demos;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Launchpad : MonoBehaviour
{
    [SerializeField] private float invulnerabilityTime;
    [SerializeField] Rigidbody _rb;
    [SerializeField] IntReference eggInvulnerability;
    [SerializeField] private float impulseUp;
    [SerializeField] private float impulseForward;
    [SerializeField] private float bulletTimeDuration;
    [SerializeField] private float bulletTimeTimeScale;
    [SerializeField] private float bulletTimeTransitionDuration;
    private Transform GetParentMostTransform(Transform t) {
        while(t.parent != null) {
            t = t.parent;
        }
        return t;
    }

    private void Start() {
        eggInvulnerability.Value = 0;
    }

    private void OnTriggerEnter(Collider other) {
        // Debug.Log("something entered");
        Transform collTransform = GetParentMostTransform(other.transform);
        if (collTransform.gameObject.tag == "Player") {
            Rigidbody collRb = collTransform.GetComponent<Rigidbody>();
            Vector3 totalForce = Vector3.up * impulseUp + transform.forward * impulseForward;
            SetPlayerInvulnerable(invulnerabilityTime);
            collRb.AddForce(totalForce, ForceMode.Impulse);
            StartCoroutine(BulletTime(bulletTimeDuration));
        }
    }

    IEnumerator BulletTime(float duration)
    {
        yield return new WaitForSecondsRealtime(.1f);
        float t0 = Time.realtimeSinceStartup;
        float r = 0;
        do
        {
            r = (Time.realtimeSinceStartup - t0) / bulletTimeTransitionDuration;
            Time.timeScale = Mathf.Lerp(Time.timeScale, bulletTimeTimeScale, 9f/60f);
            yield return new WaitForSecondsRealtime(1/60f);
        } while (r < 1);
        yield return new WaitForSecondsRealtime(duration);
        
        t0 = Time.realtimeSinceStartup;
        r = 0;
        do
        {
            r = (Time.realtimeSinceStartup - t0) / bulletTimeTransitionDuration;
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 9f/60f);
            yield return new WaitForSecondsRealtime(1/60f);
        } while (r < 1);
        Time.timeScale = 1;
    }

    void SetPlayerInvulnerable(float invulnerableTime) {
        eggInvulnerability.Value += 1;
        Invoke("DeactivateInvulnerability", invulnerableTime);
    }

    void DeactivateInvulnerability() {
        eggInvulnerability.Value -= 1;
    }
}
