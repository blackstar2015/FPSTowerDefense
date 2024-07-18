using System.Collections;
using System.Collections.Generic;
using TDTK;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<Unit> playersList;
    static List<Unit> playersListStatic;

    // Start is called before the first frame update
    void Start()
    {
        playersListStatic = playersList;
    }

    public static List<Unit> GetUnitsWithinRange(Unit srcUnit, float range) { return GetUnitsWithinRange(srcUnit.GetPos(), range); }

    public static List<Unit> GetUnitsWithinRange(Vector3 pos, float range)
    {
        List<Unit> unitList = playersListStatic;
        return Functions.GetUnitsWithinRange(pos, range, unitList);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
