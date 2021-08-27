using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelProvider chosenLevelProvider;
    private ILevelProvider levelProvider;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        levelProvider = GetLevelProvider(chosenLevelProvider);
        levelProvider.LoadLevel(0);
    }

    private ILevelProvider GetLevelProvider(LevelProvider chosenLevelProvider)
    {
        switch (chosenLevelProvider)
        {
            case LevelProvider.SceneProvider:
                return new SceneProvider();
        }

        throw new ArgumentException();
    }
}