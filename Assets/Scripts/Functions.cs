using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions
{
    public static void SetMouse(bool toLocked)
    {
        if (!toLocked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
