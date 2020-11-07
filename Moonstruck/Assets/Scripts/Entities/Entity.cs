﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityObject))]
public class Entity : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float jumpForce;
    [SerializeField, Range(0, 0.4f)] protected float jumpGracePeriod = 0.1f;
    [SerializeField, Range(0, 2)] protected float airControlPercent = 0.45f;
    [SerializeField] protected bool useGravity;

    [Header("Health")]
    [SerializeField] protected int maxHealth;
    [SerializeField] internal int currentHealth;
    [SerializeField] internal EntityState currentEntityState = EntityState.Active;

    [Header("Ground Check")]
    [SerializeField] protected GameObject groundCheckLeft;
    [SerializeField] protected GameObject groundCheckRight;
    [SerializeField] protected LayerMask groundMask, enemyMask;
    internal float groundCheckDistance = 0.1f;
    internal int internalSpeedMultiplier = 1000;


    internal Rigidbody2D rb;
    internal Collider2D col;
    internal GravityObject gravityObj;
    internal Transform initialTransform;
    internal EntityState initialState;
    internal Vector2 input, movementVector;
    internal bool canMove; // Might remove later, useless now
    internal bool canTakeDamage;

    internal float currentMovementMultiplier;
    internal float jumpTimeElapsed;
    internal bool isGrounded => CheckIsGrounded();
    internal bool canJump => isGrounded || jumpTimeElapsed >= 0;
    internal bool isCollided, hasJumped;
    internal float totalSpeedMultiplier => movementSpeed * currentMovementMultiplier * internalSpeedMultiplier;

    internal virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityObj = GetComponent<GravityObject>();
        SetupEntity();
    }

    internal virtual void Update()
    {
        if (IsActive())
        {
            jumpTimeElapsed = isGrounded ? jumpGracePeriod : jumpTimeElapsed - Time.deltaTime;
            rb.gravityScale = (isGrounded && !hasJumped) || (isGrounded && isCollided) ? 0 : gravityObj.GetCurrentGravity();
            if (input != Vector2.zero)
            {
                currentMovementMultiplier = isGrounded ? 1 : airControlPercent;
                rb.AddForce(movementVector * totalSpeedMultiplier * Time.deltaTime, ForceMode2D.Force);
            }
            else
            {
                if (rb.velocity != Vector2.zero)
                    rb.velocity = new Vector2(0.7f * rb.velocity.x, rb.velocity.y);
            }
        }


    }

    internal virtual void Jump()
    {
        if (IsActive() && canJump && !hasJumped ) 
        {
            hasJumped = true;
            rb.AddForce(Vector2.up * jumpForce * 10, ForceMode2D.Impulse);
        }
    }


    internal virtual void SetupEntity()
    {
        currentHealth = maxHealth;
        initialTransform = transform;
        initialState = currentEntityState;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        if (col != null) col.isTrigger = false;
        if (!useGravity) gravityObj.SetGravityState(GravityState.Weightless);
        rb.gravityScale = useGravity ? gravityObj.GetCurrentGravity() : 0;
        SetEntityState();
    }

    /// <summary>
    /// Resets the entity to its initial state: health, position, and EntityState
    /// </summary>
    internal virtual void ResetEntity()
    {
        currentHealth = maxHealth;
        transform.position = initialTransform.position;
        transform.rotation = initialTransform.rotation;
        currentEntityState = initialState;
        if (col != null) col.isTrigger = false;
        SetEntityState();
    }

    internal virtual void SetEntityState()
    {
        switch (currentEntityState)
        {
            case EntityState.Active:
                {
                    canMove = true;
                    SetVulnerable();
                }
                break;
            case EntityState.Inactive:
                {
                    canMove = false;
                    SetInvulnerable();
                }
                break;
            case EntityState.Dead:
                {
                    canMove = false;
                    SetInvulnerable();
                }
                break;
            default:
                {
                    canMove = true;
                }
                break;
        }
    }

    /// <summary>
    /// Returns true if the currentEntityState is Active
    /// </summary>
    internal bool IsActive()
    {
        return currentEntityState == EntityState.Active;
    }

    #region Health

    /// <summary>
    /// Damages the Entity and optionally sets it invulnerable for a time based on the hurt animation
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="setInvulnerable"></param>
    internal virtual void Damage(int dmg, bool setInvulnerable = true)
    {
        if (canTakeDamage)
        {
            currentHealth -= dmg;
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                Kill();
            }
            // TODO - Sound effect here
            // TODO - SetInvulnerable() and Disable it via animation event
        }
    }
    /// <summary>
    /// Heals the Entity
    /// </summary>
    /// <param name="heal"></param>
    internal virtual void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        // TODO - Sound effect here
    }
    /// <summary>
    /// Kills the Entity
    /// </summary>
    internal virtual void Kill()
    {
        // TODO - Kill the Entity
        currentEntityState = EntityState.Dead;
        rb.velocity = Vector2.zero;
        rb.gravityScale = gravityObj.GetNormalGravity();
        if (col != null) col.isTrigger = true;
        // TODO - Sound effect here
    }
    internal virtual void SetInvulnerable()
    {
        canTakeDamage = false;
        // TODO - Animation
    }
    internal virtual void SetVulnerable()
    {
        canTakeDamage = true;
    }

    #endregion


    /// <summary>
    /// Returns true if the Entity is grounded. 
    /// The entity is grounded if at least one of its ground checks returns true.
    /// </summary>
    internal bool CheckIsGrounded()
    {
        if (groundCheckLeft == null || groundCheckRight == null)
        {
            Debug.LogError(transform.name + ": No Ground Check Object!");
            return false;
        }
        RaycastHit2D hitR = Physics2D.Raycast(groundCheckRight.transform.position, Vector2.down, groundCheckDistance, groundMask);
        RaycastHit2D hitL = Physics2D.Raycast(groundCheckLeft.transform.position, Vector2.down, groundCheckDistance, groundMask);
        if (hitR || hitL)       
            return true;     
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCollided = true;
        if (isGrounded) hasJumped = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isCollided = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollided = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckRight.transform.position, groundCheckRight.transform.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(groundCheckLeft.transform.position, groundCheckLeft.transform.position + Vector3.down * groundCheckDistance);
    }
}

public enum EntityState
{ 
    Active, Inactive, Dead
}