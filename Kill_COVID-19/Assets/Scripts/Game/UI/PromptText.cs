using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.GlobalData;
using UFrame.MiniGame;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PromptText
{
    public static PromptText Instance()
    {
        if (instance == null)
        {
            instance = new PromptText();
        }
        return instance;
    }

    private static PromptText instance;

    private Color textColor;

    GameObject go;
    public static void CreatePromptText(bool isL10N, string data)
    {
        if (!isL10N)
        {
            Instance().CreateText(data);
            return;
        }

        string str = GlobalDataManager.GetInstance().i18n.GetL10N(data);
        Instance().CreateText(str);

    }

    public static void DestroyPromptText()
    {
        Instance().DestroyText();
    }

    private readonly float ColorTime = 0.1f;
//    1 字的透明度从0-255  耗时0.1s
//    2 字的位置从初始位置上移150像素，sineout 耗时1s
//    3 停留在该位置1s
//    4 字的位置再次上移100像素，sinein 耗时0.5s
//    5 字的透明度从255-0  耗时0.1s
    private void CreateText(string langeKey)
    {
        //var obj = Resources.Load("uiprefab/TitleText", typeof(GameObject)) as GameObject;
        //go = GameObject.Instantiate<GameObject>(obj, UIRoot.Instance.promptRoot) as GameObject;

        go = ResourceManager.GetInstance().LoadGameObject("UIPrefab/TitleText");
        go.transform.SetParent(UIRoot.Instance.promptRoot);
        go.transform.localScale = Vector3.one;
        var rect = go.transform as RectTransform;
        Text text = go.GetComponentInChildren<Text>();
        textColor = text.color;
        //text.SetText(langeKey);
        text.text = langeKey;
        //text.text = "AD_Fail";
        rect.anchoredPosition = Vector2.zero;
        text.color = new Color(textColor.r, textColor.g, textColor.b, 0);
        //1 字的透明度从0-255  耗时0.1s
        text.DOFade(1, ColorTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            //            2 字的位置从初始位置上移150像素，sineout 耗时1s
            rect.DOLocalMove(rect.localPosition + new Vector3(0, 150, 0), 1).SetEase(Ease.OutSine).OnComplete(() =>
                  {
                    //3 停留在该位置1s
                    Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
                      {
                        //    4 字的位置再次上移100像素，sinein 耗时0.5s
                        rect.DOLocalMove(rect.localPosition + new Vector3(0, 100, 0), 0.5f).SetEase(Ease.InSine)
                              .OnComplete(() =>
                              {
                                //    5 字的透明度从255-0  耗时0.1s
                                text.DOFade(0, ColorTime).SetEase(Ease.Linear).OnComplete(() =>
                                  {
                                      GameObject.Destroy(go);
                                  });
                              });
                      });
                  });
        });
    }

    void DestroyText()
    {
        if (go != null)
        {
            go.SetActive(false);
        }
    }


}


