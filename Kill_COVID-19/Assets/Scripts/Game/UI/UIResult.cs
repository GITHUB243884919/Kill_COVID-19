using UnityEngine;
using DG.Tweening;

public class UIResult : UIPage
{
    public UIResult() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "UIPrefab/UIResult";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //初始化控件
        RegComponent();
        GetTransPrefabAllTextShow(this.transform);
        transform.Find("oper_grid/op_next").DOScale(1.2f, 0.2f).SetLoops(-1, LoopType.Yoyo);
    }

    protected void RegComponent()
    {
        RegistBtnAndClick("oper_grid/op_next", OnClickedNextLevel);
    }

    protected void OnClickedNextLevel(string objName)
    {
        PageMgr.ClosePage(this);
        var level = PageMgr.GetPage<UILevel2>();
        level.GenLevel(true);
    }

}

