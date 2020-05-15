/*******************************************************************
* FileName:     SimpleAnimation.cs
* Author:       Fan Zheng Yong
* Date:         2019-9-4
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame
{
    public class SimpleAnimation
    {
        public Animation animation;
        public string lastAnimName;

        public void Init(GameObject owner)
        {
            animation = owner.GetComponentInChildren<Animation>();
#if UNITY_EDITOR
            if (animation == null)
            {
                string e = string.Format("{0} 上没有 Animation组件", owner.name);
                throw new System.Exception(e);
            }
#endif
        }

        public void Init(Animation anim)
        {
            animation = anim;
#if UNITY_EDITOR
            if (animation == null)
            {
                string e = string.Format("传入的Animation组件为空");
                throw new System.Exception(e);
            }
#endif
        }

        public void Release()
        {
            Stop();
            animation = null;
            lastAnimName = null;
        }

        public void Play(string animName)
        {
            lastAnimName = animName;
            animation.Play(animName);
        }

        public void Stop()
        {
            animation.Stop();
            animation.Rewind(lastAnimName);
        }

        public bool IsRunning()
        {
            return animation.isPlaying;
        }

        public float GetClipLength(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0f;
            }
#if UNITY_EDITOR
            if (animation[name] == null)
            {
                string e = string.Format("{0} 没有动画 {1}", animation.gameObject.name, name);
                throw new System.Exception(e);
            }
#endif
            return animation[name].length;
        }

        public bool GetAutoPlay()
        {
            return animation.playAutomatically;
        }

        public void SetAutoPlay(bool isPlay)
        {
            animation.playAutomatically = isPlay;
            if (isPlay)
            {
                lastAnimName = animation.clip.name;
                animation.Play();

                return;
            }

            animation.Stop();
        }

        //public void GetAllAnim()
        //{
        //    foreach (AnimationState state in animation)
        //    {
        //        //animName[i++] = state.name;
        //        //Debug.Log(animName[i - 1]);
        //    }
        //}
    }
}

