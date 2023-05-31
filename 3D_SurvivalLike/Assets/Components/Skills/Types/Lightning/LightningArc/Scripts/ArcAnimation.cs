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

    [SerializeField] private Transform _arcEndPoint;

    public float noisePower = 1f;
    public float noiseFrequency = 20f;

    public void SetArcRadius(float radius)
    {
        // set the end point of the arc to the radius, dont change the y position
        _arcEndPoint.position = transform.forward * radius; 
        _arcEndPoint.position = new Vector3(_arcEndPoint.position.x, 1f, _arcEndPoint.position.z);
    }
    // create a noise by moving the intermediate points of the arc to the left and right periodically
    public void CreateNoise()
    {
        _arcPoint2.localPosition = _arcPoint2OrignalPosition.localPosition + new Vector3(Random.Range(-noisePower, noisePower), 0f, Random.Range(-noisePower, noisePower));
        _arcPoint3.localPosition = _arcPoint3OrignalPosition.localPosition - new Vector3(Random.Range(-noisePower, noisePower), 0f, Random.Range(-noisePower, noisePower));
    }

    public void RotateArc(float rotationSpeed)
    {
        // rotate the arc to face the target
        _arcVFX.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime,  Space.Self);
    }
}
