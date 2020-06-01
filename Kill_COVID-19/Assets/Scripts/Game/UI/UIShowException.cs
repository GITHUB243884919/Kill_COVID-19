using Game;
using Game.GlobalData;
using Game.MessageCenter;
using HillUFrame.Logger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using HillUFrame;
using HillUFrame.MessageCenter;
using UnityEngine;
using UnityEngine.UI;
using HillUFrame.MiniGame;

public class UIShowException : UIPage
{
    Text txt_ExceptionInfo;
    public UIShowException() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "uiprefab/UIShowException";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //初始化控件
        this.RegistAllCompent();
        GetTransPrefabAllTextShow(this.transform);

        txt_ExceptionInfo.text = (string)this.m_data;
    }

    private void RegistAllCompent()
    {
        txt_ExceptionInfo = RegistCompent<Text>("ExceptionInfo");
    }

}

