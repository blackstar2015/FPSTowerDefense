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
        }
        else
        {
            SetMouse(false); // mouse is always on in Overview mode.
            CameraControl.mainCam = overheadViewObjects[0].GetComponentInChildren<Camera>();
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
