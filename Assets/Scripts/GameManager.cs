using System.Collections;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private GameObjectReference _playerReference;
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private CheckpointComponent _levelStartPoint;
    [SerializeField] private GameEvent _playerDiedEvent;
    [SerializeField] private GameEvent _onRespawnPlayer;
    [SerializeField] private BoolGameEvent _onPausedEvent;
    [SerializeField] private FloatGameEvent _onScreenFade;
    [SerializeField] private Canvas _screenFade;
    [SerializeField] private float _fadeChangeStep = 0.05f;
    [SerializeField] private float _fadeChangeInterval = 0.05f;

    private Coroutine _fadeCoroutine;
    private Image _fadeImage;
    private float _currentTimeScale;

    private void Awake()
    {
        SetSpawnPoint();
        _playerReference.Value = Instantiate(_playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _fadeImage = _screenFade.GetComponentInChildren<Image>();
    }

    private void Start()
    {
        _onRespawnPlayer.Raise();
        _screenFade.gameObject.SetActive(true);
        _onScreenFade.Raise(0f);
    }

    private void OnDestroy()
    {
        _activeCheckpoint.Value = _levelStartPoint;
    }
    
    private void OnEnable()
    {
        _eggHealth.AddListener(OnHealthChange);
        _onScreenFade.AddListener(OnFade);
        _onPausedEvent.AddListener(OnPause);
    }

    private void OnDisable() 
    {
        _eggHealth.RemoveListener(OnHealthChange);
        _onScreenFade.RemoveListener(OnFade);
        _onPausedEvent.RemoveListener(OnPause);
    }

    private void SetSpawnPoint()
    {
        _activeCheckpoint.Value = _levelStartPoint;
    }

    private void OnHealthChange()
    {
        if (_eggHealth.Value > float.Epsilon)
        {
            return;
        }
        
        _playerDiedEvent.Raise();
    }

    private void OnFade(float targetOpacity)
    {
        if (null != _fadeCoroutine)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(FadeCoroutine(targetOpacity));
    }

    private IEnumerator FadeCoroutine(float targetOpacity)
    {
        var fadeColor = _fadeImage.color; 
        var currentOpacity = fadeColor.a;

        while (NotAtOpacity(currentOpacity, targetOpacity))
        {
            if (currentOpacity < targetOpacity)
            {
                SetFadeAlpha(_fadeImage.color.a + _fadeChangeStep);

                if (_fadeImage.color.a > targetOpacity)
                {
                    SetFadeAlpha(targetOpacity);
                }
                
                yield return new WaitForSecondsRealtime(_fadeChangeInterval);
            }
            else if (currentOpacity > targetOpacity)
            {
                SetFadeAlpha(_fadeImage.color.a - _fadeChangeStep);

                if (_fadeImage.color.a < targetOpacity)
                {
                    SetFadeAlpha(targetOpacity);
                }

                yield return new WaitForSecondsRealtime(_fadeChangeInterval);
            }
        }
    }
    
    private void SetFadeAlpha(float alpha)
    {
        var fadeColor = _fadeImage.color; 
        
        _fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
    }

    private static bool NotAtOpacity(float opacity, float targetOpacity)
    {
        return opacity < targetOpacity || opacity > targetOpacity;
    }

    private void OnPause(bool isPaused)
    {
        OnPauseFadeHandler(isPaused);
        OnPauseTimeScaleHandler(isPaused);
    }

    private void OnPauseFadeHandler(bool isPaused)
    {
        if (isPaused)
        {
            _onScreenFade.Raise(0.75f);

            return;
        }
        
        _onScreenFade.Raise(0f);
    }

    private void OnPauseTimeScaleHandler(bool isPaused)
    {
        if (isPaused)
        {
            _currentTimeScale = Time.timeScale;
            Time.timeScale = 0f;

            return;
        }

        Time.timeScale = _currentTimeScale;
    }
}
