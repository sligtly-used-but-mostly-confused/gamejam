using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    [SerializeField]
    private float _moveSpeed = 5;

    public void Move(Vector3 dir)
    {
        transform.position += dir * _moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlatformChild>())
        {
            if(collision.gameObject.GetComponent<PlatformChild>().Owner.GetTopObject() == collision.gameObject)
            {
                var newY = collision.transform.localScale.y + collision.transform.position.y;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
    }
}
