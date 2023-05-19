using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float _speed = 20f;
    [SerializeField] int _damage = 10;
    Rigidbody2D _rb;

    [SerializeField] GameObject _explosion;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.AddForce(transform.forward * _speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.collider.GetComponent<IDamageable>();
        if (target != null) target.TakeDamage(_damage);
        ContactPoint2D cp = collision.GetContact(0);
        Instantiate(_explosion, cp.point, Quaternion.identity);
        Destroy(gameObject);
    }
}
