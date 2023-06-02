using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ChainLightningSegment : MonoBehaviour
{
    //Reference to the visual effect component
    [SerializeField] private VisualEffect vfx;
    float startSize = 0f; // Initial size value
    [SerializeField] public float endScaleZ = 5f; // Final size value
    [SerializeField] float duration = 0.5f; // Duration of the size change
    private float timer = 0f; // Timer to track the elapsed time
    void Awake()
    {
        vfx = GetComponent<VisualEffect>();
        vfx.SetFloat("Length", startSize);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / duration);
        float currentScaleZ = Mathf.Lerp(startSize, endScaleZ, t);
        vfx.SetFloat("Length", currentScaleZ);
        vfx.SetFloat("Offset", endScaleZ);
    }


}
