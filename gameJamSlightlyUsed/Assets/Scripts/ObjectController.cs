using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    [SerializeField]
    protected Vector2 _moveSpeedRange = new Vector2(5,15);
    [SerializeField]
    protected float _moveSpeed = 5;
    [SerializeField]
    protected float MoveSpeedStep = .25f;
    [SerializeField]
    protected int _SqrtMaxHealth = 2;
    protected int _maxHealth = 4;
    [SerializeField]
    private float _loseLifeCoolDownTime = 1;
    [SerializeField]
    private GameObject _healthPrefab;
    [SerializeField]
    private int _life;
    [SerializeField]
    private Material _fullLifeMat;
    [SerializeField]
    private Material _emptyLifeMat;
    [SerializeField]
    private float _healthYOffset = 1;
    public int NumKills = 0;

    private Coroutine _loseLifeOnCoolDownCoroutine;
    private List<GameObject> _healthObjects = new List<GameObject>();

    public virtual void Start()
    {
        ResetLifes();
    }

    protected void ResetLifes()
    {
        _healthObjects.ForEach(x => Destroy(x));
        _healthObjects.Clear();

        _maxHealth = _SqrtMaxHealth * _SqrtMaxHealth;

        float healthSize = .5f / (float)_SqrtMaxHealth;

        for (int i = 0; i < _SqrtMaxHealth; i++)
        {
            for (int j = 0; j < _SqrtMaxHealth; j++)
            {
                var health = Instantiate(_healthPrefab);
                health.transform.SetParent(transform);
                health.transform.localScale = new Vector3(1, 1, 1) * healthSize;
                health.transform.localPosition = new Vector3(i * healthSize * 2 - Mathf.Sqrt(_maxHealth) * healthSize / 2, _healthYOffset,
                                                             j * healthSize * 2 - Mathf.Sqrt(_maxHealth) * healthSize / 2);
                _healthObjects.Add(health);
            }
        }

        _life = _maxHealth;
    }

    public virtual void OnKillOther(ObjectController other)
    {
        NumKills++;
    }

    public virtual void LevelUp()
    {
        _moveSpeed = Mathf.Clamp(_moveSpeed + MoveSpeedStep, _moveSpeedRange.x, _moveSpeedRange.y);
    }

    public void Move(Vector3 dir)
    {
        transform.position += dir * _moveSpeed;
    }

    public void LoseLife(int amount, ObjectController attacker)
    {
        if(_loseLifeOnCoolDownCoroutine == null)
        {
            _loseLifeOnCoolDownCoroutine = StartCoroutine(LoseLifeOnCoolDown(amount, attacker));
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator LoseLifeOnCoolDown(int amount, ObjectController attacker)
    {
        _life -= amount;
        if(_life <= 0)
        {
            attacker.OnKillOther(this);
            Die();
        }

        ChangeLife(_life);
        yield return new WaitForSeconds(_loseLifeCoolDownTime);
        _loseLifeOnCoolDownCoroutine = null;
    }

    protected void ChangeLife(int newLife)
    {
        _life = newLife;

        int i;
        for (i = 0; i < _life; i++)
        {
            _healthObjects[i].GetComponent<Renderer>().material = _fullLifeMat;
        }

        for (; i < _maxHealth; i++)
        {
            _healthObjects[i].GetComponent<Renderer>().material = _emptyLifeMat;
        }
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlatformChild>())
        {
            if(collision.gameObject.GetComponent<PlatformChild>().Owner.GetTopObject() == collision.gameObject)
            {
                var newY = collision.transform.lossyScale.y/2 + collision.transform.position.y + transform.lossyScale.y/2;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
    }
}
