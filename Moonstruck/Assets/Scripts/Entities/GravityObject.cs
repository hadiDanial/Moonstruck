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
    public Animator anim;

    private float timer;
    private bool returnToInitial = false;
    internal virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialState = currentState;
        SetGravityStateInternal();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(returnToInitial && timer<=0)
        {
            returnToInitial = false;
            SetGravityState(initialState);
        }
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

    /// <summary>
    /// Sets the gravity state.
    /// </summary>
    /// <param name="state">New state to set.</param>
    public void SetGravityState(GravityState state)
    {
        currentState = state;
        SetRigidbodyGravity();
        returnToInitial = false;
    }

    /// <summary>
    /// Sets the gravity state for a certain duration.
    /// </summary>
    /// <param name="state">New state to set.</param>
    /// <param name="duration">Optional delay until the new state is set.</param>
    public void SetGravityState(GravityState state, float duration = 1f)
    {
        currentState = state;
        SetRigidbodyGravity();
        // TODO - Glow animation?
        returnToInitial = true;
        timer = duration;
    }

    /// <summary>
    /// Set the rigidbody's gravity scale to the gravityState's gravity value.
    /// </summary>
    /// <param name="gravityState">Gravity value to set for the rigidbody.</param>
    internal void SetRigidbodyGravity()
    {
        rb.gravityScale = GetCurrentGravityValue();
    }
    /// <summary>
    /// Set the rigidbody's gravity scale to the gravityState's gravity value.
    /// </summary>
    /// <param name="gravityState">Gravity value to set for the rigidbody.</param>
    internal void SetRigidbodyGravity(GravityState gravityState)
    {
        rb.gravityScale = GetStateGravity(gravityState);
    }

    /// <summary>
    /// Returns the current GravityState.
    /// </summary>
    public GravityState GetGravityState()
    {
        return currentState;
    }

    /// <summary>
    /// Returns the gravity value for the current state.
    /// </summary>
    public float GetCurrentGravityValue()
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

    /// <summary>
    /// Returns the gravity value for a given state.
    /// </summary>
    /// <param name="state">State to get gravity value from.</param>
    /// <returns>Gravity value of the state.</returns>
    public static float GetStateGravity(GravityState state)
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

    /// <summary>
    /// Returns the value for normal gravity.
    /// </summary>
    public static float GetNormalGravity()
    {
        return normalGravity;
    }


}

public enum GravityState
{
    NormalGravity, MoonGravity, GroundedGravity, Weightless
}
