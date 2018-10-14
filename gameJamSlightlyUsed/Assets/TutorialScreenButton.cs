using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreenButton : MonoBehaviour {

    public GameObject TopPanel;

    private void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update () {
        MappedInput.InputDevices.ForEach(x =>
        {
            if (x.GetButtonDown(MappedButton.Ready))
            {
                KillScreen();
            }
        });
    }

    public void KillScreen()
    {
        Time.timeScale = 1;
        Destroy(TopPanel);
    }
}
