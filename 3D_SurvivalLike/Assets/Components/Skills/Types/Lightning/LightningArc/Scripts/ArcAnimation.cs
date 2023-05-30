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

    
    private LightningArc _lightningArc;
    [SerializeField] private float _noisePower = 1f;
    [SerializeField] private float _noiseFrequency = 20f;
    private float timer;

    private const int SPEED_COEFFICIENT = 20; 
    void Start()
    {
        _lightningArc = GetComponent<LightningArc>();
    }

    private void Update()
    {
        RotateArc();
        timer += Time.deltaTime;
        if (timer > 1/_noiseFrequency)
        {
            timer = 0f;
            CreateNoise();
        }
    }

    // create a noise by moving the intermediate points of the arc to the left and right periodically
    private void CreateNoise()
    {
        _arcPoint2.localPosition = _arcPoint2OrignalPosition.localPosition + new Vector3(Random.Range(-_noisePower, _noisePower), 0f, Random.Range(-_noisePower, _noisePower));
        _arcPoint3.localPosition = _arcPoint3OrignalPosition.localPosition - new Vector3(Random.Range(-_noisePower, _noisePower), 0f, Random.Range(-_noisePower, _noisePower));
    }

    private void RotateArc()
    {
        // rotate the arc to face the target
        _arcVFX.transform.Rotate(Vector3.up, SPEED_COEFFICIENT *_lightningArc.rotationSpeed * Time.deltaTime,  Space.Self);
    }
}
