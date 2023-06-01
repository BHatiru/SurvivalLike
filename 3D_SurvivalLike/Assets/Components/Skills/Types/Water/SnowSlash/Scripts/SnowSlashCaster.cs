using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSlashCaster : BaseSkill, ICaster
{
    [SerializeField] private float slashRange = 4f;
    [SerializeField] private float _slashStackOffset = 0.3f;
    [SerializeField] private float _delayBetweenSlashes = 0.1f;
    private int OldLvl;
    private int OldQuantity;
    SnowSlash[] _slashStack;
    void Start()
    {
        UpdateStats(_SkillData);
        OldLvl = 0;
        OldQuantity = quantity;
        
        slashRange = radius;
        _slashStack = new SnowSlash[quantity];
        SpawnNewSlashes();
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown){
            Cast();
        }
        //TODO test feature, remove later
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Level++;
            UpdateStats(_SkillData);
            slashRange = radius;
        }
    }

    public void Cast()
    {
        //rotate in same direction as a player
        transform.rotation = Quaternion.Euler(0, player.rotation.eulerAngles.y, 0);
        //cast the snow slash several times based on quantity with small delay between each slash
        StartCoroutine(ConsequtiveDelayedSlashes(_delayBetweenSlashes));
        timer = 0f;
    }

    private IEnumerator ConsequtiveDelayedSlashes(float delay)
    {
        if(LevelChanged()){
            DestroyOldSlashes();
            SpawnNewSlashes();
            OldLvl = Level;
            OldQuantity = quantity;
        }
        foreach(SnowSlash slash in _slashStack){
            slash.Cast(damage, radius);
            yield return new WaitForSeconds(delay);
        }
    }

    private void DestroyOldSlashes(){
        Debug.Log("Entering the destroy method\n Old quantity: " + OldQuantity + "\n New quantity: " + quantity + "\n");
        for(int i=0; i < OldQuantity; i++)
            Destroy(_slashStack[i].gameObject);
    }
    private void SpawnNewSlashes(){
        _slashStack = new SnowSlash[quantity];
        for(int i = 0; i < quantity; i++){
            _slashStack[i] = SpawnSnowSlash();
            _slashStack[i].InitializeSlash(transform, radius);
            _slashStack[i].ApplyOffset(i*_slashStackOffset, radius);
            _slashStack[i].UpdateEffect(i);
            //_slashStack[i].transform.position =  new Vector3(0f,0f,_slashStackOffset*i); // offset the slashes in the stack so they don't overlap
            //rotate the slashes so they are looking forward
            //_slashStack[i].transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private bool LevelChanged() => OldQuantity != quantity;


    private SnowSlash SpawnSnowSlash(){
        SnowSlash slash = Instantiate(VFX_skillObj, transform.position, Quaternion.identity, transform).GetComponent<SnowSlash>();
        return slash;
    }
}
