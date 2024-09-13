using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    [SerializeField] private QuestProgress questProgress;
    [SerializeField] private TextMeshProUGUI questTitle;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private Image questIcon;
    [SerializeField] private SpriteAtlas iconAtlas;
    [SerializeField] private RewardType rewardType;
    [SerializeField] private TextMeshProUGUI rewardAmount;
    [SerializeField] private Image rewardIcon;
    [SerializeField] private Slider questProgressBar;
    [SerializeField] private TextMeshProUGUI questProgressText;
    [SerializeField] private Button collectReward;
    [SerializeField] private Image finishedBackground;
    private Action<QuestProgress> onFinish;

    private void Start() {

    }
    public void Init(QuestProgress newQuestProgress, Action<QuestProgress> onFinishCallBack)
    {
        questProgress = newQuestProgress;
        questTitle.text = questProgress.GetQuestData().title;
        questDescription.text = questProgress.GetQuestData().description;
        rewardAmount.text = questProgress.GetQuestData().rewardAmount.ToString();
        SetRewardIcon();
        collectReward.enabled = false;
        onFinish = onFinishCallBack;
        UpdateProgress();
        if (questProgress.QuestStatus == QuestStatus.Finined)
            finishedBackground.gameObject.SetActive(true);
        else

            finishedBackground.gameObject.SetActive(false);
    }
    public void SetRewardIcon(){
        switch(questProgress.GetQuestData().rewardType){
            case RewardType.Gold:
                rewardIcon.sprite = iconAtlas.GetSprite("Coin");
            break;
            case RewardType.Gem:
                rewardIcon.sprite = iconAtlas.GetSprite("Gem");
            break;
            case RewardType.Exp:
                rewardIcon.sprite = iconAtlas.GetSprite("Exp");
            break;
            case RewardType.Item:
                rewardIcon.sprite = iconAtlas.GetSprite("Item");
            break;
            default:
                rewardIcon.sprite = iconAtlas.GetSprite("Unknown");
            break;
        }
    }
    public void UpdateProgress()
    {
        questProgressText.text = $"{questProgress.Progress}/{questProgress.Require}";
        questProgressBar.value = (float)questProgress.Progress / questProgress.Require;
        if (questProgress.QuestStatus == QuestStatus.Finined)
        {
            collectReward.interactable = true;
            collectReward.enabled = true;
            finishedBackground.gameObject.SetActive(true);
        }
    }
    public void OnRewardClaim()
    {
        onFinish?.Invoke(questProgress);
        // UserData.Instance.onQuestFinish?.Invoke(questProgress);
        Destroy(gameObject);
    }

}
