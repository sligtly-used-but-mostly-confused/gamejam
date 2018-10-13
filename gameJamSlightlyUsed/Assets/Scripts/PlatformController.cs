using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Raise()
    {
        var topObject = _platForms.Peek();
        var child = Instantiate(_platformChildPrefab);
        child.transform.SetParent(gameObject.transform);
        child.transform.position = topObject.transform.position + Vector3.up * topObject.transform.localScale.y;
        _platForms.Push(child);
    }

    public void Lower()
    {
        var topObject = _platForms.Pop();
        Destroy(topObject);
    }
}
