using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UFrame;

public class UIWaitAd : UIPage {
	public static bool isWaitLoadingAd = false;
		
	private Image _rotImg;
	private Text _text;
	private GameObject bgGo;

    VoidParamIntCDs voidParamIntCDs = new VoidParamIntCDs();

    public UIWaitAd() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None, UITickedMode.Update)
	{
		uiPath = "UIPrefab/UIWaitAd";
	}

	public override void Awake (GameObject go)
	{
		base.Awake (go);
		UIWaitAd.isWaitLoadingAd = true;

		this._rotImg = this.RegistCompent<Image> ("bg/rotImg");
		this._text = this.RegistCompent<Text>("titleText");
		this._text.gameObject.SetActive (false);
		this.bgGo = this.transform.Find("bg").gameObject;
	}

	public override void Refresh ()
	{
		base.Refresh ();
		this.bgGo.SetActive (true);

        if (voidParamIntCDs == null)
        {
            voidParamIntCDs = new VoidParamIntCDs();
        }
        else
        {
            voidParamIntCDs.Release();
        }
        //int waitCD = (int)this.m_data;
        //voidParamIntCDs.AddCD(waitCD, null, (CD, _param) =>
        //{
        //    HideAndShowText();
        //});
        //voidParamIntCDs.Run();
    }

    public override void Hide()
    {
        base.Hide();
        UIWaitAd.isWaitLoadingAd = false;
        if (voidParamIntCDs != null)
        {
            voidParamIntCDs.Release();
        }
    }

    public override void Tick(int deltaTimeMS)
    {
        if (voidParamIntCDs != null)
        {
            voidParamIntCDs.Tick(deltaTimeMS);
        }
    }

    public void HideAndShowText()
    {
		this.Hide ();	
	}
}
