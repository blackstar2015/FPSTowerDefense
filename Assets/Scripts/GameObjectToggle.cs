using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectToggle : MonoBehaviour
{
    [SerializeField] List<GameObject> gameObjectsToToggle;
    [SerializeField] bool activated;

    // Start is called before the first frame update
    void Start()
    {
        SetActive();
    }

    void OnValidate()
    {
        SetActive();
    }

    void SetActive()
    {
        foreach (GameObject gameObject in gameObjectsToToggle)
            if (gameObject != null) // note for whatever reason, coalescing null check doesn't work here.
                gameObject.SetActive(activated);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
