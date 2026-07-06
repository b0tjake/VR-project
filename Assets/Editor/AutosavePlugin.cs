#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class AutosavePlugin
{
    // Configure how often you want to save (in minutes)
    private static readonly double SaveIntervalMinutes = 2.0; 
    private static double nextSaveTime;

    static AutosavePlugin()
    {
        // 1. Hook into Unity's update loop to track time
        EditorApplication.update += OnEditorUpdate;

        // 2. Hook into Play Mode changes (Saves your work right before hitting Play!)
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

        // Set the timer for the first interval
        ResetTimer();
    }

    private static void OnEditorUpdate()
    {
        // Don't autosave while the game is actually playing
        if (EditorApplication.isPlaying || EditorApplication.isPaused) return;

        if (EditorApplication.timeSinceStartup >= nextSaveTime)
        {
            ExecuteAutosave("Timer Interval Reached");
        }
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // Save the moment you click the Play button, right before it switches
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            ExecuteAutosave("Entering Play Mode");
        }
    }

    private static void ExecuteAutosave(string reason)
    {
        // Only save if scenes are actually changed/dirty to save performance
        bool anySceneDirty = false;
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).isDirty)
            {
                anySceneDirty = true;
                break;
            }
        }

        if (anySceneDirty)
        {
            Debug.Log($"<color=#00FF00>[Autosave]</color> Saving all open scenes and modified assets... (Reason: {reason})");
            
            // Saves open scenes
            EditorSceneManager.SaveOpenScenes();
            // Saves modified project assets (prefabs, materials, etc.)
            AssetDatabase.SaveAssets(); 
        }

        ResetTimer();
    }

    private static void ResetTimer()
    {
        nextSaveTime = EditorApplication.timeSinceStartup + (SaveIntervalMinutes * 60);
    }
}
#endif 