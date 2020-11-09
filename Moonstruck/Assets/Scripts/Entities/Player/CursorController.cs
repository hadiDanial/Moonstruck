using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [Header("Cursor Setup")]
    [SerializeField] private GameObject cursor;
    [SerializeField] private SpriteRenderer cursorSprite;
    [SerializeField] private Sprite gameCursor;
    [SerializeField] private Sprite activeCursor;
    [SerializeField] private Sprite menuCursor;
    [SerializeField] private CursorType cursorType = CursorType.Game;

    [Header("Cursor Settings")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private bool rotateCursor = false;
    [SerializeField] private float rotateSpeed = 30f;
    [SerializeField] private bool useGamepad = false;

    private float zPos = -3;
    private Vector2 screenDimensions;
    private Vector3 currentPosition, cursorPosition;
    private Vector2 gamepadInput, mousePos;
    private Camera cam;
    private GameSettings settings;

    private void Awake()
    {
        if (!Application.isEditor)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        screenDimensions = new Vector2(Screen.width, Screen.height);
        currentPosition = new Vector2(screenDimensions.x / 2, screenDimensions.y / 2);
        cam = Camera.main;
        SetCursor();
    }
    
    private void Update()
    {
        if(useGamepad)
        {
            // TODO - Need some kind of smoothing here, and maybe a lock-on mode or aim assist
            currentPosition.x += gamepadInput.x * settings.gamepadAimSensitivity * movementSpeed;
            currentPosition.y += (settings.invertY ? -gamepadInput.y : gamepadInput.y) * settings.gamepadAimSensitivity * movementSpeed;
        }
        else
        {
            currentPosition = mousePos;
        }
        currentPosition.x = Mathf.Clamp(currentPosition.x, 0, screenDimensions.x - 1);
        currentPosition.y = Mathf.Clamp(currentPosition.y, 0, screenDimensions.y - 1);
        cursorPosition = cam.ScreenToWorldPoint(currentPosition);
        cursorPosition.z = zPos;
        cursor.transform.position = cursorPosition;

        if (rotateCursor)
            cursor.transform.Rotate(-Vector3.forward * rotateSpeed * Time.deltaTime);
    }
    internal void Setup(GameSettings settings)
    {
        this.settings = settings;
    }

    internal void SetGamepad(Vector2 input)
    {
        useGamepad = true;
        gamepadInput = input;
    }

    internal void SetMouse(Vector2 input)
    {
        useGamepad = false;
        mousePos = input;
    }

    private void SetCursor()
    {
        if(gameCursor == null || activeCursor == null || menuCursor == null)
        {
            Debug.LogWarning("Cursor texture not set!");
            return;
        }
        switch (cursorType)
        {
            case CursorType.Game:
                cursorSprite.sprite = gameCursor;
                break;
            case CursorType.Active:
                cursorSprite.sprite = activeCursor;
                break;
            case CursorType.Menu:
                cursorSprite.sprite = menuCursor;
                break;
            default:
                cursorSprite.sprite = gameCursor;
                break;
        }
    }

    public void SetCursorType(CursorType type)
    {
        cursorType = type;
        SetCursor();
    }
}

public enum CursorType
{
    Game, Active, Menu
}
