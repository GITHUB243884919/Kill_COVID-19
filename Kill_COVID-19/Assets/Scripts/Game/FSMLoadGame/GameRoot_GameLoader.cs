using System.Collections;
using System.Collections.Generic;
using HillUFrame;
using HillUFrame.Common;
using UnityEngine;

namespace Game
{
    public enum CrossRoadGameState
    {
        /// <summary>
        /// 加载原始场景
        /// </summary>
        LoadOrgScene,
    }

    public partial class GameRoot : SingletonMono<GameRoot>
    {
        public FSMGameLoad fsmGameLoad;

        public void LoadGame()
        {
            fsmGameLoad = new FSMGameLoad(null);
            fsmGameLoad.AddState(new StateLoadOrgScene((int)CrossRoadGameState.LoadOrgScene, fsmGameLoad));

            fsmGameLoad.Run();
            fsmGameLoad.GotoState((int)CrossRoadGameState.LoadOrgScene);
        }
    }
}

