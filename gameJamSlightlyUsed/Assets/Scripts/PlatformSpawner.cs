using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject _platformPrefab;

    [SerializeField]
    private Vector2Int PlatformGridSize;

	// Use this for initialization
	void Start () {
		for(int i = -PlatformGridSize.x; i <= PlatformGridSize.x; i++)
        {
            for (int j = -PlatformGridSize.y; j <= PlatformGridSize.y; j++)
            {
                var obj = Instantiate(_platformPrefab);
                obj.transform.position = new Vector3(i, 0, j);
            }
        }
	}
}
