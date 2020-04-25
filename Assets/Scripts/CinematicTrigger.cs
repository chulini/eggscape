using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField] private GameStateVariable currentGameState;
    [SerializeField] private GameObjectReference cinematicGameObject;
    [SerializeField] private GameEvent _onSkipCinematic;

    private void OnEnable()
    {
        cinematicGameObject.Value = gameObject;
        currentGameState.AddListener(CurrentGameStateChanged);
        _onSkipCinematic.AddListener(CinematicFinished);
        
    }

    private void OnDisable()
    {
        currentGameState.RemoveListener(CurrentGameStateChanged);
        cinematicGameObject.Value = null;
        _onSkipCinematic.RemoveListener(CinematicFinished);
    }

    private void CurrentGameStateChanged()
    {
        if (currentGameState.Value != GameState.cinematic)
        {
            return;
        }
        
        GetComponent<PlayableDirector>().Play();
        Invoke(nameof(CinematicFinished), (float) GetComponent<PlayableDirector>().duration);
    }

    private void CinematicFinished()
    {
        currentGameState.Value = GameState.playing;
        DestroyImmediate(gameObject);
    }
}
