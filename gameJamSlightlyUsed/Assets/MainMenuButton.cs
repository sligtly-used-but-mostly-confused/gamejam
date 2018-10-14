using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour {


	void Update () {
        MappedInput.InputDevices.ForEach(x =>
        {
            if (x.GetButtonDown(MappedButton.Ready))
            {
                LevelManager.Instance.LoadNextLevel();
            }
        });
        
    }
}
