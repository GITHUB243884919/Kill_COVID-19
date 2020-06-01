/*******************************************************************
* FileName:     GlobalDataManager.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-16
* Description:  
* other:    
********************************************************************/


using HillUFrame.Logger;
using System;
using System.Collections;
using System.Collections.Generic;
using HillUFrame;
using HillUFrame.Common;
using UnityEngine;

namespace Game.GlobalData
{
    public partial class GlobalDataManager : Singleton<GlobalDataManager>, ISingleton
    {
        bool isInit = false;

        public I18N i18n { get; protected set; }

        public PlayerData playerData = null;

        /// <summary>
        /// 玩家
        /// </summary>
        public Transform transPlayer = null;

        public Vector3 spawnLeftTop;

        public Vector3 spawnRightBottom;

        public void Init()
        {
            if (isInit)
            {
                return;
            }
            isInit = true;


            playerData = PlayerData.Load();
        }


        /// <summary>
        /// 不是所有都Release
        /// </summary>
        public void Release()
        {

        }





    }

}
