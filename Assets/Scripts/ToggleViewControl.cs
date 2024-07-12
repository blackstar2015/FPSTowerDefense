using Cinemachine;
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
    [SerializeField] bool mouseLocked;



    // Start is called before the first frame update
    void Start()
    {
        SetViewValues();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleView();

        if (inFirstPerson && Input.GetKeyDown(KeyCode.X))
            ToggleMouse();
    }

    void ToggleView()
    {
        inFirstPerson = !inFirstPerson;
        SetViewValues();
    }

    void SetViewValues()
    {
        ToggleGameObjects(overheadViewObjects, !inFirstPerson);
        ToggleGameObjects(firstPersonViewObjects, inFirstPerson);

        if (inFirstPerson)
        {
            SetMouse(mouseLocked);
            CameraControl.mainCam = firstPersonViewObjects[0].GetComponentInChildren<Camera>();
            //UIPrefab.usePieMenuForBuild = true;
            //UIPrefab.buildMode = UIControl._BuildMode.PointNBuild;
        }
        else
        {
            SetMouse(false); // mouse is always on in Overview mode.
            CameraControl.mainCam = overheadViewObjects[0].GetComponentInChildren<Camera>();
            //UIPrefab.usePieMenuForBuild = false;
            //UIPrefab.buildMode = UIControl._BuildMode.DragNDrop;
        }
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
        if (!toLocked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            firstPersonViewObjects[0].GetComponentInChildren<CinemachineInputProvider>().enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            firstPersonViewObjects[0].GetComponentInChildren<CinemachineInputProvider>().enabled = true;
        }
    }
}
