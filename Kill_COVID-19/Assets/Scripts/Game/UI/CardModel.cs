using Game.MessageCenter;
using System;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public interface ICalcUnitData
    {
        Button btn { get; set; }
    }

    public class UnitData : ICalcUnitData
    {
        /// <summary>
        /// 分母
        /// </summary>
        public int pointUp = 0;

        /// <summary>
        /// 分子
        /// </summary>
        public int pointDown = 1;

        
        /// <summary>
        /// 结果牌
        /// </summary>
        public Button resultBtn;

        public Button btn { get; set; }

        public static UnitData Data(ICalcUnitData i)
        {
            return i as UnitData;
        }

    }

    public class OperData : ICalcUnitData
    {
        public CalcOper op;

        public Button btn { get; set; }

        public static OperData Data(ICalcUnitData i)
        {
            return i as OperData;
        }
    }

    public class CalcUnit
    {
        public CalcUnitType unitType;
        public ICalcUnitData unitData;
    }

    public class CardModel
    {
        public int leftCalcStepNum = 3;
        public CalcUnit first;
        public CalcUnit second;
        public CalcUnit op;

        public Button resultBtn;
        public Text txtFirst;
        public Text txtOp;
        public Text txtSecond;
        public Text txtResult;

        int correctResult = 24;

        public void AddCalcUnit(CalcUnit calcUnit)
        { 
            RecordCalcUnit(calcUnit);
            Calc();
        }
        
        bool CouldCalc { get { return first != null && second != null && op!= null; } }

        void RecordCalcUnit(CalcUnit calcUnit)
        {
            switch(calcUnit.unitType)
            {
                case CalcUnitType.Card:
                    if (first == null)
                    {
                        if (second != null && second.unitData.btn == calcUnit.unitData.btn)
                        {
                            txtFirst.text = "";

                            second.unitData.btn.GetComponent<Outline>().enabled = false;
                            second = null;
                            txtSecond.text = "";

                            this.resultBtn = null;
                            return;
                        }

                        first = calcUnit;
                        txtFirst.text = UnitData.Data(first.unitData).pointUp.ToString();
                        this.resultBtn = UnitData.Data(first.unitData).resultBtn;
                    }
                    else if (second == null)
                    {
                        if (first.unitData.btn == calcUnit.unitData.btn)
                        {
                            first.unitData.btn.GetComponent<Outline>().enabled = false;
                            first = null;
                            txtFirst.text = "";

                            this.resultBtn = null;
                            return;
                        }
                        second = calcUnit;
                        txtSecond.text = UnitData.Data(second.unitData).pointUp.ToString();
                    }
                    else
                    {
                        //重复点击
                        if (first.unitData.btn == calcUnit.unitData.btn)
                        {
                            first.unitData.btn.GetComponent<Outline>().enabled = false;
                            first = null;
                            txtFirst.text = "";

                            this.resultBtn = null;
                        }
                        else if (second.unitData.btn == calcUnit.unitData.btn)
                        {
                            second.unitData.btn.GetComponent<Outline>().enabled = false;
                            second = null;
                            txtSecond.text = "";
                        }
                        else
                        {
                            //第一个被顶走，换成第二个，第二个填上新的
                            first.unitData.btn.GetComponent<Outline>().enabled = false;
                            first = second;
                            txtFirst.text = UnitData.Data(first.unitData).pointUp.ToString();
                            this.resultBtn = UnitData.Data(first.unitData).resultBtn;
                            second = calcUnit;
                            txtSecond.text = UnitData.Data(second.unitData).pointUp.ToString();
                        }
                    }
                    
                    break;
                case CalcUnitType.Oper:
                    if (op != null)
                    {
                        op.unitData.btn.GetComponent<Outline>().enabled = false;
                    }
                    this.op = calcUnit;
                    op.unitData.btn.GetComponent<Outline>().enabled = true;
                    break;
                default:
                    throw new System.Exception(string.Format("错误的UnitType {0}", calcUnit.unitType));
            }
        }

        void Calc()
        {
            if (!CouldCalc)
            {
                return;
            }
            
            int n1 = UnitData.Data(first.unitData).pointUp;
            int n2 = UnitData.Data(second.unitData).pointUp;
            Debug.LogErrorFormat("{0} {1} {2} ", n1, OperData.Data(op.unitData).op, n2);
            CalcUnit calcUnit = new CalcUnit();
            calcUnit.unitType = CalcUnitType.Card;
            calcUnit.unitData = new UnitData();
            calcUnit.unitData.btn = resultBtn;
            UnitData.Data(calcUnit.unitData).resultBtn = resultBtn;

            switch (OperData.Data(op.unitData).op)
            {
                case CalcOper.Add:
                    Op_Add(calcUnit);
                    break;
                case CalcOper.Sub:
                    Op_Sub(calcUnit);
                    break;
                case CalcOper.Mul:
                    Op_Mul(calcUnit);
                    break;
                case CalcOper.Div:
                    Op_Div(calcUnit);
                    break;
            }

            UnitData.Data(first.unitData).btn.gameObject.SetActive(false);
            UnitData.Data(second.unitData).btn.gameObject.SetActive(false);
            resultBtn.gameObject.SetActive(true);
			resultBtn.GetComponent<Outline>().enabled = false;
			if (UnitData.Data(calcUnit.unitData).pointDown != 1)
            {
                resultBtn.GetComponentInChildren<Text>().text = UnitData.Data(calcUnit.unitData).pointUp + "/" + UnitData.Data(calcUnit.unitData).pointDown;
                txtResult.text = UnitData.Data(calcUnit.unitData).pointUp + "/" + UnitData.Data(calcUnit.unitData).pointDown;
            }
            else
            {
                resultBtn.GetComponentInChildren<Text>().text = UnitData.Data(calcUnit.unitData).pointUp.ToString();
                txtResult.text = UnitData.Data(calcUnit.unitData).pointUp.ToString();
            }

            op.unitData.btn.GetComponent<Outline>().enabled = false;
            first = null;
            resultBtn = null;
            second = null;
            op = null;
            --leftCalcStepNum;
            txtFirst.text = "";
            txtSecond.text = "";
            txtResult.text = "";

            if (leftCalcStepNum == 0)
            {

                leftCalcStepNum = 3;
                if (UnitData.Data(calcUnit.unitData).pointDown == 1 &&
                    UnitData.Data(calcUnit.unitData).pointUp == this.correctResult)
                {
                    
                    PageMgr.ShowPage<UIResult>();
                    return;
                }
                
                MessageManager.GetInstance().Send((int)GameMessageDefine.GenLevel);
            }
        }

        void Op_Add(CalcUnit calcUnit)
        {
            int n1 = UnitData.Data(first.unitData).pointUp;
            int n2 = UnitData.Data(second.unitData).pointUp;
            int r = 0;

            if (UnitData.Data(first.unitData).pointDown != 1 ||
                UnitData.Data(second.unitData).pointDown != 1)
            {
                UnitData.Data(calcUnit.unitData).pointUp =
                    UnitData.Data(first.unitData).pointUp * UnitData.Data(second.unitData).pointDown + 
                    UnitData.Data(first.unitData).pointDown * UnitData.Data(second.unitData).pointUp;

                UnitData.Data(calcUnit.unitData).pointDown =
                    UnitData.Data(first.unitData).pointDown * UnitData.Data(second.unitData).pointDown;

                if (UnitData.Data(calcUnit.unitData).pointUp % UnitData.Data(calcUnit.unitData).pointDown == 0)
                {
                    UnitData.Data(calcUnit.unitData).pointUp = UnitData.Data(calcUnit.unitData).pointUp / UnitData.Data(calcUnit.unitData).pointDown;
                    UnitData.Data(calcUnit.unitData).pointDown = 1;
                }
            }
            else
            {
                r = n1 + n2;
                UnitData.Data(calcUnit.unitData).pointUp = r;
                UnitData.Data(calcUnit.unitData).pointDown = 1;
            }
        }

        void Op_Sub(CalcUnit calcUnit)
        {
            int n1 = UnitData.Data(first.unitData).pointUp;
            int n2 = UnitData.Data(second.unitData).pointUp;
            int r = 0;

            if (UnitData.Data(first.unitData).pointDown != 1 ||
                UnitData.Data(second.unitData).pointDown != 1)
            {
                UnitData.Data(calcUnit.unitData).pointUp =
                    UnitData.Data(first.unitData).pointUp * UnitData.Data(second.unitData).pointDown -
                    UnitData.Data(first.unitData).pointDown * UnitData.Data(second.unitData).pointUp;

                UnitData.Data(calcUnit.unitData).pointDown =
                    UnitData.Data(first.unitData).pointDown * UnitData.Data(second.unitData).pointDown;

                if (UnitData.Data(calcUnit.unitData).pointUp % UnitData.Data(calcUnit.unitData).pointDown == 0)
                {
                    UnitData.Data(calcUnit.unitData).pointUp = UnitData.Data(calcUnit.unitData).pointUp / UnitData.Data(calcUnit.unitData).pointDown;
                    UnitData.Data(calcUnit.unitData).pointDown = 1;
                }
            }
            else
            {
                r = n1 - n2;
                UnitData.Data(calcUnit.unitData).pointUp = r;
                UnitData.Data(calcUnit.unitData).pointDown = 1;
            }
        }

        void Op_Mul(CalcUnit calcUnit)
        {
            int n1 = UnitData.Data(first.unitData).pointUp;
            int n2 = UnitData.Data(second.unitData).pointUp;
            int r = 0;

            if (UnitData.Data(first.unitData).pointDown != 1 ||
                UnitData.Data(second.unitData).pointDown != 1)
            {
                UnitData.Data(calcUnit.unitData).pointUp =
                    UnitData.Data(first.unitData).pointUp * UnitData.Data(second.unitData).pointUp;

                UnitData.Data(calcUnit.unitData).pointDown =
                    UnitData.Data(first.unitData).pointDown * UnitData.Data(second.unitData).pointDown;

                if (UnitData.Data(calcUnit.unitData).pointUp % UnitData.Data(calcUnit.unitData).pointDown == 0)
                {
                    UnitData.Data(calcUnit.unitData).pointUp = UnitData.Data(calcUnit.unitData).pointUp / UnitData.Data(calcUnit.unitData).pointDown;
                    UnitData.Data(calcUnit.unitData).pointDown = 1;
                }
            }
            else
            {
                r = n1 * n2;
                UnitData.Data(calcUnit.unitData).pointUp = r;
                UnitData.Data(calcUnit.unitData).pointDown = 1;
            }
        }

        void Op_Div(CalcUnit calcUnit)
        {
            int n1 = UnitData.Data(first.unitData).pointUp;
            int n2 = UnitData.Data(second.unitData).pointUp;
            int r = 0;
            if (n2 == 0)
            {
                //除0 结果直接=0
                r = 0;
                UnitData.Data(calcUnit.unitData).pointUp = r;
                UnitData.Data(calcUnit.unitData).pointDown = 1;
            }
            else if (n1 % n2 != 0 ||
                UnitData.Data(first.unitData).pointDown != 1 ||
                UnitData.Data(second.unitData).pointDown != 1)
            {
                //有余数 或 分数
                UnitData.Data(calcUnit.unitData).pointUp =
                    UnitData.Data(first.unitData).pointUp * UnitData.Data(second.unitData).pointDown;

                UnitData.Data(calcUnit.unitData).pointDown =
                    UnitData.Data(first.unitData).pointDown * UnitData.Data(second.unitData).pointUp;
                
                if (UnitData.Data(calcUnit.unitData).pointUp % UnitData.Data(calcUnit.unitData).pointDown == 0)
                {
                    UnitData.Data(calcUnit.unitData).pointUp = UnitData.Data(calcUnit.unitData).pointUp / UnitData.Data(calcUnit.unitData).pointDown;
                    UnitData.Data(calcUnit.unitData).pointDown = 1;
                }
            }
            else
            {
                r = n1 / n2;
                UnitData.Data(calcUnit.unitData).pointUp = r;
                UnitData.Data(calcUnit.unitData).pointDown = 1;
            }
        }
    }
}

