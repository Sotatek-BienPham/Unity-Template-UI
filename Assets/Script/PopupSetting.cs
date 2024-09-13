using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
// using Hellmade.Sound;
using System.Linq;

public class PopupSetting : Popups
{
    public static PopupSetting Instance;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private List<Button> button;

   
    private float _musicVolume;
    private float _soundVolume;

    public float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            // EazySoundManager.GlobalMusicVolume = value;
            // EazySoundManager.GlobalSoundsVolume = value;
        }
    }

    public float SoundVolume
    {
        get { return _soundVolume; }
        set
        {
            _soundVolume = value;
            // EazySoundManager.GlobalMusicVolume = value;
            // EazySoundManager.GlobalSoundsVolume = value;
        }
    }

    private Action<bool> _onResult;
    void InitUI()
    {

        // volumeSlider.value = EazySoundManager.GlobalMusicVolume;
        // volumeSlider.onValueChanged.AddListener(OnChangeSliderMusic);
        // sfxSlider.value = EazySoundManager.GlobalSoundsVolume;
        // sfxSlider.onValueChanged.AddListener(OnChangeSfxVoloume);
    }

    


    #region BASE POPUP 
    static void CheckInstance(Action completed)//
    {
        if (Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupSetting>("Prefab/UI/PopupPrefabs/PopupSetting" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupSetting;
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
        base.Disappear(()=>{
            Panel.gameObject.SetActive(false);
        });
        sfxSlider.onValueChanged.RemoveAllListeners();
        volumeSlider.onValueChanged.RemoveAllListeners();
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
