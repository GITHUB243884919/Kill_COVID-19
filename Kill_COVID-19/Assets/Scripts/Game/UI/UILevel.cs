using _24PointGame;
using Game;
using Game.GlobalData;
using Game.MessageCenter;
using UFrame;
using UFrame.MessageCenter;
using UFrame.MiniGame;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : UIPage
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

    /// <summary>
    /// 运算中第一个数
    /// </summary>
    int firstNum = Const.Invalid_Int;

    /// <summary>
    /// 运算符
    /// </summary>
    CalcOper calcOper = CalcOper.None;

    /// <summary>
    /// 运算中第二个数
    /// </summary>
    int SecondNum = Const.Invalid_Int;

    /// <summary>
    /// 是否选定第一个数
    /// </summary>
    bool isSelectedFirstNum = false;

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

    PlayerData playerData { get { return GlobalDataManager.GetInstance().playerData; } }

    /// <summary>
    /// 请求广告时候广告没加载
    /// </summary>
    bool requestADButUnload = false;

    public UILevel() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None, UITickedMode.Update)
    {
        uiPath = "UIPrefab/UILevel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);

        InitComponent();

        GenLevel(true);

        MessageManager.GetInstance().Regist((int)GameMessageDefine.RewardADLoadSuccess, OnRewardADLoadSuccess);

        MessageManager.GetInstance().Regist((int)GameMessageDefine.RewardADLoadFail, OnRewardADLoadFail);
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
        firstNum = Const.Invalid_Int;
        calcOper = CalcOper.None;
        SecondNum = Const.Invalid_Int;
        isSelectedFirstNum = false;
        leftCalcStepNum = 3;
        txtTips.text = "";

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
    }

    protected bool CheckFinish()
    {
        return true;
    }

    #region 数字点击
    protected void OnClickedNum(Button obj)
    {
        //一次运算中不能点同一位置的card
        if (firstNumCard == obj)
        {
            return;
        }

        obj.gameObject.GetComponent<Outline>().enabled = true;
        int operNum = Const.Invalid_Int;
        switch(obj.name)
        {
            //poker
            case "poker_topleft":
                operNum = topLeftNum;
                resultCard = btnResultTopLeft;
                break;
            case "poker_topright":
                operNum = topRightNum;
                resultCard = btnResultTopRight;
                break;
            case "poker_bottomleft":
                operNum = bottomLeftNum;
                resultCard = btnResultBottomLeft;
                break;
            case "poker_bottomright":
                operNum = bottomRightNum;
                resultCard = btnResultBottomRight;
                break;
            //result
            case "result_topleft":
                operNum = int.Parse(obj.GetComponentInChildren<Text>().text);
                resultCard = btnResultTopLeft;
                break;
            case "result_topright":
                operNum = int.Parse(obj.GetComponentInChildren<Text>().text);
                resultCard = btnResultTopRight;
                break;
            case "result_bottomleft":
                operNum = int.Parse(obj.GetComponentInChildren<Text>().text);
                resultCard = btnResultBottomLeft;
                break;
            case "result_bottomright":
                operNum = int.Parse(obj.GetComponentInChildren<Text>().text);
                resultCard = btnResultBottomRight;
                break;
        }

        bool shouldResult = false;
        switch (calcOper)
        {
            case CalcOper.None:
                firstNum = operNum;
                firstNumCard = obj;
                isSelectedFirstNum = true;
                break;
            case CalcOper.Add:
                firstNum += operNum;
                shouldResult = true;
                break;
            case CalcOper.Sub:
                firstNum -= operNum;
                shouldResult = true;
                break;
            case CalcOper.Mul:
                firstNum *= operNum;
                shouldResult = true;
                break;
            case CalcOper.Div:
                if (operNum == 0)
                {
                    //除0处理
                    calcOper = CalcOper.None;
                    firstNumCard.gameObject.GetComponent<Outline>().enabled = false;
                    obj.gameObject.GetComponent<Outline>().enabled = false;
                    return;
                }
                firstNum /= operNum;
                shouldResult = true;
                break;
        }

        if (shouldResult)
        {
            calcOper = CalcOper.None;
            isSelectedFirstNum = false;
            firstNumCard.gameObject.SetActive(false);
            obj.gameObject.SetActive(false);
            resultCard.gameObject.SetActive(true);
            resultCard.GetComponentInChildren<Text>().text = firstNum.ToString();
            --leftCalcStepNum;
            if (leftCalcStepNum == 0)
            {
                if (firstNum == correctResult)
                {
                    PageMgr.ShowPage<UIResult>();
                    return;
                }

                GenLevel(false);
            }
        }

    }
    #endregion

    #region 运算符+, -, *, / 点击
    protected void OnClickedOp(Button obj)
    {
        if (!isSelectedFirstNum)
        {
            return;
        }

        switch (obj.name)
        {
            case "op_add":
                calcOper = CalcOper.Add;
                break;
            case "op_sub":
                calcOper = CalcOper.Sub;
                break;
            case "op_mul":
                calcOper = CalcOper.Mul;
                break;
            case "op_div":
                calcOper = CalcOper.Div;
                break;
        }
    }
    #endregion

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
        ++playerData.leftNumTips;
        txtLeftTips.text = playerData.leftNumTips.ToString();
        txtTips.text = strTips;
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



}
