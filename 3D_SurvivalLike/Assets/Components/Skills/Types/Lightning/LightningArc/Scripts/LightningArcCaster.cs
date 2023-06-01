using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningArcCaster : BaseSkill, ICastable
{
    [SerializeField] private float rotationSpeed = 4.3f;  // Speed of rotation for the raycast
    private LightningArc[] lightningArcs;
    private int oldQuantity;
    void Start()
    {
        UpdateStats(_SkillData);
        oldQuantity = quantity;
        rotationSpeed = speed;
        SpawnNewArcs();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Level++;
            UpdateStats(_SkillData);
        }
        Cast();
    }

    public void Cast()
    {
        //TODO: cast several arcs based on quantity stat, set arcs initial rotation properly so that they would take whole circle evenly
        if (LevelChanged())
        {
            DestroyOldArcs();
            SpawnNewArcs();
            oldQuantity = quantity;
        }
        foreach (LightningArc arc in lightningArcs)
        {
            arc.Cast(rotationSpeed, radius, damage);
        }
    }

    private bool LevelChanged() => oldQuantity != quantity;

    private void DestroyOldArcs(){
        for (int i = 0; i < oldQuantity; i++)
        {
            lightningArcs[i].DestroyArc();
        }
    }
    private void SpawnNewArcs(){
        lightningArcs = new LightningArc[quantity];
        for (int i = 0; i < quantity; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 360 / quantity * (i+1), 0);
            lightningArcs[i] = Instantiate(VFX_skillObj, transform.position, Quaternion.identity, transform).GetComponent<LightningArc>();
            lightningArcs[i].SetArcRotation(rotation);
            lightningArcs[i].SetArcRadius(radius);
            
        }
    }
}



