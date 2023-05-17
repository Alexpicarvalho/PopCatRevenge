using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [Header("Input Buffering")]
    [SerializeField] int _inputQueueSize;
    [SerializeField] float _inputLifeTime;
    [SerializeField] InputData[] _inputQueue;
    // Start is called before the first frame update
    private void Awake()
    {
        _inputQueue = new InputData[_inputQueueSize];
    }

    void AddInputToQueue(InputData input)
    {

    }

    void RemoveInputFromQueue(InputData input)
    {

    }
}

public struct InputData
{
    public KeyCode? KeyCode;
    public int? MouseButton;
    public float LifeTime;

    public InputData(KeyCode key)
    {
        KeyCode = key;
        MouseButton = null;
        LifeTime = 0;
    }
    public InputData(int mouseButton)
    {
        KeyCode = null;
        MouseButton = mouseButton;
        LifeTime = 0;
    }
}
