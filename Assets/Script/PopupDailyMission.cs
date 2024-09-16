using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.U2D;

public class PopupDailyMission : Popups
{
    public static PopupDailyMission Instance;
    [SerializeField] private QuestItem questItem;
    [SerializeField] private Transform scrollHolder;
    [SerializeField] private TextMeshProUGUI bigRewardProgressText;
    [SerializeField] private TextMeshProUGUI bigRewardAmount;
    [SerializeField] private Image bigRewardImage;
    [SerializeField] private Slider bigRewardProgressBar;
    [SerializeField] private TextMeshProUGUI bigRewardTimeCountdown;
    [SerializeField] private Button bigRewardClaimButton;
    [SerializeField] private GameObject bigRewardEffect;
    [SerializeField] private GameObject bigRewardEffect2;
    [SerializeField] private SpriteAtlas iconAtlas;
    private QuestManager questManager;
    private Action<QuestProgress> onQuestFinish;
    // private Action<QuestProgress> onQuestCreate;
    
    private Action<bool> _onResult;

    private void Start()
    {

        onQuestFinish = (questProgress) =>
        {
            questManager.onQuestFinish?.Invoke(questProgress);
            UpdateBigReward();
            // dùng tạm --------------------------
            foreach (Transform item in scrollHolder)
            {
                Destroy(item.gameObject);
            }
            foreach (var item in questManager.listQuestInProgress)
            {
                var newQuestItem = Instantiate(questItem, scrollHolder);
                newQuestItem.Init(item, onQuestFinish);
            }
            // -----------------------------------
        };
        // onQuestCreate = questManager.onCreateNewQuest;
    }
    void InitUI()
    {
        // Thay tham chiếu này tùy vào dự án
        // questManager = UserData.Instance.questManager;
        // hoặc
        if(questManager == null)
            questManager = FindObjectOfType<QuestManager>();
        bigRewardAmount.text = questManager.listDailyQuest.bigRewardAmount.ToString();
        SetBigRewardIcon();
        UpdateBigReward();
        foreach (Transform item in scrollHolder)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in questManager.listQuestInProgress)
        {
            var newQuestItem = Instantiate(questItem, scrollHolder);
            newQuestItem.Init(item, onQuestFinish);
        }
    }
    public void OnNewQuestCreate(QuestProgress questProgress){
        var newQuestItem = Instantiate(questItem, scrollHolder);
        newQuestItem.Init(questProgress, onQuestFinish);
    }
    public void UpdateTimeCountdown(int hour, int minute, int second){
        bigRewardTimeCountdown.text = $"Reset in {hour}h {minute}m {second}s";
    }
    public void UpdateBigReward()
    {   
        bigRewardProgressText.text = $"{questManager.questsFinished}/{questManager.bigRewardRequire}";
        bigRewardProgressBar.value = (float)questManager.questsFinished / questManager.bigRewardRequire;
        if(bigRewardProgressBar.value>=1){
            bigRewardClaimButton.enabled = true;
            bigRewardEffect.SetActive(true);
            bigRewardEffect2.SetActive(true);
        }
        else{
            bigRewardClaimButton.enabled = false;
            bigRewardEffect.SetActive(false);
            bigRewardEffect2.SetActive(false);
        }
    }
    public void OnBigRewardClaim(){
        Debug.Log("Big reward claimed");
    }

    public void SetBigRewardIcon(){
        switch(questManager.listDailyQuest.bigRewardType){
            case RewardType.Gold:
                bigRewardImage.sprite = iconAtlas.GetSprite("Coin");
            break;
            case RewardType.Gem:
                bigRewardImage.sprite = iconAtlas.GetSprite("Gem");
            break;
            case RewardType.Exp:
                bigRewardImage.sprite = iconAtlas.GetSprite("Exp");
            break;
            case RewardType.Item:
                bigRewardImage.sprite = iconAtlas.GetSprite("Item");
            break;
            default:
                bigRewardImage.sprite = iconAtlas.GetSprite("Unknown");
            break;
        }
    }
    #region BASE POPUP 
    static void CheckInstance(Action completed)//
    {
        if (Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupDailyMission>("Prefab/UI/PopupPrefabs/PopupDailyMission" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupDailyMission;
                if (asset != null)
                {
                    Instance = Instantiate(asset,
                        CanvasPopup4.transform,
                        false);

                    if (completed != null)
                    {
                        completed();
                    }
                }
            };

        }
        else
        {
            if (completed != null)
            {
                completed();
            }
        }
    }

    public static void Show()//
    {

        CheckInstance(() =>
        {
            Instance.Appear();
            Instance.InitUI();
        });

    }

    public static void Hide()
    {
        Instance.Disappear();
    }
    public override void Appear()
    {
        IsLoadBoxCollider = false;
        base.Appear();
        //Background.gameObject.SetActive(true);
        Panel.gameObject.SetActive(true);
    }
    public void Disappear()
    {
        //Background.gameObject.SetActive(false);
        base.Disappear(() =>
        {
            Panel.gameObject.SetActive(false);
        });
    }

    public override void Disable()
    {
        base.Disable();
    }

    public override void NextStep(object value = null)
    {
    }
    #endregion

}
