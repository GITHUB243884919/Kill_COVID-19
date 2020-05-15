using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 运算操作
    /// </summary>
    public enum CalcOper
    {

        None,
        Add,
        Sub,
        Mul,
        Div,
    }

    public enum UIPOS
    {
        LeftDown,
        LeftUp,
        RightDown,
        RightUp,
        Bottom_1,
        Bottom_2,
        Bottom_3,
        Bottom_4,
    }

    public enum CalcUnitType
    {
        /// <summary>
        /// 牌
        /// </summary>
        Card,
        
        /// <summary>
        /// 操作符号
        /// </summary>
        Oper,
    }

    public class GameConst
    {
        public static int Default_LeftNumResetPoker = 10;
        public static int Default_LeftNumTips = 5;
    }

}
