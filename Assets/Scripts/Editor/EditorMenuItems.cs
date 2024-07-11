using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorMenuItems : EditorWindow
{
    [MenuItem("Tools/TDTK/New Scene (FPS)", false, -90)]
    private static void NewFpsSceneFromTemplate()
    {
        OpenTemplateScene();
    }

    private const string templateScenePath = "Assets/Scenes/Scene Templates/newFpsScene.unity"; // Hardcoded path to your template scene


    private static void OpenTemplateScene()
    {
        if (!File.Exists(templateScenePath))
        {
            Debug.LogError("Template scene not found at path: " + templateScenePath);
            return;
        }

        // Open the template scene in single mode, replacing the current scene
        EditorSceneManager.OpenScene(templateScenePath, OpenSceneMode.Single);

        // Create a new, empty scene with the "Untitled" name and replace the current scene
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Copy the contents of the template scene to the new scene
        EditorSceneManager.OpenScene(templateScenePath, OpenSceneMode.Additive);
        Scene templateScene = SceneManager.GetSceneByPath(templateScenePath);
        if (templateScene.IsValid())
        {
            foreach (GameObject go in templateScene.GetRootGameObjects())
            {
                SceneManager.MoveGameObjectToScene(go, newScene);
            }

            // Unload the template scene
            EditorSceneManager.CloseScene(templateScene, true);

            Debug.Log("New scene created from template with the name 'Untitled'");
            EditorSceneManager.MarkSceneDirty(newScene);
        }
        else
        {
            Debug.LogError("Failed to open the template scene.");
        }
    }

    //private static void OpenTemplateScene()
    //{
    //    if (!File.Exists(templateScenePath))
    //    {
    //        Debug.LogError("Template scene not found at path: " + templateScenePath);
    //        return;
    //    }

    //    Scene templateScene = EditorSceneManager.OpenScene(templateScenePath, OpenSceneMode.Single);
    //    if (templateScene.IsValid())
    //    {
    //        Debug.Log($"Template scene opened: {templateScenePath}");
    //        // Optionally, you can unsave the scene so it doesn't prompt to save the template scene
    //        EditorSceneManager.MarkSceneDirty(templateScene);
    //        templateScene.name = "Untitled";
    //    }
    //    else
    //    {
    //        Debug.LogError("Failed to open the template scene.");
    //    }
    //}

}

