using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private float _minOffset = 1f;
    [SerializeField] private float _maxOffset = 2f;

    private Vector3 ogPosition;
    private Vector3 ogScale;
    private Vector3 targetPosition;
    private float timer;
    void Start()
    {
        transform.LookAt(2*transform.position - Camera.main.transform.position);
        float direction = UnityEngine.Random.rotation.eulerAngles.y;
        ogPosition = transform.position;
        ogScale = transform.localScale;
        float dist = UnityEngine.Random.Range(_minOffset, _maxOffset);
        targetPosition= ogPosition + (Quaternion.Euler(0f, direction, 0f) * new Vector3(0f, dist, dist));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = _lifeTime/2f;
        if (timer >= _lifeTime)
        {
            Destroy(gameObject);
        }else if (timer >= fraction)
        {
            transform.localScale = Vector3.Lerp(ogScale, Vector3.zero, (timer-fraction) / fraction);
        }
        transform.localPosition = Vector3.Lerp(ogPosition, targetPosition, timer / _lifeTime);
        //transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer / _lifeTime);
    }

    public void SetDamageText(float damage)
    {
        _damageText.text = Mathf.RoundToInt(damage).ToString();
    }
}
