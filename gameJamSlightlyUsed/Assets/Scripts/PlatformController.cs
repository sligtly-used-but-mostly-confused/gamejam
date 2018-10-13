using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformController : MonoBehaviour {

    [SerializeField]
    private GameObject _platformChildPrefab;

    private Stack<GameObject> _platForms = new Stack<GameObject>();

    private void Awake()
    {
        _platForms.Push(gameObject);
    }

    private void Start()
    {

    }

    public GameObject GetTopObject()
    {
        return _platForms.Peek();
    }

    public void Raise()
    {
        var topObject = _platForms.Peek();
        var child = Instantiate(_platformChildPrefab);
        child.transform.SetParent(gameObject.transform);
        child.transform.position = topObject.transform.position + Vector3.up * topObject.transform.localScale.y;
        child.GetComponent<PlatformChild>().Owner = this;
        _platForms.Push(child);
    }

    public void Lower()
    {
        var topObject = _platForms.Pop();
        Destroy(topObject);
    }

    public static PlatformController FindPlatformAt(Vector2Int pos)
    {
        
        var foundObjs = FindObjectsOfType<PlatformController>().Where(x =>
        {
            Vector2Int location = new Vector2Int(Mathf.RoundToInt(x.transform.position.x), Mathf.RoundToInt(x.transform.position.z));
            return location == pos;
        });

        return foundObjs.First();
    }
}
