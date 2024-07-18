using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDTK;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //[SerializeField] List<Unit> playersList;
    static List<UnitPlayer> playersListStatic;

    // Start is called before the first frame update
    void Start()
    {
        playersListStatic = FindObjectsOfType<UnitPlayer>().ToList();
    }

    public static List<IUnit> GetUnitsWithinRange(Unit srcUnit, float range) { return GetUnitsWithinRange(srcUnit.GetPos(), range); }

    public static List<IUnit> GetUnitsWithinRange(Vector3 pos, float range)
    {
        List<UnitPlayer> unitList = playersListStatic;
        return Functions.GetUnitsWithinRange(pos, range, unitList.Cast<IUnit>().ToList());
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
