using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamageable
{
    [SerializeField] int _maxHP;
    [SerializeField] int _currentHP;

    [Header("Refs")]
    FlashOnDamage _flash;
    Animator _anim;
    public virtual void TakeDamage(int amount)
    {
        _currentHP -= amount;
        _flash.Flash();
        _anim.SetTrigger("Hit");
        if (_currentHP <= 0) _currentHP = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        _flash = GetComponentInChildren<FlashOnDamage>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public interface IDamageable
{
    void TakeDamage(int amount);
}
