using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : ObjectController {

    public int EnemySqrtMaxHealth = 3;

	// Update is called once per frame
	void Update () {
        float minDistance = 1000000;
        GameObject closestPlayer = null;


        foreach(var x in FindObjectsOfType<PlayerController>())
        {
            if ((x.transform.position - transform.position).magnitude < minDistance)
            {
                closestPlayer = x.gameObject;
                minDistance = (x.transform.position - transform.position).magnitude;
            }
        }

        var dir = (closestPlayer.transform.position - transform.position);
        dir = new Vector3(dir.x, 0, dir.z).normalized;

        Move(dir * Time.deltaTime);
    }

    public void Hit(ObjectController other)
    {
        LoseLife(1, other);
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

    public override void OnKillOther(ObjectController other)
    {
        base.OnKillOther(other);
        LevelUp();
        _SqrtMaxHealth = Mathf.Min(_SqrtMaxHealth + 1, EnemySqrtMaxHealth);
        ResetLifes();
    }

    protected override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().LoseLife(1, this);
        }
    }
}
