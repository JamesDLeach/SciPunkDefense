using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static InputManager _inputManager;
    private static GridManager _gridManager;

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

    public delegate void GameEvent();
    public static event GameEvent GameStart, GameOver;
    public static void TriggerGameStart()
    {
        if (GameStart != null)
        {
            GameStart();
        }
    }
    public static void TriggerGameOver()
    {
        if (GameOver != null)
        {
            GameOver();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
        _inputManager = GetComponent<InputManager>();
        _gridManager = GetComponent<GridManager>();
    }
}