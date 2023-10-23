using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Dash : MonoBehaviour
{
    [Header("Dash Values")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    [SerializeField] private AnimationCurve _dashCurve;

    [Header("DashEffects")]
    [SerializeField] GameObject _dashVFXObj;
    [SerializeField] ParticleSystem _dashVFX;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _dashVFX = _dashVFXObj.GetComponentInChildren<ParticleSystem>();
        _dashVFX.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) DashCall();
    }

    private void DashCall()
    {
        StartCoroutine(nameof(Dasher));
    }

    private IEnumerator Dasher()
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        PlayerMaterialHandler playerMaterialHandler = GetComponent<PlayerMaterialHandler>();

        playerMaterialHandler.ToggleDashEffect(true);
        _dashVFX.Play();

        float startTime = Time.time;

        while (Time.time < startTime + _dashTime)
        {
            _rb.MovePosition( transform.position + new Vector3(pm._movementDirection.x, pm._movementDirection.y, 0) 
                * _dashSpeed
                * (_dashCurve.Evaluate(Time.time - startTime) / _dashTime)
                * Time.deltaTime);

            yield return null;
        }

        playerMaterialHandler.ToggleDashEffect(false);
        _dashVFX.Stop();
    }
}
