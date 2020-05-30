using Game;
using Game.GlobalData;
using Game.MessageCenter;
using UFrame.Logger;
using System;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.MessageCenter;
using UnityEngine;
using UnityEngine.UI;
using UFrame.MiniGame;
using UnityEngine.EventSystems;

public class UIStage : UIPage
{
    Player player;
    public UIStage() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "UIPrefab/UIStage";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);

        //初始化控件
        this.RegistAllCompent();
        GetTransPrefabAllTextShow(this.transform);
    }

    private void RegistAllCompent()
    {
        RegistBtnAndClick("oper_grid/op_add", OnBtnSkill_1);
        RegistBtnAndClick("oper_grid/op_sub", OnBtnSkill_2);
    }

    void OnBtnSkill_1(Button btn)
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponentInChildren<Player>();
        }
        player.Skill_1();
    }

    void OnBtnSkill_2(Button btn)
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponentInChildren<Player>();
        }
        player.Skill_2();
    }

}

