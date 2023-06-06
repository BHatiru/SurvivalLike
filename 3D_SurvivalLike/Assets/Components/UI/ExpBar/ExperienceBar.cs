using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    private Slider slider;
    private TextMeshProUGUI levelText;
    private void Start()
    {
        slider = GetComponent<Slider>();
        levelText = GetComponentInChildren<TextMeshProUGUI>();
        ExperienceManager.Instance.OnExperienceChange += UpdateExperienceBar;
        ExperienceManager.Instance.OnLevelUp += UpdateLevelText;
    }


    public void UpdateExperienceBar(int currentExp, int maxExp)
    {
        slider.maxValue = maxExp;
        slider.value = currentExp;
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = "Lvl. " + level;
    }

    private void OnDestroy()
    {
        ExperienceManager.Instance.OnExperienceChange -= UpdateExperienceBar;
        ExperienceManager.Instance.OnLevelUp -= UpdateLevelText;
    }
}
