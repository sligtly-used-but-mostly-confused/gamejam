using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ObjectController {

	// Update is called once per frame
	void Update () {

        var dir = (FindObjectOfType<PlayerController>().transform.position - transform.position).normalized;

        Move(dir * Time.deltaTime);
    }
}
