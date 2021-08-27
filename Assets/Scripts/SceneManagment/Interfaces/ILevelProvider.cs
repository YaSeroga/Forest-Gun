using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelProvider
{
    Action<int> OnLevelStartLoading { get; }
    Action<int> OnLevelLoaded { get; }
    Action<int> OnLevelStartUnloading { get; }

    void LoadLevel(int levelIndex);
    void UnloadLevel(int levelIndex);
}
