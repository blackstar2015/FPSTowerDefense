using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions
{
    // if set to true, SetMouse calls won't hide the mouse. Use for overhead view when you always want the mouse cursor on.
    public static bool mouseOverride = false;

    public static void SetMouse(bool toLocked)
    {
        if (!toLocked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else if (!mouseOverride)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
