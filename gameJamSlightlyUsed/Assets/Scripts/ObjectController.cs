﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    [SerializeField]
    private float _moveSpeed = 5;
    [SerializeField]
    private int _SqrtMaxHealth = 2;
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

    private Coroutine _loseLifeOnCoolDownCoroutine;
    private List<GameObject> _healthObjects = new List<GameObject>();

    private void Start()
    {
        _maxHealth = _SqrtMaxHealth * _SqrtMaxHealth;

        float healthSize = .5f / (float) _SqrtMaxHealth;

        for (int i = 0; i < _SqrtMaxHealth; i++)
        {
            for(int j = 0; j < _SqrtMaxHealth; j++)
            {
                var health = Instantiate(_healthPrefab);
                health.transform.SetParent(transform);
                health.transform.localScale = new Vector3(1, 1, 1) * healthSize;
                health.transform.localPosition = new Vector3(i * healthSize * 2 - Mathf.Sqrt(_maxHealth) * healthSize / 2, 1,
                                                             j * healthSize * 2 - Mathf.Sqrt(_maxHealth) * healthSize / 2);
                _healthObjects.Add(health);
            }
        }

        _life = _maxHealth;
    }

    public void Move(Vector3 dir)
    {
        transform.position += dir * _moveSpeed;
    }

    public void LoseLife(int amount)
    {
        if(_loseLifeOnCoolDownCoroutine == null)
        {
            _loseLifeOnCoolDownCoroutine = StartCoroutine(LoseLifeOnCoolDown(amount));
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator LoseLifeOnCoolDown(int amount)
    {
        _life -= amount;
        if(_life <= 0)
        {
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

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlatformChild>())
        {
            if(collision.gameObject.GetComponent<PlatformChild>().Owner.GetTopObject() == collision.gameObject)
            {
                var newY = collision.transform.lossyScale.y/2 + collision.transform.position.y + transform.lossyScale.y/2;
                Debug.Log(newY);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
    }
}
