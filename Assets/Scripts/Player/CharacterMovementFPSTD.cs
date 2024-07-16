using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMovement;

public class CharacterMovementFPSTD : CharacterMovement3D
{
    protected override void Awake()
    {
        base.Awake();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
