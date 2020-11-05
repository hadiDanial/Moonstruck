﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityObject : MonoBehaviour
{
    private static float normalGravity = -9.8f;
    private static float moonGravity = -1.62f;

    internal Rigidbody2D rb;
    public GravityState currentState = GravityState.NormalGravity;
    public GravityState initialState;
    internal virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialState = currentState;
        SetGravityStateInternal();
    }

    private void SetGravityStateInternal()
    {
        switch (currentState)
        {
            case GravityState.NormalGravity:
                rb.gravityScale = normalGravity;
                break;
            case GravityState.MoonGravity:
                rb.gravityScale = moonGravity;
                break;
            case GravityState.Weightless:
                rb.gravityScale = 0;
                break;
            default:
                break;
        }
    }

    public void SetGravityState(GravityState state, float delay = 0f)
    {
        currentState = state;
        Invoke("SetGravityStateInternal", delay);
    }
}

public enum GravityState
{
    NormalGravity, MoonGravity, Weightless
}