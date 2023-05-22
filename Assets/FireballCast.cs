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
    AudioSource _audioSource;
    [SerializeField] AudioClip _pop;
    // Start is called before the first frame update
    void Start()
    {
        _timeSinceLastUse = _cooldown;
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastUse += Time.deltaTime;

        if (_timeSinceLastUse >= _cooldown && Input.GetKey(KeyCode.Space))
        {
            _timeSinceLastUse = 0;
            CallFireball();
        }
    }

    private void CallFireball()
    {
        _anim.SetTrigger("Fireball");
        _audioSource.PlayOneShot(_pop);
    }

    public void ExecuteFirebal()
    {
       
        Instantiate(_fireball, _firePoint.position, Quaternion.LookRotation(/*AimFireball()*/ _firePoint.position - transform.position));
    }

    private Vector3 AimFireball()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePos - _firePoint.position;
    }
}
    