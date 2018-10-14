using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformController : MonoBehaviour {

    [SerializeField]
    private GameObject _platformChildPrefab;

    private Stack<GameObject> _platForms = new Stack<GameObject>();

    private static List<GameObject> Platforms = new List<GameObject>(); 

    private void Awake()
    {
        
        
    }

    private void Start()
    {
        _platForms.Push(gameObject);
        Platforms.Add(gameObject);
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
        child.transform.localScale = Vector3.one;
        child.transform.position = topObject.transform.position + Vector3.up * topObject.transform.lossyScale.y;
        child.GetComponent<PlatformChild>().Owner = this;
        _platForms.Push(child);
        Platforms.Add(child);
    }

    public void Lower()
    {
        if(_platForms.Count == 1)
        {
            return;
        }

        var topObject = _platForms.Pop();
        Destroy(topObject);
    }

    public static PlatformController FindPlatformAt(Vector3Int pos)
    {
        var foundObjs = FindObjectsOfType<PlatformController>().Where(x =>
        {
            Vector2Int location = new Vector2Int(Mathf.RoundToInt(x.transform.position.x), Mathf.RoundToInt(x.transform.position.z));
            return location == new Vector2Int(pos.x, pos.z);
        });

        if(foundObjs.Count() == 0)
        {
            return null;
        }

        float minDistanceY = 1000000;
        PlatformController ClosestObj = null;

        foundObjs.ToList().ForEach(x =>
        {
            var distanceY = Mathf.Abs(x.transform.position.y - pos.y);
            if(minDistanceY > distanceY)
            {
                ClosestObj = x;
                minDistanceY = distanceY;
            }
        });



        return ClosestObj;
    }
}
