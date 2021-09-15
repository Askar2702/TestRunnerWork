using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }
    public event Action<bool> Run;
    public event Action<bool> SetSide;
    private void Awake()
    {
        if (!instance) instance = this;
    }
    void Start()
    {
        UiManager.instance.StartPlay += StartGame;
        InputManager.instance.SetPoint += GetInput;
    }

    private void StartGame()
    {
        SetStatePlayer(true);
    }

    public void StopRun()
    {
        SetStatePlayer(false);
    }
    private void SetStatePlayer(bool isRun)
    {
        Run?.Invoke(isRun);
    }

    private void GetInput(float point)
    {
        if (point < 0) SetSide?.Invoke(true);
        else SetSide?.Invoke(false);
    }

    public Vector3 GetPosPlayer()
    {
        return transform.position;
    }
}
