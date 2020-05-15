using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Config;
using Game;
using Game.MessageCenter;
using UFrame;
using Game.GlobalData;
using System;
using UFrame.MessageCenter;
using System.Numerics;
using UFrame.Logger;
using UFrame.MiniGame;
using UFrame.BehaviourFloat;
using DG.Tweening;
using UFrame.EntityFloat;
using UnityEngine.EventSystems;

/// <summary>
/// 主界面
/// </summary>
public class UIQuitGamePage : UIPage
{

    public UIQuitGamePage() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None, UITickedMode.None)
    {
        uiPath = "UIPrefab/UIQuitGamePage";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        GetTransPrefabAllTextShow(this.transform);

        RegistBtnAndClick("UiBgMask/TipsGroup/ButtonGroup/YesButton", OnClickYes);
        RegistBtnAndClick("UiBgMask/TipsGroup/ButtonGroup/NoButton", OnClickNo);
        RegistBtnAndClick("UiBgMask/TipsGroup/AdvertTipsBg/CloseButton", OnClickClose);


        Text TitleText = RegistCompent<Text>("UiBgMask/TipsGroup/AdvertTipsBg/TitleText");
        Text ButtonText = RegistCompent<Text>("UiBgMask/TipsGroup/ButtonGroup/YesButton/ButtonText");
        Text ButtonText1 = RegistCompent<Text>("UiBgMask/TipsGroup/ButtonGroup/NoButton/ButtonText");

        //GetTransPrefabText(TitleText);
        //GetTransPrefabText(ButtonText);
        //GetTransPrefabText(ButtonText1);

    }
    public override void Active()
    {
        base.Active();
        
    }
    private void OnClickYes(string path)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        return;
#endif
        Application.Quit();
    }

    private void OnClickNo(string path)
    {
        PageMgr.ClosePage(this);
    }

    private void OnClickClose(string path)
    {
        PageMgr.ClosePage(this);
    }
    public override void Hide()
    {
        base.Hide();
    }
}
