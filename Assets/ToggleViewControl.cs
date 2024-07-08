using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleViewControl : MonoBehaviour
{
    [Tooltip("Specify objects to be enabled if and only if in overhead view.")]
    [SerializeField] List<GameObject> overheadViewObjects;
    [Tooltip("Specify objects to be enabled if and only if in first person view.")]
    [SerializeField] List<GameObject> firstPersonViewObjects;


    [Header("Monitoring")]
    [SerializeField] bool inFirstPerson = false; //ViewEnum currentView = ViewEnum.Overhead;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            ToggleView();
    }

    void ToggleView()
    {
        inFirstPerson = !inFirstPerson;

        ToggleGameObjects(overheadViewObjects, !inFirstPerson);
        ToggleGameObjects(firstPersonViewObjects, inFirstPerson);


        if (!inFirstPerson)
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

    void ToggleGameObjects(List<GameObject> list, bool activateThem)
    {
        foreach (GameObject gameObject in list)
            gameObject.SetActive(activateThem);
    }


}
