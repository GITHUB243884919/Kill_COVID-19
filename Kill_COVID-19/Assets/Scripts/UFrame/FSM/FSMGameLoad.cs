using System;
using HillUFrame;
using UnityEngine;

namespace HillUFrame
{
    public class FSMGameLoad : FSMMachine
    {
        public FSMGameLoad(System.Action callBack_OpenLoadingPage)
        {
            callBack_OpenLoadingPage?.Invoke();
        }

        public override void Release()
        {
            base.Release();
        }

        public void SetLoadingPageSlider()
        {
            //(PageMgr.allPages[loadingPageName] as UICrossRoadLoading).SliderValueLoading(
            //    1f / numState);
        }
    }
}

