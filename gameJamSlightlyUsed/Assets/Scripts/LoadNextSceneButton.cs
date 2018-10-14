using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextSceneButton : MonoBehaviour {

    private void Update()
    {
        MappedInput.InputDevices.ForEach(x =>
        {
            if(x.GetButtonDown(MappedButton.Ready))
            {
                LoadNextScene();
            }
        });
    }

    public void LoadNextScene()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}
