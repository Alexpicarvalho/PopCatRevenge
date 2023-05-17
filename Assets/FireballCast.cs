using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCast : MonoBehaviour
{
    [SerializeField] Transform _firePoint;
    [SerializeField] GameObject _fireball;
    [SerializeField] float _cooldown = 1f;
    float _timeSinceLastUse;

    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _timeSinceLastUse = _cooldown;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastUse += Time.deltaTime;

        if (_timeSinceLastUse >= _cooldown && Input.GetMouseButtonDown(0))
        {
            _timeSinceLastUse = 0;
            CallFireball();
        }
    }

    private void CallFireball()
    {
        _anim.SetTrigger("Fireball");
    }

    public void ExecuteFirebal()
    {
        Instantiate(_fireball, _firePoint.position, Quaternion.LookRotation(AimFireball()));
    }

    private Vector3 AimFireball()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePos - _firePoint.position;
    }
}
    