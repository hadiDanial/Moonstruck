using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Moonstruck/General/New Game Settings")]
public class GameSettings : ScriptableObject
{
    // Aiming
    public float mouseSensitivity = 1;
    public float gamepadAimSensitivity = 1;
    public bool invertY = false;
}
