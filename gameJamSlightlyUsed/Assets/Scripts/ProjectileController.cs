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
        Debug.Log("here");
        if(collision.gameObject.GetComponent<EnemyController>())
        {
            var pos = new Vector2Int(Mathf.RoundToInt(collision.transform.position.x), Mathf.RoundToInt(collision.transform.position.z));
            PlatformController.FindPlatformAt(pos).Raise();

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
