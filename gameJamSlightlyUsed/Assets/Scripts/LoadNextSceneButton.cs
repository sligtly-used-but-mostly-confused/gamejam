using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextSceneButton : MonoBehaviour {

	public void LoadNextScene()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}
