using System;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Uses first scene in build settings like boot scene and other for levels
/// </summary>
public class SceneProvider : ILevelProvider
{
    public Action<int> OnLevelStartLoading { get; }
    public Action<int> OnLevelLoaded { get; }
    public Action<int> OnLevelStartUnloading { get; }

    public void LoadLevel(int levelIndex)
    {
        OnLevelStartLoading?.Invoke(levelIndex);

        int sceneIndex = LevelIndexToSceneIndex(levelIndex);
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
        OnLevelLoaded?.Invoke(levelIndex);
    }

    public void UnloadLevel(int levelIndex)
    {
        OnLevelStartUnloading?.Invoke(levelIndex);

        int sceneIndex = LevelIndexToSceneIndex(levelIndex);
        SceneManager.UnloadSceneAsync(sceneIndex);
    }

    private int LevelIndexToSceneIndex(int levelIndex)
    {
        int levelCount = SceneManager.sceneCountInBuildSettings - 1; // Scene count without boot scene

        if (levelCount < 1) throw new Exception("No levels in build settings");

        int remainderedValue = levelIndex % levelCount;
        return remainderedValue + 1; // Ignoring first scene
    }
}