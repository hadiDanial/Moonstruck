using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
    public GameSettings settings;
    private CursorController cursor;

    internal override void Awake()
    {
        base.Awake();
        cursor = GetComponentInChildren<CursorController>();
        cursor.Setup(settings);
    }

    public void OnMovement(InputValue value)
    {
        if (!IsActive())
            return;
        input = value.Get<Vector2>();
        movementVector = useGravity ? new Vector2(input.x, 0).normalized : input;
    }
    public void OnJump()
    {
        if (!IsActive())
            return;
        Jump();
    }
    public void OnShoot()
    {

    }
    public void OnMoonSelf()
    {

    }
    public void OnMoonOther()
    {

    }
    public void OnMouseAim(InputValue val)
    {
        cursor.SetMouse(val.Get<Vector2>());
    }
    public void OnGamepadAim(InputValue val)
    {
        cursor.SetGamepad(val.Get<Vector2>());
    }

}
