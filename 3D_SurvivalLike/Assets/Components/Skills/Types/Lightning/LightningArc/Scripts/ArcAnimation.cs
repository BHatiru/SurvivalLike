using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcAnimation : MonoBehaviour
{
    [SerializeField] private Transform _arcVFX;
    [SerializeField] private Transform _arcPoint2;
    [SerializeField] private Transform _arcPoint2OrignalPosition;
    
    [SerializeField] private Transform _arcPoint3;
    [SerializeField] private Transform _arcPoint3OrignalPosition;





    public void RotateArc(float rotationSpeed)
    {
        // rotate the arc to face the target
        _arcVFX.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime,  Space.Self);
    }
}
