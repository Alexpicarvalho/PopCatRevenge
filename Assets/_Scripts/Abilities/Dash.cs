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
    [SerializeField] private float _dashCooldown;
    [SerializeField] private AnimationCurve _dashCurve;

    [Header("DashEffects")]
    [SerializeField] GameObject _dashVFXObj;
    [SerializeField] ParticleSystem _dashVFX;

    private Rigidbody2D _rb;
    private float _lastDashTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _dashVFX = _dashVFXObj.GetComponentInChildren<ParticleSystem>();
        _lastDashTime = _dashCooldown;

        _dashVFX.Stop();
    }

    private void Update()
    {
        _lastDashTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && _lastDashTime >= _dashCooldown) DashCall();
    }

    private void DashCall()
    {
        _lastDashTime = 0;
        StartCoroutine(Dasher());
    }

    private IEnumerator Dasher()
    {
        //Cache Values
        PlayerMovement pm = GetComponent<PlayerMovement>();
        PlayerMaterialHandler playerMaterialHandler = GetComponent<PlayerMaterialHandler>();

        playerMaterialHandler.ToggleDashEffect(true);
        _dashVFX.Play();


        float startTime = Time.time;
        //Vector3 dashDirection = new Vector3(pm._movementDirection.x, pm._movementDirection.y, 0).normalized;

        pm.enabled = false;
        _rb.gravityScale = -.5f;

        while (Time.time < startTime + _dashTime)
        {
            float normalizedTime = (Time.time - startTime) / _dashTime;
            float curveValue = /*_dashCurve.Evaluate(normalizedTime)*/1;

            //transform.position += _dashSpeed * curveValue * Time.deltaTime * transform.right;
            _rb.AddForce(transform.right * _dashSpeed * curveValue, ForceMode2D.Impulse);

            yield return null;
        }

        playerMaterialHandler.ToggleDashEffect(false);
        pm.enabled = true;
        _rb.gravityScale = 1;
        _dashVFX.Stop();
    }
    private void OnDrawGizmos()
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * 10);
    }
}
