using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrakingEnemyController : EnemyController {

    public override void Die()
    {
        var pos = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));

        for (int i = pos.x - 1; i <= pos.x + 1; i++)
        {
            for (int j = pos.z - 1; j <= pos.z + 1; j++)
            {
                var platform = PlatformController.FindPlatformAt(new Vector3Int(i, pos.y, j));
                if (platform)
                {
                    platform.Lower();
                }
            }
        }

        PlatformColor.UpdateAllColors();
        Destroy(gameObject);

    }
}
