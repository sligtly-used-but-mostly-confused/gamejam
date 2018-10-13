using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : ObjectController {

	// Update is called once per frame
	void Update () {
        float minDistance = 1000000;
        GameObject closestPlayer = null;

        FindObjectsOfType<PlayerController>().ToList().ForEach(x =>
        {
            if((x.transform.position - transform.position).magnitude < minDistance)
            {
                closestPlayer = x.gameObject;
                minDistance = (x.transform.position - transform.position).magnitude;
            }
        });

        var dir = (closestPlayer.transform.position - transform.position).normalized;

        Move(dir * Time.deltaTime);
    }

    public void Hit()
    {
        LoseLife(1);
    }

    public override void Die()
    {
        var pos = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
        
        for(int i = pos.x - 1; i <= pos.x + 1; i++)
        {
            for (int j = pos.z - 1; j <= pos.z + 1; j++)
            {
                var platform = PlatformController.FindPlatformAt(new Vector3Int(i, pos.y, j));
                if (platform)
                {
                    platform.Raise();
                }
            }
        }

        

        Destroy(gameObject);
    }
}
