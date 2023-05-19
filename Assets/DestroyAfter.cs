using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public bool _destroyParent = true; 
    public float _destroyAfterTime = .2f;
    private void Awake()
    {
        if(transform.parent != null && _destroyParent) Destroy(transform.parent.gameObject, _destroyAfterTime);
        else Destroy(gameObject, _destroyAfterTime);
    }
}
