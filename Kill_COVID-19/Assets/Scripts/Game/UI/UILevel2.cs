using _24PointGame;
using Game;
using Game.GlobalData;
using Game.MessageCenter;
using UFrame;
using UFrame.MessageCenter;
using UFrame.MiniGame;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UILevel2 : UIPage
{
    readonly string TopLeft_Poker_Path = "card_grid/poker_topleft";
    readonly string TopRight_Poker_Path = "card_grid/poker_topright";
    readonly string BottomLeft_Poker_Path = "card_grid/poker_bottomleft";
    readonly string BottomRight_Poker_Path = "card_grid/poker_bottomright";

    readonly string TopLeft_Result_Path = "result_grid/result_topleft";
    readonly string TopRight_Result_Path = "result_grid/result_topright";
    readonly string BottomLeft_Result_Path = "result_grid/result_bottomleft";
    readonly string BottomRight_Result_Path = "result_grid/result_bottomright";

    readonly string Oper_Add_Path = "oper_grid/op_add";
    readonly string Oper_Sub_Path = "oper_grid/op_sub";
    readonly string Oper_Mul_Path = "oper_grid/op_mul";
    readonly string Oper_Div_Path = "oper_grid/op_div";

    /// <summary>
    /// 数的范围的最小值
    /// </summary>
    readonly int Min_Num = 1;

    /// <summary>
    /// 数的范围的最大值
    /// </summary>
    readonly int Max_Num = 10;

    /// <summary>
    /// 正确的结果
    /// </summary>
    readonly int correctResult = 24;

    /// <summary>
    /// 剩余计算步骤数
    /// </summary>
    int leftCalcStepNum = 3;

    Image imgPokerTopLeft = null;
    Image imgPokerTopRight = null;
    Image imgPokerBottomLeft = null;
    Image imgPokerBottomRight = null;

    Outline olPokerTopLeft = null;
    Outline olPokerTopRight = null;
    Outline olPokerBottomLeft = null;
    Outline olPokerBottimRight = null;

    Outline olResultTopLeft = null;
    Outline olResultTopRight = null;
    Outline olResultBottomLeft = null;
    Outline olResultBottimRight = null;

    Button btnPokerTopLeft = null;
    Button btnPokerTopRight = null;
    Button btnPokerBottomLeft = null;
    Button btnPokerBottomRight = null;

    Button btnResultTopLeft = null;
    Button btnResultTopRight = null;
    Button btnResultBottomLeft = null;
    Button btnResultBottomRight = null;

    Text txtTips = null;
    Text txtLeftReset = null;
    Text txtLeftTips = null;
    Text txtLeftNext = null;

    string strTips = ""; 
	
    int topLeftNum = Const.Invalid_Int;
    int topRightNum = Const.Invalid_Int;
    int bottomLeftNum = Const.Invalid_Int;
    int bottomRightNum = Const.Invalid_Int;

    /// <summary>
    /// 运算中当做第一个数的poker牌或者结果牌
    /// </summary>
    Button firstNumCard = null;

    /// <summary>
    /// 运算中当成结果的牌
    /// </summary>
    Button resultCard = null;

    CardOperator cardOperator = new CardOperator();

    Text    txtFirst;
    Text    txtOp;
    Text    txtSecond;
    Text    txtResult;

    PlayerData playerData { get { return GlobalDataManager.GetInstance().playerData; } }

    /// <summary>
    /// 请求广告时候广告没加载
    /// </summary>
    bool requestADButUnload = false;

    public UILevel2() : base(UIType.Fixed, UIMode.DoNothing, UICollider.None, UITickedMode.Update)
    {
        uiPath = "UIPrefab/UILevel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);

        InitComponent();

        GenLevel(true);
        Debug.LogError(Time.realtimeSinceStartup);
        GlobalDataManager.GetInstance().cardModel.txtFirst = this.txtFirst;
        GlobalDataManager.GetInstance().cardModel.txtOp = this.txtOp;
        GlobalDataManager.GetInstance().cardModel.txtSecond = this.txtSecond;
        GlobalDataManager.GetInstance().cardModel.txtResult = this.txtResult;
        Debug.LogError(Time.realtimeSinceStartup);

        MessageManager.GetInstance().Regist((int)GameMessageDefine.GenLevel, OnGenLevel);

        MessageManager.GetInstance().Regist((int)GameMessageDefine.RewardADLoadSuccess, OnRewardADLoadSuccess);

        MessageManager.GetInstance().Regist((int)GameMessageDefine.RewardADLoadFail, OnRewardADLoadFail);
        Debug.LogError(Time.realtimeSinceStartup);

		transform.Find("tips").DOScale(1.2f, 0.2f).SetLoops(-1, LoopType.Yoyo);

	}

    public override void Tick(int deltaTimeMS)
    {
        //Debug.LogErrorFormat("AdmobManager {0}", AdmobManager.GetInstance().isLoaded);
        //if (AdmobManager.GetInstance().isLoaded)
        //{
        //    PageMgr.ClosePage<UIWaitAd>();
        //}

    }

    public void GenLevel(bool isNewLevel)
    {
        txtTips.text = "";

        txtFirst.text = "";
        txtOp.text = "";
        txtSecond.text = "";
        txtResult.text = "";

        GlobalDataManager.GetInstance().cardModel.leftCalcStepNum = 3;
        GlobalDataManager.GetInstance().cardModel.first = null;
        GlobalDataManager.GetInstance().cardModel.second = null;
        GlobalDataManager.GetInstance().cardModel.op = null;
        GlobalDataManager.GetInstance().cardModel.resultBtn = null;

        //所有的outline隐藏
        HideOutline();

        //结果牌隐藏
        HideResult();

        //显示扑克
        ShowPoker();

        //重置
        if (!isNewLevel)
        {
            ResetPoker();
            return;
        }

        //新生成
        topLeftNum = Const.Invalid_Int;
        topRightNum = Const.Invalid_Int;
        bottomLeftNum = Const.Invalid_Int;
        bottomRightNum = Const.Invalid_Int;
        bool retCode = false;
        do
        {
            retCode = GenPoker();
        }
        while (!retCode);
    }

    protected bool GenPoker()
    {
        topLeftNum = GenNum();
        topRightNum = GenNum();
        bottomLeftNum = GenNum();
        bottomRightNum = GenNum();
        GenCard(imgPokerTopLeft, topLeftNum);
        GenCard(imgPokerTopRight, topRightNum);
        GenCard(imgPokerBottomLeft, bottomLeftNum);
        GenCard(imgPokerBottomRight, bottomRightNum);

        cardOperator.InitCards(topLeftNum, topRightNum, bottomLeftNum, bottomRightNum);
        string result = cardOperator.Operate(correctResult);

        if (cardOperator.showResult.Count > 0)
        {
            Debug.LogError(cardOperator.showResult[0]);
            strTips = cardOperator.showResult[0];
            return true;
        }

        return false;
    }

    protected void ResetPoker()
    {
        ResetNum(imgPokerTopLeft, topLeftNum);
        ResetNum(imgPokerTopRight, topRightNum);
        ResetNum(imgPokerBottomLeft, bottomLeftNum);
        ResetNum(imgPokerBottomRight, bottomRightNum);
    }

    protected void ResetNum(Image img, int num)
    {
        string spPath = null;
        spPath = string.Format("UIAtlas/cards/style_01/{0}", num.ToString().PadLeft(4, '0'));
        img.sprite = ResourceManager.LoadSpriteFromPrefab(spPath);
    }

    protected int GenNum()
    {
        return Random.Range(Min_Num, Max_Num + 1);
    }

    protected void GenCard(Image img, int num)
    {
        string spPath = string.Format("UIAtlas/cards/style_01/{0}", num.ToString().PadLeft(4, '0'));
        img.sprite = ResourceManager.LoadSpriteFromPrefab(spPath);
    }

    protected void InitComponent()
    {
        imgPokerTopLeft = RegistCompent<Image>(TopLeft_Poker_Path);
        imgPokerTopRight = RegistCompent<Image>(TopRight_Poker_Path);
        imgPokerBottomLeft = RegistCompent<Image>(BottomLeft_Poker_Path);
        imgPokerBottomRight = RegistCompent<Image>(BottomRight_Poker_Path);

        olPokerTopLeft = RegistCompent<Outline>(TopLeft_Poker_Path);
        olPokerTopRight = RegistCompent<Outline>(TopRight_Poker_Path);
        olPokerBottomLeft = RegistCompent<Outline>(BottomLeft_Poker_Path);
        olPokerBottimRight = RegistCompent<Outline>(BottomRight_Poker_Path);

        olResultTopLeft = RegistCompent<Outline>(TopLeft_Result_Path);
        olResultTopRight = RegistCompent<Outline>(TopRight_Result_Path);
        olResultBottomLeft = RegistCompent<Outline>(BottomLeft_Result_Path);
        olResultBottimRight = RegistCompent<Outline>(BottomRight_Result_Path);

        btnPokerTopLeft = RegistCompent<Button>(TopLeft_Poker_Path);
        btnPokerTopRight = RegistCompent<Button>(TopRight_Poker_Path);
        btnPokerBottomLeft = RegistCompent<Button>(BottomLeft_Poker_Path);
        btnPokerBottomRight = RegistCompent<Button>(BottomRight_Poker_Path);

        btnResultTopLeft = RegistCompent<Button>(TopLeft_Result_Path);
        btnResultTopRight = RegistCompent<Button>(TopRight_Result_Path);
        btnResultBottomLeft = RegistCompent<Button>(BottomLeft_Result_Path);
        btnResultBottomRight = RegistCompent<Button>(BottomRight_Result_Path);

        RegistBtnAndClick(TopLeft_Poker_Path, OnClickedNum);
        RegistBtnAndClick(TopRight_Poker_Path, OnClickedNum);
        RegistBtnAndClick(BottomLeft_Poker_Path, OnClickedNum);
        RegistBtnAndClick(BottomRight_Poker_Path, OnClickedNum);

        RegistBtnAndClick(Oper_Add_Path, OnClickedOp);
        RegistBtnAndClick(Oper_Sub_Path, OnClickedOp);
        RegistBtnAndClick(Oper_Mul_Path, OnClickedOp);
        RegistBtnAndClick(Oper_Div_Path, OnClickedOp);

        RegistBtnAndClick(TopLeft_Result_Path, OnClickedNum);
        RegistBtnAndClick(TopRight_Result_Path, OnClickedNum);
        RegistBtnAndClick(BottomLeft_Result_Path, OnClickedNum);
        RegistBtnAndClick(BottomRight_Result_Path, OnClickedNum);

        txtTips = RegistCompent<Text>("tips");
        RegistBtnAndClick("head/restart", OnClickRestart);
        RegistBtnAndClick("head/tips", OnClickTips);
        RegistBtnAndClick("head/next", OnClickNext);

        txtLeftReset = RegistCompent<Text>("head/restart/text");
        txtLeftReset.text = playerData.leftNumResetPoker.ToString();

        txtLeftTips = RegistCompent<Text>("head/tips/text");
        txtLeftTips.text = playerData.leftNumTips.ToString();

        txtLeftNext = RegistCompent<Text>("head/next/text");
        txtLeftNext.text = playerData.leftNumNextPoker.ToString();

        txtFirst = RegistCompent<Text>("Txt_first");
        txtOp = RegistCompent<Text>("Txt_op");
        txtSecond = RegistCompent<Text>("Txt_second");
        txtResult = RegistCompent<Text>("Txt_result");

		RegistBtnAndClick("head/btn_IAP", OnClickedIAP);
		RegistBtnAndClick("head/btn_AD", OnClickedAD);
	}

    protected void HideOutline()
    {
        olPokerTopLeft.enabled = false;
        olPokerTopRight.enabled = false;
        olPokerBottomLeft.enabled = false;
        olPokerBottimRight.enabled = false;

        olResultTopLeft.enabled = false;
        olResultTopRight.enabled = false;
        olResultBottomLeft.enabled = false;
        olResultBottimRight.enabled = false;

		GameObject.Find(Oper_Add_Path).GetComponent<Outline>().enabled = false;
		GameObject.Find(Oper_Sub_Path).GetComponent<Outline>().enabled = false;
		GameObject.Find(Oper_Mul_Path).GetComponent<Outline>().enabled = false;
		GameObject.Find(Oper_Div_Path).GetComponent<Outline>().enabled = false;
	}

    protected void HideResult()
    {
        btnResultTopLeft.gameObject.SetActive(false);
        btnResultTopRight.gameObject.SetActive(false);
        btnResultBottomLeft.gameObject.SetActive(false);
        btnResultBottomRight.gameObject.SetActive(false);
    }

    protected void ShowPoker()
    {
        btnPokerTopLeft.gameObject.SetActive(true);
        btnPokerTopRight.gameObject.SetActive(true);
        btnPokerBottomLeft.gameObject.SetActive(true);
        btnPokerBottomRight.gameObject.SetActive(true);
		transform.Find("guide").gameObject.SetActive(true);
    }

    #region 数字点击
    protected void OnClickedNum(Button obj)
    {
        obj.gameObject.GetComponent<Outline>().enabled = true;
        CalcUnit calcUnit = new CalcUnit();
        calcUnit.unitType = CalcUnitType.Card;
        calcUnit.unitData = new UnitData();
        calcUnit.unitData.btn = obj;
        switch (obj.name)
        {
            //poker
            case "poker_topleft":
                UnitData.Data(calcUnit.unitData).pointUp = topLeftNum;
                UnitData.Data(calcUnit.unitData).resultBtn = btnResultTopLeft;
                GlobalDataManager.GetInstance().cardModel.AddCalcUnit(calcUnit);
                break;
            case "poker_topright":
                UnitData.Data(calcUnit.unitData).pointUp = topRightNum;
                UnitData.Data(calcUnit.unitData).resultBtn = btnResultTopRight;
                GlobalDataManager.GetInstance().cardModel.AddCalcUnit(calcUnit);
                break;
            case "poker_bottomleft":
                UnitData.Data(calcUnit.unitData).pointUp = bottomLeftNum;
                UnitData.Data(calcUnit.unitData).resultBtn = btnResultBottomLeft;
                GlobalDataManager.GetInstance().cardModel.AddCalcUnit(calcUnit);
                break;
            case "poker_bottomright":
                UnitData.Data(calcUnit.unitData).pointUp = bottomRightNum;
                UnitData.Data(calcUnit.unitData).resultBtn = btnResultBottomRight;
                GlobalDataManager.GetInstance().cardModel.AddCalcUnit(calcUnit);
                break;
            //result
            case "result_topleft":
                if (int.TryParse(obj.GetComponentInChildren<Text>().text, out UnitData.Data(calcUnit.unitData).pointUp))
                {
                    UnitData.Data(calcUnit.unitData).pointDown = 1;
                }
                else
                {
                    string[] s = obj.GetComponentInChildren<Text>().text.Split('/');
                    UnitData.Data(calcUnit.unitData).pointUp = int.Parse(s[0]);
                    UnitData.Data(calcUnit.unitData).pointDown = int.Parse(s[1]);
                }
                UnitData.Data(calcUnit.unitData).resultBtn = btnResultTopLeft;
                GlobalDataManager.GetInstance().cardModel.AddCalcUnit(calcUnit);
                break;
            case "result_topright":
                if (int.TryParse(obj.GetComponentInChildren<Text>().text, out UnitData.Data(calcUnit.unitData).pointUp))
                {
                    UnitData.Data(calcUnit.unitData).pointDown = 1;
                }
                else
                {
                    string[] s = obj.GetComponentInChildren<Text>().text.Split('/');
                    UnitData.Data(calcUnit.unitData).pointUp = int.Parse(s[0]);
                    UnitData.Data(calcUnit.unitData).pointDown = int.Parse(s[1]);
                }
                UnitData.Data(calcUnit.unitData).resultBtn = btnResultTopRight;
                GlobalDataManager.GetInstance().cardModel.AddCalcUnit(calcUnit);

                break;
            case "result_bottomleft":
                if (int.TryParse(obj.GetComponentInChildren<Text>().text, out UnitData.Data(calcUnit.unitData).pointUp))
                {
                    UnitData.Data(calcUnit.unitData).pointDown = 1;
                }
                else
                {
                    string[] s = obj.GetComponentInChildren<Text>().text.Split('/');
                    UnitData.Data(calcUnit.unitData).pointUp = int.Parse(s[0]);
                    UnitData.Data(calcUnit.unitData).pointDown = int.Parse(s[1]);
                }
                UnitData.Data(calcUnit.unitData).resultBtn = btnResultBottomLeft;
                GlobalDataManager.GetInstance().cardModel.AddCalcUnit(calcUnit);
                break;
            case "result_bottomright":
                if (int.TryParse(obj.GetComponentInChildren<Text>().text, out UnitData.Data(calcUnit.unitData).pointUp))
                {
                    UnitData.Data(calcUnit.unitData).pointDown = 1;
                }
                else
                {
                    string[] s = obj.GetComponentInChildren<Text>().text.Split('/');
                    UnitData.Data(calcUnit.unitData).pointUp = int.Parse(s[0]);
                    UnitData.Data(calcUnit.unitData).pointDown = int.Parse(s[1]);
                }
                UnitData.Data(calcUnit.unitData).resultBtn = btnResultBottomRight;
                GlobalDataManager.GetInstance().cardModel.AddCalcUnit(calcUnit);
                break;
        }
    }
    #endregion

    #region 运算符+, -, *, / 点击
    protected void OnClickedOp(Button obj)
    {
        CalcUnit calcUnit = new CalcUnit();
        calcUnit.unitType = CalcUnitType.Oper;
        calcUnit.unitData = new OperData();
        calcUnit.unitData.btn = obj;
        obj.gameObject.GetComponent<Outline>().enabled = true;
        switch (obj.name)
        {
            case "op_add":
                OperData.Data(calcUnit.unitData).op = CalcOper.Add;
                break;
            case "op_sub":
                OperData.Data(calcUnit.unitData).op = CalcOper.Sub;
                break;
            case "op_mul":
                OperData.Data(calcUnit.unitData).op = CalcOper.Mul;
                break;
            case "op_div":
                OperData.Data(calcUnit.unitData).op = CalcOper.Div;
                break;
        }

        GlobalDataManager.GetInstance().cardModel.AddCalcUnit(calcUnit);
    }
	#endregion

	protected void OnClickedIAP(Button obj)
	{
		UFrameIAP.GetInstance().BuyConsumable();
	}

	protected void OnClickedAD(Button obj)
	{
		if (AdmobManager.GetInstance().isLoaded) {
			requestADButUnload = false;
			AdmobManager.GetInstance().UserChoseToWatchAd(OnTipsWatchRewardAdSuccessed);
		} else {
			requestADButUnload = true;
			PageMgr.ShowPage<UIWaitAd>(5000);
		}
	}

	protected void OnClickRestart(Button obj)
    {
        if (playerData.leftNumResetPoker > 0)
        {
            --playerData.leftNumResetPoker;
            txtLeftReset.text = playerData.leftNumResetPoker.ToString();
            GenLevel(false);
        }
    }

    protected void OnClickNext(Button obj)
    {
        if (playerData.leftNumNextPoker > 0)
        {
            --playerData.leftNumNextPoker;
            txtLeftNext.text = playerData.leftNumNextPoker.ToString();
            GenLevel(true);
        }
    }

    protected void OnClickTips(Button obj)
    {
//#if UNITY_EDITOR
//        OnTipsWatchRewardAdSuccessed();
//        return;
//#endif
        if (playerData.leftNumTips > 0)
        {
            --playerData.leftNumTips;
            txtLeftTips.text = playerData.leftNumTips.ToString();
            OnTipsWatchRewardAdSuccessed();
        }

        return;
        if (AdmobManager.GetInstance().isLoaded)
        {
            requestADButUnload = false;
            AdmobManager.GetInstance().UserChoseToWatchAd(OnTipsWatchRewardAdSuccessed);
        }
        else
        {
            requestADButUnload = true;
            PageMgr.ShowPage<UIWaitAd>(5000);
        }
    }

    void OnTipsWatchRewardAdSuccessed()
    {
        //++playerData.leftNumTips;
        txtLeftTips.text = playerData.leftNumTips.ToString();
        txtTips.text = strTips;
		transform.Find("guide").gameObject.SetActive(false);
	}
    
    void OnRewardADLoadSuccess(Message msg)
    {
        PageMgr.ClosePage<UIWaitAd>();
        if (requestADButUnload)
        {
            requestADButUnload = false;
            AdmobManager.GetInstance().UserChoseToWatchAd(OnTipsWatchRewardAdSuccessed);
        }
    }

    void OnRewardADLoadFail(Message msg)
    {
        PageMgr.ClosePage<UIWaitAd>();
        if (requestADButUnload)
        {
            requestADButUnload = false;
            PromptText.CreatePromptText(false, "Load AD Fail");
        }
    }

    void OnGenLevel(Message msg)
    {
        GenLevel(false);
    }



}
