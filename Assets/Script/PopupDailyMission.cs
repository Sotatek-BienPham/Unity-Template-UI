using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
// using Hellmade.Sound;
using System.Linq;

public class PopupDailyMission : Popups
{
    public static PopupDailyMission Instance;
    [SerializeField] private QuestItem questItem;
    [SerializeField] private Transform scrollHolder;
    [SerializeField] private TextMeshProUGUI bigRewardProgressText;
    [SerializeField] private Slider bigRewardProgressBar;
    [SerializeField] private TextMeshProUGUI bigRewardTimeCountdown;
    [SerializeField] private Button bigRewardClaimButton;
    private Action<QuestProgress> onQuestFinish;
    // private Action<QuestProgress> onQuestCreate;
    
    private Action<bool> _onResult;
    private void Start()
    {
        onQuestFinish = (questProgress) =>
        {

            UserData.Instance.onQuestFinish?.Invoke(questProgress);
            UpdateBigReward();
            // dùng tạm --------------------------
            foreach (Transform item in scrollHolder)
            {
                Destroy(item.gameObject);
            }
            foreach (var item in UserData.Instance.listQuestInProgress)
            {
                var newQuestItem = Instantiate(questItem, scrollHolder);
                newQuestItem.Init(item, onQuestFinish);
            }
            // -----------------------------------
        };
        // onQuestCreate = UserData.Instance.onCreateNewQuest;
    }
    void InitUI()
    {
        UpdateBigReward();
        foreach (var item in UserData.Instance.listQuestInProgress)
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
        bigRewardProgressText.text = $"{UserData.Instance.questsFinished}/{UserData.Instance.bigRewardRequire}";
        bigRewardProgressBar.value = (float)UserData.Instance.questsFinished / UserData.Instance.bigRewardRequire;
        if(bigRewardProgressBar.value>=1){
            bigRewardClaimButton.enabled = true;
        }
        else    
        bigRewardClaimButton.enabled = false;
    }
    public void OnBigRewardClaim(){
        Debug.Log("Big reward claimed");
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
            foreach (Transform item in scrollHolder)
            {
                Destroy(item.gameObject);
            }
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
