using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : Entity
{
    internal override void Awake()
    {
        base.Awake();        
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


}
