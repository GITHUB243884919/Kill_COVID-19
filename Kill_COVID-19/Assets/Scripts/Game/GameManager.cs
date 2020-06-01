/*******************************************************************
* FileName:     GameManager.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-8
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HillUFrame.Common;
using HillUFrame;
using Game.GlobalData;
using Game.MessageCenter;
using HillUFrame.MessageCenter;
using HillUFrame.Logger;
using System;

namespace Game
{
    public class GameManagerTick : TickBase
    {
        public Dictionary<string, UIPage> tickedPages = new Dictionary<string, UIPage>();
        public override void Tick(int deltaTimeMS)
        {
            //MessageManager不受暂停和停止限制
            MessageManager.GetInstance().Tick();

            if (GameRoot.GetInstance().fsmGameLoad != null)
            {
                GameRoot.GetInstance().fsmGameLoad.Tick(deltaTimeMS);
            }

            if (!this.CouldRun())
            {
                return;
            }

            GameManager.GetInstance().gameModules.Tick(deltaTimeMS);

            foreach(var val in tickedPages.Values)
            {
                if (val != null)
                {
                    val.Tick(deltaTimeMS);
                }
            }
        }
    }

    public class GameManager : SingletonMono<GameManager>
    {
        GameManagerTick tickObj;

        static int moduleOrderID = 0;

        public int tickCount;

        public GameModules gameModules = new GameModules();

        public override void Awake()
        {
            base.Awake();
            Init();
            this.Run();
        }

        public void Start()
        {
            var pd = GlobalDataManager.GetInstance().playerData;
            if (pd.isFirstInstall)
            {
                //ThirdPartTA.Identify();
                //ThirdPartTA.StartTrack();
                //ThirdPartTA.Track(TAEventsMonitorEnum.register);
            }
            else
            {
                //ThirdPartTA.StartTrack();
            }

            //ThirdPartTA.Track(TAEventsMonitorEnum.gamestart);

            //ThirdPartTA.TrackAppInstall();
            //每次登录写last_login_time
            //LogWarp.Log("ThirdPartTA.UserSet.last_login_time");
            //var taParam = new Dictionary<string, object>();
            //taParam.Add("last_login_time", DateTime.Now);
            //ThirdPartTA.UserSet(taParam);

            LogWarp.Log("GameRoot.isRunning = true");
            GameRoot.GetInstance().isRunning = true;
        }

        public void Update()
        {
            SetAndroidQuit();
            this.Tick(Math_F.FloatToInt1000(Time.deltaTime));
        }

        public void Init()
        {
            MessageManager.GetInstance().SetCallbackNotFoundMessage(this.OnNotFoundMessage);
            MessageManager.GetInstance().Regist((int)GameMessageDefine.LoadSceneFinished, OnLoadSceneFinished);
            MessageManager.GetInstance().Regist((int)GameMessageDefine.UIMessage_AddToTick, OnUIMessage_AddToTick);
            MessageManager.GetInstance().Regist((int)GameMessageDefine.UIMessage_RemoveFromTick, OnUIMessage_RemoveFromTick);

            InitGlobaData();
            //InitModule();
            PageMgr.SetButtonSound(Config.globalConfig.getInstace().UiButtonSoynd);
            this.tickObj = new GameManagerTick();
#if UNITY_EDITOR
            tickCount = 0;
#endif
        }

        public void Release()
        {
#if UNITY_EDITOR
            tickCount = 0;
#endif
            MessageManager.GetInstance().Regist((int)GameMessageDefine.LoadSceneFinished, OnLoadSceneFinished);
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.UIMessage_AddToTick, OnUIMessage_AddToTick);
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.UIMessage_RemoveFromTick, OnUIMessage_RemoveFromTick);

            UnLoadModule();
            RemoveAllTickPage();
            this.Stop();
        }

        public void Run()
        {
            tickObj.Run();
        }

        public void Stop()
        {
            tickObj.Stop();
        }
        public void Pause()
        {
            tickObj.Pause();
        }
        public void Pause(bool isPause)
        {
            tickObj.isPause = isPause;
        }

        public void Tick(int deltaTimeMS)
        {
#if UNITY_EDITOR
            tickCount++;
#endif
            tickObj.Tick(deltaTimeMS);
        }

        protected void AddPageToTick(UIPage page)
        {
            tickObj.tickedPages.Add(page.name, page);
        }

        protected void RemovePageFromTick(string pageName)
        {
            tickObj.tickedPages.Remove(pageName);
        }

        public void LoadModule()
        {
            //gameModules.AddMoudle()

        }

        public void UnLoadModule()
        {
            gameModules.Release();
        }

        public void RemoveAllTickPage()
        {
            tickObj.tickedPages.Clear();
        }

        protected void InitGlobaData()
        {
            GlobalDataManager.GetInstance().Init();
        }

        protected void OnLoadSceneFinished(Message msg)
        {
            LoadModule();
            gameModules.Run();
        }

        protected void OnUIMessage_AddToTick(Message msg)
        {
            var _msg = msg as UIMessage_AddToTick;

            tickObj.tickedPages.Add(_msg.page.name, _msg.page);
        }

        protected void OnUIMessage_RemoveFromTick(Message msg)
        {
            var _msg = msg as MessageString;
            RemovePageFromTick(_msg.str);
        }

        protected void OnNotFoundMessage(int messageID)
        {
            if (messageID < 10001)
            {
                LogWarp.LogErrorFormat("消息未注册  {0}", (UFrameBuildinMessage)messageID);
                return;
            }

            LogWarp.LogErrorFormat("消息未注册  {0}  {1}", (GameMessageDefine)messageID, messageID);
        }

        [System.Diagnostics.Conditional("UNITY_ANDROID")]
        protected void SetAndroidQuit()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Escape))
#else
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape)) 
#endif
            {
                PageMgr.ShowPage<UIQuitGamePage>();
            }
        }
    }

}
