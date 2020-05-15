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
using UFrame.Common;
using UFrame;
using Game.GlobalData;
using Game.MessageCenter;
using UFrame.MessageCenter;
using UFrame.Logger;
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

            if (!this.CouldRun())
            {
                return;
            }

            GameModuleManager.GetInstance().Tick(deltaTimeMS);

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

            LogWarp.Log("LoadingMgr.Inst.isRunning = true");
            LoadingMgr.Inst.isRunning = true;
        }

        public void Update()
        {
            SetAndroidQuit();
            this.Tick(Math_F.FloatToInt1000(Time.deltaTime));
        }

        public void Init()
        {
            MessageManager.GetInstance().SetCallbackNotFoundMessage(this.OnNotFoundMessage);
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
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.UIMessage_AddToTick, OnUIMessage_AddToTick);
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.UIMessage_RemoveFromTick, OnUIMessage_RemoveFromTick);

            UnLoadModule();
            //tickObj.tickedPages.Clear();
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

        public void LoadSceneModule()
        {
            ////
            ////玩家数据维护
            //GameModuleManager.GetInstance().AddMoudle(new PlayerDataModule(moduleOrderID++));
            ////动物数据维护
            ////GameModuleManager.GetInstance().AddMoudle(new AnimalModule(moduleOrderID++));

            ////生成
            //GameModuleManager.GetInstance().AddMoudle(new ParkingCenter(moduleOrderID++));
            //GameModuleManager.GetInstance().AddMoudle(new SpawnModule(moduleOrderID++));

            ////决策模块
            ////大门
            //GameModuleManager.GetInstance().AddMoudle(new EntryGateModule(moduleOrderID++));

            ////动物栏
            //GameModuleManager.GetInstance().AddMoudle(new LittleZooModule(moduleOrderID++));

            ////Buff
            //GameModuleManager.GetInstance().AddMoudle(new BuffModule(moduleOrderID++));

            ////道具
            //GameModuleManager.GetInstance().AddMoudle(new ItemModule(moduleOrderID++));

            ////移动
            //GameModuleManager.GetInstance().AddMoudle(new MoveMovableEntityMoudle(moduleOrderID++));

            ////世界地图
            //GameModuleManager.GetInstance().AddMoudle(new WordlMapModule(moduleOrderID++));

            ////引导任务
            //GameModuleManager.GetInstance().AddMoudle(new GuideMissionModule(moduleOrderID++));

            ////杂项模块
            //GameModuleManager.GetInstance().AddMoudle(new MiscModule(moduleOrderID++));

            ////图鉴收集模块
            //GameModuleManager.GetInstance().AddMoudle(new AnimalAtlasModule(moduleOrderID++));

            GameModuleManager.GetInstance().Stop();
        }

        public void UnLoadModule()
        {
            GameModuleManager.GetInstance().Release();
        }

        public void RemoveAllTickPage()
        {
            tickObj.tickedPages.Clear();
        }

        protected void InitGlobaData()
        {
            GlobalDataManager.GetInstance().Init();
        }

        protected void OnLoadZooSceneFinished(Message msg)
        {
            LoadSceneModule();
            GameModuleManager.GetInstance().Run();

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
