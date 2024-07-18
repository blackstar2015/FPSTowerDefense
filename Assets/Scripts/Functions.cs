using System.Collections;
using System.Collections.Generic;
using TDTK;
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

    public static List<IUnit> GetUnitsWithinRange(Vector3 pos, float range, List<IUnit> unitList)
    {
        List<IUnit> tgtList = new List<IUnit>();

        for (int i = 0; i < unitList.Count; i++)
        {
            if (Vector3.Distance(pos, unitList[i].GetPos()) < range + unitList[i].GetRadius())
                tgtList.Add(unitList[i]);
        }

        return tgtList;
    }
}
