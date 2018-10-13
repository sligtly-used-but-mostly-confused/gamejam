using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private void Start()
    {
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.GetComponent<EnemyController>())
        {
            collision.gameObject.GetComponent<EnemyController>().Hit();
            Destroy(gameObject);
        }
    }
}
