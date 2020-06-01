/*******************************************************************
* FileName:     MessageManager.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using System;
using HillUFrame.Common;
using HillUFrame.MessageCenter;

namespace HillUFrame
{
    public class MessageManager : Singleton<MessageManager>, ISingleton
    {
        public ActionMessageCenter gameMessageCenter = new ActionMessageCenter();
        public void Init()
        {
        }

        public void Tick()
        {
            gameMessageCenter.Tick();
        }

        public void SetCallbackNotFoundMessage(System.Action<int> callback)
        {
            gameMessageCenter.callbackNotFoundMessage = callback;
        }

        public void Regist(int messageID, Action<HillUFrame.MessageCenter.Message> executor)
        {
            gameMessageCenter.Regist(messageID, executor);
        }

        public void UnRegist(int messageID, Action<HillUFrame.MessageCenter.Message> executor)
        {
            gameMessageCenter.UnRegist(messageID, executor);
        }

        public void Send(HillUFrame.MessageCenter.Message msg, bool immediately = true)
        {
            gameMessageCenter.Send(msg, immediately);
        }

        public void Send(int messageID, bool immediately = true)
        {
            gameMessageCenter.Send(messageID, immediately);
        }
    }
}

