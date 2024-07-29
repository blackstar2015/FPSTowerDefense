using Cinemachine;
using FMODUnity;
using GameEvents;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TDTK;
using UnityEngine;

public class ToggleViewControl : MonoBehaviour
{
    [Tooltip("Specify objects to be enabled if and only if in overhead view. First object in list must contain the camera in its hierarchy.")]
    [SerializeField] List<GameObject> overheadViewObjects;
    [Tooltip("Specify objects to be enabled if and only if in first person view. First object in list must contain the camera in its hierarchy.")]
    [SerializeField] List<GameObject> firstPersonViewObjects;
    [SerializeField] private UIControl UIPrefab;
    [SerializeField] bool inFirstPerson = false; //ViewEnum currentView = ViewEnum.Overhead;
    [SerializeField] BoolEventAsset InFirstPersonEvent;
    [SerializeField] bool mouseLocked;
    [field: SerializeField, BoxGroup("SFX")] public EventReference ToggleViewSFX { get; protected set; }

    [SerializeField]
    private GameObject crosshair;
    [SerializeField]
    private GameObject crosshairCharge;
    //[SerializeField] bool disableMouseLookInMouseCursorMode;



    // Start is called before the first frame update
    void Start()
    {
        if(UIPrefab != null)
        {
            crosshair = GameObject.Find("UI Variant/UICamera_Screen/Canvas_HUD/Crosshair");
            crosshairCharge = GameObject.Find("UI Variant/UICamera_Screen/Canvas_HUD/CrosshairCharging");
        }

        if(crosshair == null)
        {
            Debug.Log("Crosshair not found.");
        }

        if (crosshairCharge == null)
        {
            Debug.Log("CrosshairCharge not found.");
        }
        SetViewValues();
        SetMouseLookEvents();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Tab))
        //    ToggleView();

        if (inFirstPerson && Input.GetKeyDown(KeyCode.X))
            ToggleMouse();

        if (Input.GetKeyDown(KeyCode.G))
            SpawnManager.Spawn();

        if (Input.GetKeyDown(KeyCode.H))
        {
            crosshair.SetActive(!crosshair.activeSelf);
            crosshairCharge.SetActive(!crosshairCharge.activeSelf);
        }
    }

    void ToggleView()
    {
        inFirstPerson = !inFirstPerson;
        SetViewValues();
        if (!ToggleViewSFX.IsNull) RuntimeManager.PlayOneShot(ToggleViewSFX, transform.position);

    }

    void SetViewValues()
    {
        ToggleGameObjects(overheadViewObjects, !inFirstPerson);
        ToggleGameObjects(firstPersonViewObjects, inFirstPerson);

        Functions.mouseOverride = !inFirstPerson;

        if (inFirstPerson)
        {
            SetMouse(mouseLocked);
            CameraControl.mainCam = firstPersonViewObjects[0].GetComponentInChildren<Camera>();
            InFirstPersonEvent.Invoke(true);
            //UIPrefab.usePieMenuForBuild = true;
            //UIPrefab.buildMode = UIControl._BuildMode.PointNBuild;
        }
        else
        {
            SetMouse(false); // mouse is always on in Overview mode.
            CameraControl.mainCam = overheadViewObjects[0].GetComponentInChildren<Camera>();
            InFirstPersonEvent.Invoke(false);
            //UIPrefab.usePieMenuForBuild = false;
            //UIPrefab.buildMode = UIControl._BuildMode.DragNDrop;
        }
    }

    void SetMouseLookEvents()
    {

        Functions.OnMouseLocked.AddListener(SetMouseLookOn);
        Functions.OnMouseReleased.AddListener(SetMouseLookOff);
    }

    void SetMouseLookOn()
    {
        var cinemachineInputProvider = firstPersonViewObjects[0].GetComponentInChildren<CinemachineInputProvider>();
        cinemachineInputProvider.enabled = true;
    }

    void SetMouseLookOff()
    {
        var cinemachineInputProvider = firstPersonViewObjects[0].GetComponentInChildren<CinemachineInputProvider>();
        cinemachineInputProvider.enabled = false;
    }

    void ToggleGameObjects(List<GameObject> list, bool activateThem)
    {
        foreach (GameObject gameObject in list)
            gameObject.SetActive(activateThem);
    }

    void ToggleMouse()
    {
        mouseLocked = !mouseLocked;
        SetMouse(mouseLocked);
    }

    void SetMouse(bool toLocked)
    {        
        Functions.SetMouse(toLocked);
        //var cinemachineInputProvider = firstPersonViewObjects[0].GetComponentInChildren<CinemachineInputProvider>();

        //if (!disableMouseLookInMouseCursorMode)
        //    return;

        //if (!toLocked)
        //    cinemachineInputProvider.enabled = false;
        //else
        //    cinemachineInputProvider.enabled = true;
    }
}
