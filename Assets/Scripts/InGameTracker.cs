using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum GameState { Playing, Stop, Dialogue}

public class InGameTracker : MonoBehaviour
{
    public static InGameTracker instance;

    public UnityAction<GameState> onStateChange;
    [SerializeField] private GameState _state;
    public GameState state
    {
        set
        {
            if (value == _state)
                return;

            _state = value;
            onStateChange?.Invoke(value);
            Debug.Log("Set State: " + _state);
        }

        get
        {
            return _state;
        }
    
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
