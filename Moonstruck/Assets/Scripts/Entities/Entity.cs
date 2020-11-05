using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityObject))]
public class Entity : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] internal int maxHealth;
    [SerializeField] internal int currentHealth { get; set; }
    [SerializeField] internal EntityState currentEntityState = EntityState.Active;

    [Header("Ground Check")]
    [SerializeField] internal GameObject groundCheckLeft, groundCheckRight;
    [SerializeField] internal LayerMask groundMask, enemyMask;


    internal Rigidbody2D rb;
    internal GravityObject gravityObj;
    internal Transform initialTransform;
    internal EntityState initialState;
    internal bool canMove, canTakeDamage;
    internal bool isGrounded => CheckIsGrounded();
    internal virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityObj = GetComponent<GravityObject>();
        SetupEntity();
    }

    internal virtual void SetupEntity()
    {
        currentHealth = maxHealth;
        initialTransform = transform;
        initialState = currentEntityState;
        SetEntityState();
    }

    internal virtual void ResetEntity()
    {
        currentHealth = maxHealth;
        transform.position = initialTransform.position;
        transform.rotation = initialTransform.rotation;
        currentEntityState = initialState;
        SetEntityState();
    }

    internal virtual void SetEntityState()
    {
        switch (currentEntityState)
        {
            case EntityState.Active:
                {
                    canMove = true;
                    canTakeDamage = false;
                }
                break;
            case EntityState.Inactive:
                {
                    canMove = false;
                    canTakeDamage = false;
                }
                break;
            case EntityState.Dead:
                {
                    canMove = false;
                    canTakeDamage = false;
                }
                break;
            default:
                {
                    canMove = true;
                }
                break;
        }
    }

    internal virtual void Damage(int dmg)
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
        }
    }
    internal virtual void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        // TODO - Sound effect here
    }
    internal virtual void Kill()
    {
        // TODO - Kill the Entity
        // TODO - Sound effect here
        throw new NotImplementedException();
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

    public bool CheckIsGrounded()
    {
        if (groundCheckLeft == null || groundCheckRight == null)
        {
            Debug.LogError(transform.name + ": No Ground Check Object!");
            return false;
        }
        RaycastHit2D hitR = Physics2D.Raycast(groundCheckRight.transform.position, Vector2.down, 0.1f, groundMask);
        RaycastHit2D hitL = Physics2D.Raycast(groundCheckLeft.transform.position, Vector2.down, 0.1f, groundMask);
        if (hitR || hitL) return true;
        return false;
    }
}

public enum EntityState
{ 
    Active, Inactive, Dead
}