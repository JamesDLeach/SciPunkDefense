using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static InputManager _inputManager;
    private static GridManager _gridManager;

    public int currency;
    public int startCurrency;
    public int round;
    public int coreHealth;
    public bool coreSpawned;

    public delegate void GameEvent();
    public static event GameEvent GameStart, GameOver, GameStateChange;

    public enum GameState
    {
        Menu,
        Building,
        Wave
    }
    private GameState _state;
    public GameState State { 
        get
        {
            return _state;
        }
        set
        {
            this._state = value;
            GameStateChange?.Invoke();
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("GameManager is NULL");
            }
            return _instance;
        }
    }

    public static InputManager InputManager
    {
        get
        {
            if (_inputManager is null)
            {
                Debug.LogError("InputManager is NULL");
            }
            return _inputManager;
        }
    }
    public static GridManager GridManager
    {
        get
        {
            if(_gridManager is null)
            {
                Debug.LogError("GridManager is NULL");
            }
            return _gridManager;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
        _inputManager = GetComponent<InputManager>();
        _gridManager = GetComponent<GridManager>();
        State = GameState.Menu;
    }

    private void Start()
    {
        GameStart?.Invoke();
    }
}