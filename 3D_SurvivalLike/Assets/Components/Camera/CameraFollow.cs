using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] Vector3 _offset;
    private Vector3 _velocity = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        if(_target != null){
            Vector3 targetPosition = _target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }
    }
}
