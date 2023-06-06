using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SkillMenuPanel : MonoBehaviour
{
    private Button[] skillButtons;

    private GameManager gameManager;
    private SkillCastersManager skillManager;
    [SerializeField] Button _skillChoiceButtonPrefab;
    [SerializeField] Transform _skillBtnsParentPanel;

    [SerializeField] private int skillsToChoose = 3;
    
    private List<SkillData> possibleSkillChoices;
    void Start()
    {

        gameManager = GameManager.Instance;
        skillManager = SkillCastersManager.Instance;

        ExperienceManager.Instance.OnLevelUp += UpdateSkillMenu;

        
        UpdateChoosableSkillSet();
        ShufflePossibleSkillChoises();
        SetSkillMenuPanel();
    }


    public void UpdateSkillMenu(int level){
        Debug.Log("Update skill menu");
        UpdateChoosableSkillSet();
        ShufflePossibleSkillChoises();
        SetSkillMenuPanel();
    }
    public void UpdateChoosableSkillSet(){
        possibleSkillChoices = new List<SkillData>();
        //cycle to add all possible skills. skill is possible to choose if it is not yet acquired or if it is acquired but not max level
        foreach(SkillData skill in skillManager.nonacquiredSkills){
            skill.CurrentLevel = -1;
            possibleSkillChoices.Add(skill);
        }
        foreach(SkillData skill in skillManager.acquiredSkills){
            if(skill.CurrentLevel < skill.SkillMaxLvl){
                possibleSkillChoices.Add(skill);
            }
        }

        if(possibleSkillChoices.Count < skillsToChoose){
            skillsToChoose = possibleSkillChoices.Count;
        }
        skillButtons = new Button[skillsToChoose];
        Debug.Log("Possible skill choices: " + possibleSkillChoices.Count);
    }

    public void SetSkillMenuPanel(){
        for(int i = 0; i < skillsToChoose; i++){
            int currIndex = i;
            //create a new buttons for each skill and set their text to the skill name, and proper icons
            //Each time method is called a random skill is chosen from the possible skill choices
            Debug.Log("Index: "+ i);
            skillButtons[i] = Instantiate(_skillChoiceButtonPrefab, _skillBtnsParentPanel);
            skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = possibleSkillChoices[i].UpgradeInfo[possibleSkillChoices[i].CurrentLevel+1].skillName;
            skillButtons[i].GetComponentInChildren<IconHandler>().gameObject.GetComponent<Image>().sprite = possibleSkillChoices[i].skillIcon;
            //set button color to match skill color, experimental and will remove if doesnt work, 
            //begin
            ColorBlock colorVar =  skillButtons[i].colors;
            colorVar.highlightedColor = possibleSkillChoices[i].SkillColor[possibleSkillChoices[i].skillType];
            skillButtons[i].colors = colorVar;
            //end

            skillButtons[i].onClick.AddListener(()=>ChooseSkill(possibleSkillChoices[currIndex]));
        }
    }

    public void ShufflePossibleSkillChoises(){
        //shuffle the possible skill choices so that the order is random, this is done so that the player doesnt always get the same skill choices
        for (int i = 0; i < possibleSkillChoices.Count; i++) {
            SkillData temp = possibleSkillChoices[i];
            int randomIndex = Random.Range(i, possibleSkillChoices.Count-1);
            possibleSkillChoices[i] = possibleSkillChoices[randomIndex];
            possibleSkillChoices[randomIndex] = temp;
        }
        Debug.Log("Shuffled skill choices");
    }

    private void ChooseSkill(SkillData skill){
        //if skill is not yet acquired, acquire it, if it is, upgrade it
        Debug.Log("Skill chosen: " + skill.UpgradeInfo[skill.CurrentLevel+1].skillName);
        if(skillManager.nonacquiredSkills.Contains(skill)){
            Debug.Log("New skill");
            skillManager.AcquireNewSkill(skill);
        }
        else{
            Debug.Log("Skill upgrade");
            skillManager.AcquireUpgrade(skill);
        }
        gameManager.ChangeGameState(GameManager.GameState.Gameplay);
        DestroyPreviousSkillChoices();
        UpdateChoosableSkillSet();
        ShufflePossibleSkillChoises();
    }

    private void DestroyPreviousSkillChoices(){
        //destroy all previous skill choice buttons
        foreach(Button button in skillButtons){
            Destroy(button.gameObject);
        }
    }
    
    void OnDestroy()
    {
        ExperienceManager.Instance.OnLevelUp -= UpdateSkillMenu;
    }
}
