using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;

    [SerializeField]
    private List<string> _levels = new List<string>();

    public string CurrentLevel;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextLevel()
    {
        int index = _levels.IndexOf(CurrentLevel);
        index++;
        index = (index + _levels.Count) % _levels.Count;
        CurrentLevel = _levels[index];
        SceneManager.LoadScene(CurrentLevel);
    }
}
