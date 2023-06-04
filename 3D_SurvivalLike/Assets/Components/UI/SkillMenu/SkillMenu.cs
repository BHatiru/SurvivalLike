
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillMenu : MonoBehaviour
{
    [SerializeField] private GameObject _skillMenu;
    [SerializeField] private PlayerInput playerInput;
    void Start()
    {
        ExperienceManager.Instance.OnLevelUp += OpenSkillMenu;
    }

    public void OpenSkillMenu(int level)
    {  
        if (level == 1) return;
        //need to disable keyboart input
        playerInput.enabled = false;
        _skillMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSkillMenu()
    {
        playerInput.enabled = true;
        _skillMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ChooseSkill(){
        //SKill choice logic

        //Close menu after choice
        CloseSkillMenu();
    }

    void OnDestroy()
    {
        ExperienceManager.Instance.OnLevelUp -= OpenSkillMenu;
    }
}
