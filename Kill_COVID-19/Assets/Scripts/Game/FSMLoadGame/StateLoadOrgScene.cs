using Game.GlobalData;
using Game.MessageCenter;
using HillUFrame;
using HillUFrame.MiniGame;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 加载原始场景
    /// </summary>
    public class StateLoadOrgScene : FSMState
    {
        public StateLoadOrgScene(int stateName, FSMMachine fsmCtr) :
            base(stateName, fsmCtr)
        {
        }

        public override void Enter(int preStateName)
        {
            base.Enter(preStateName);

            ResourceManager.GetInstance().LoadSceneAsync("Stage", LoadSceneFinished, null);
            PageMgr.ShowPage<UIStage>();
        }

        public override void AddAllConvertCond()
        {
        }

        public override void Tick(int deltaTimeMS)
        {
        }

        public void LoadSceneFinished()
        {
            GlobalDataManager.GetInstance().transPlayer = GameObject.Find("Player").transform;
            MessageManager.GetInstance().Send((int)GameMessageDefine.LoadSceneFinished);
        }
    }
}