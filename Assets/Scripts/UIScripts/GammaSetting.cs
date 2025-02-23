﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
#if USING_URP
using UnityEngine.Rendering.Universal;
#endif
#if USING_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif


[CreateAssetMenu(menuName = "Settings/Gamma Setting")]
public class GammaSetting : FloatSetting
{
    [Header("Gamma")]
    [SerializeField] private VolumeProfile _volumeProfile;

    public override void SetValue(float newValue)
    {
        base.SetValue(newValue);

        if (_volumeProfile.TryGet(out UnityEngine.Rendering.Universal.LiftGammaGain urpLiftGammaGain))
        {
            urpLiftGammaGain.gamma.value = Vector4.one * newValue;
        }
        else
        {
            Debug.LogWarning($"Volume Profile: {_volumeProfile.name} is missing Lift Gamma Gain component.", _volumeProfile);
        }
    }

}
