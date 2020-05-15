using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class moneyConfig
	{
		private moneyConfig(){ 
		}
		private static moneyConfig _inst;
		public static moneyConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new moneyConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,moneyCell> AllData;
		public moneyCell getCell(string key){
			moneyCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public moneyCell getCell(int key){
			moneyCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 5;
		private void InitData(){
			this.AllData = new Dictionary<string,moneyCell> ();
			this.AllData.Add("0",new moneyCell("money_1","UIAtlas/Main/Gold"));
			this.AllData.Add("1",new moneyCell("money_2","UIAtlas/Main/Gold"));
			this.AllData.Add("2",new moneyCell("money_3","UIAtlas/Main/Gold"));
			this.AllData.Add("3",new moneyCell("money_4","UIAtlas/Main/Gold"));
			this.AllData.Add("4",new moneyCell("money_5","UIAtlas/Main/Gold"));
		}
	}
	public class moneyCell
	{
		///<summary>
		///货币名称
		///<summary>
		public readonly string moneyname;
		///<summary>
		///图标
		///<summary>
		public readonly string moneyicon;
		public moneyCell(string moneyname,string moneyicon){
			this.moneyname = moneyname;
			this.moneyicon = moneyicon;
		}
	}
}