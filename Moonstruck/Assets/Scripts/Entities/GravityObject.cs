using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityObject : MonoBehaviour
{
    private static float normalGravity = -50f;
    private static float moonGravity = -10f;
    private static float groundedGravity = -2f;
    private static float zeroGravity = 0;

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
            case GravityState.GroundedGravity:
                rb.gravityScale = groundedGravity;
                break;
            case GravityState.Weightless:
                rb.gravityScale = zeroGravity;
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


    public GravityState GetGravityState()
    {
        return currentState;
    }

    public float GetCurrentGravity()
    {
        switch (currentState)
        {
            case GravityState.NormalGravity:
                return normalGravity;
            case GravityState.MoonGravity:
                return moonGravity;
            case GravityState.GroundedGravity:
                return groundedGravity;
            case GravityState.Weightless:
                return zeroGravity;
            default:
                return normalGravity;
        }
    }
    public float GetStateGravity(GravityState state)
    {
        switch (state)
        {
            case GravityState.NormalGravity:
                return normalGravity;
            case GravityState.MoonGravity:
                return moonGravity;
            case GravityState.GroundedGravity:
                return groundedGravity;
            case GravityState.Weightless:
                return zeroGravity;
            default:
                return normalGravity;
        }
    }

    public float GetNormalGravity()
    {
        return normalGravity;
    }

    internal void SetGravity(GravityState gravityState)
    {
        rb.gravityScale = GetStateGravity(gravityState);
    }
}

public enum GravityState
{
    NormalGravity, MoonGravity, GroundedGravity, Weightless
}
