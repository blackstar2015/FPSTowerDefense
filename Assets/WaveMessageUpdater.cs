using System.Collections;
using System.Collections.Generic;
using TDTK;
using TMPro;
using UnityEngine;

public class WaveMessageUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textObject;

    // Start is called before the first frame update
    void Start()
    {
        //TDTK.TDTK.onSpawnCountDownE += OnSpawnCountDown;
        TDTK.TDTK.onEnableSpawnE += OnSpawnCountDown;
        textObject.text = SpawnManager.GetNextWaveMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSpawnCountDown()
    {        
        var message = SpawnManager.GetNextWaveMessage();
        textObject.text = message;
    }
}
