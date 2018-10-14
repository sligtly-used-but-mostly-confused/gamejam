using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevelButton : MonoBehaviour {

	public void ResetLevel()
    {
        LevelManager.Instance.ReloadLevel();
    }

    public void RestartGame()
    {
        LevelManager.Instance.RestartGame();
    }

    public void SkipLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}
