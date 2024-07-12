using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    [SerializeField] private GameObject QuitCanvas;

    private void Update()
    {
        bool isActive = QuitCanvas.activeSelf;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitCanvas.SetActive(!isActive);
        }
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
