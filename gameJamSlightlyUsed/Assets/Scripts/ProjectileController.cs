using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("here");
        if(collision.gameObject.GetComponent<EnemyController>())
        {
            Destroy(collision.gameObject);

        }
    }
}
