using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialHandler : MonoBehaviour
{
    [SerializeField] string _dashEffectProperty;

    Material _material;
    int _effectID;

    private void Awake()
    {
        _material = GetComponentInChildren<SpriteRenderer>().materials[0];

        _effectID = Shader.PropertyToID(_dashEffectProperty);
    }

    public void ToggleDashEffect(bool on)
    {
       if(on) _material.SetFloat(_effectID, 1);
       else _material.SetFloat(_effectID, 0);
    }
}
