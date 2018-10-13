using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectController {
    InputDevice input;
    protected readonly Vector3Int[] _moveDirs = { new Vector3Int(1,0,0), new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 1), new Vector3Int(0, 0, -1) };

    [SerializeField]
    private float _shootSpeedStep = 2.5f;
    [SerializeField]
    private float _shootSpeed = 5;
    [SerializeField]
    private float _shootCooldownStep = -.1f;
    [SerializeField]
    private float _shootCooldown = 1;
    [SerializeField]
    private GameObject _aimingReticle;
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private int _controllerId = 3;

    [SerializeField]
    protected Vector2 _shootSpeedRange = new Vector2(5, 15);
    [SerializeField]
    protected Vector2 _shootCooldownRange = new Vector2(.1f, 1);

    private Coroutine ShootingCooldownCoroutine = null;

    public override void Start()
    {
        base.Start();

        var mat = PlayerManager.Instance.GetPlayerMaterial();
        GetComponent<Renderer>().material = mat;

        var endpoint = PlayerManager.Instance.GetEndpoint();
        endpoint.GetComponent<Renderer>().material = mat;
        endpoint.Owner = this;
    }

    void Update () {
        input = MappedInput.InputDevices[_controllerId];
        Vector3 leftStick = input.GetAxis2DCircleClamp(MappedAxis.Horizontal, MappedAxis.Vertical);
        var fixedLeftStick = Camera.main.transform.rotation * new Vector3(leftStick.x, 0, leftStick.y);
        fixedLeftStick = new Vector3(fixedLeftStick.x, 0, fixedLeftStick.z);
        
        Move(fixedLeftStick * Time.deltaTime);

        Vector3 rightStick = input.GetAxis2DCircleClamp(MappedAxis.AimX, MappedAxis.AimY);

        var aimDir = new Vector3(rightStick.x, 0, rightStick.y).normalized;
        var fixedAimDir = Camera.main.transform.rotation * aimDir;
        fixedAimDir = new Vector3(fixedAimDir.x, 0, fixedAimDir.z);

        _aimingReticle.transform.localPosition = fixedAimDir;

        if (input.GetAxis(MappedAxis.ShootGravGun) != 0 && aimDir.magnitude > 0 && ShootingCooldownCoroutine == null)
        {
            ShootingCooldownCoroutine = StartCoroutine(ShootOnCooldown());
        }

        if(input.GetAxis(MappedAxis.ChangeCameraAngle) != 0)
        {
            float changeCameraDir = input.GetAxis(MappedAxis.ChangeCameraAngle);
            CameraController.Instance.Rotate(-changeCameraDir);
        }
    }

    public override void Die()
    {
        transform.position = FindObjectOfType<RespawnPosition>().transform.position;
        ChangeLife(_maxHealth);
        _shootSpeed = _shootSpeedRange.x ;
        _moveSpeed = _moveSpeedRange.x;
        _shootCooldown = _shootCooldownRange.y;
    }

    public override void OnKillOther(ObjectController other)
    {
        base.OnKillOther(other);
        for(int i = 0; i < other.NumKills + 1; i++)
            LevelUp();
    }
    
    public override void LevelUp()
    {
        base.LevelUp();
        _shootSpeed = Mathf.Clamp(_shootSpeed + _shootSpeedStep, _shootSpeedRange.x, _shootSpeedRange.y);
        _shootCooldown = Mathf.Clamp(_shootCooldown + _shootCooldownStep, _shootCooldownRange.x, _shootCooldownRange.y);
    }

    private IEnumerator ShootOnCooldown()
    {
        Shoot(_aimingReticle.transform.localPosition);
        yield return new WaitForSeconds(_shootCooldown);
        ShootingCooldownCoroutine = null;
    }

    private void Shoot(Vector3 dir)
    {
        var projectile = Instantiate(_projectilePrefab);
        projectile.transform.position = _aimingReticle.transform.position;
        projectile.GetComponent<Rigidbody>().velocity = dir * _shootSpeed;
        projectile.GetComponent<ProjectileController>().Owner = this;
    }

    private static Vector3Int ClosestDirection(Vector3 v, Vector3Int[] compass)
    {
        var maxDot = -Mathf.Infinity;
        var ret = Vector3Int.zero;

        foreach (var dir in compass)
        {
            var t = Vector3.Dot(v, dir);
            if (t > maxDot)
            {
                ret = dir;
                maxDot = t;
            }
        }

        return ret;
    }
}
