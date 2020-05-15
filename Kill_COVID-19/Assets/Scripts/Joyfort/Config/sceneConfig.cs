using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class sceneConfig
	{
		private sceneConfig(){ 
		}
		private static sceneConfig _inst;
		public static sceneConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new sceneConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,sceneCell> AllData;
		public sceneCell getCell(string key){
			sceneCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public sceneCell getCell(int key){
			sceneCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 25;
		private void InitData(){
			this.AllData = new Dictionary<string,sceneCell> ();
			this.AllData.Add("0",new sceneCell("Ui_Text_103","海岛1",1,1,0,"UIAtlas/UIIcon/Tiger","dwy_9",1,1,1,0,0));
			this.AllData.Add("1",new sceneCell("Ui_Text_103","海岛2",1,2,48,"UIAtlas/UIIcon/Tiger","dwy_9",3,1,0,300,0));
			this.AllData.Add("2",new sceneCell("Ui_Text_103","海岛3",1,3,180,"UIAtlas/UIIcon/Tiger","dwy_9",9,1,0,900,0));
			this.AllData.Add("3",new sceneCell("Ui_Text_103","海岛4",1,4,379,"UIAtlas/UIIcon/Tiger","dwy_9",12,1,0,1400,0));
			this.AllData.Add("4",new sceneCell("Ui_Text_103","海岛5",1,5,648,"UIAtlas/UIIcon/Tiger","dwy_9",15,1,0,1900,0));
			this.AllData.Add("5",new sceneCell("Ui_Text_104","城市1",2,1,800,"UIAtlas/UIIcon/Tiger","city_scene",60,0,0,2400,1));
			this.AllData.Add("6",new sceneCell("Ui_Text_104","城市2",2,2,900,"UIAtlas/UIIcon/Tiger","city_scene",70,0,0,2900,1));
			this.AllData.Add("7",new sceneCell("Ui_Text_104","城市3",2,3,1000,"UIAtlas/UIIcon/Tiger","city_scene",80,0,0,3400,1));
			this.AllData.Add("8",new sceneCell("Ui_Text_104","城市4",2,4,1100,"UIAtlas/UIIcon/Tiger","city_scene",90,0,0,3900,1));
			this.AllData.Add("9",new sceneCell("Ui_Text_104","城市5",2,5,1200,"UIAtlas/UIIcon/Tiger","city_scene",100,0,0,4400,1));
			this.AllData.Add("10",new sceneCell("Ui_Text_105","沙漠1",3,1,1300,"UIAtlas/UIIcon/Tiger","west_scene",110,0,0,4900,2));
			this.AllData.Add("11",new sceneCell("Ui_Text_105","沙漠2",3,2,1400,"UIAtlas/UIIcon/Tiger","west_scene",120,0,0,5400,2));
			this.AllData.Add("12",new sceneCell("Ui_Text_105","沙漠3",3,3,1500,"UIAtlas/UIIcon/Tiger","west_scene",130,0,0,5900,2));
			this.AllData.Add("13",new sceneCell("Ui_Text_105","沙漠4",3,4,1600,"UIAtlas/UIIcon/Tiger","west_scene",140,0,0,6400,2));
			this.AllData.Add("14",new sceneCell("Ui_Text_105","沙漠5",3,5,1700,"UIAtlas/UIIcon/Tiger","west_scene",150,0,0,6900,2));
			this.AllData.Add("15",new sceneCell("Ui_Text_106","绿洲1",4,1,1800,"UIAtlas/UIIcon/Tiger","dwy_9_m",160,0,0,7400,3));
			this.AllData.Add("16",new sceneCell("Ui_Text_106","绿洲2",4,2,1900,"UIAtlas/UIIcon/Tiger","dwy_9_m",170,0,0,7900,3));
			this.AllData.Add("17",new sceneCell("Ui_Text_106","绿洲3",4,3,2000,"UIAtlas/UIIcon/Tiger","dwy_9_m",180,0,0,8400,3));
			this.AllData.Add("18",new sceneCell("Ui_Text_106","绿洲4",4,4,2100,"UIAtlas/UIIcon/Tiger","dwy_9_m",190,0,0,8900,3));
			this.AllData.Add("19",new sceneCell("Ui_Text_106","绿洲5",4,5,2200,"UIAtlas/UIIcon/Tiger","dwy_9_m",200,0,0,9400,3));
			this.AllData.Add("20",new sceneCell("Ui_Text_107","花海1",5,1,2300,"UIAtlas/UIIcon/Tiger","dwy_9_m",210,0,0,9900,4));
			this.AllData.Add("21",new sceneCell("Ui_Text_107","花海2",5,2,2400,"UIAtlas/UIIcon/Tiger","dwy_9_m",220,0,0,10400,4));
			this.AllData.Add("22",new sceneCell("Ui_Text_107","花海3",5,3,2500,"UIAtlas/UIIcon/Tiger","dwy_9_m",230,0,0,10900,4));
			this.AllData.Add("23",new sceneCell("Ui_Text_107","花海4",5,4,2600,"UIAtlas/UIIcon/Tiger","dwy_9_m",240,0,0,11400,4));
			this.AllData.Add("24",new sceneCell("Ui_Text_107","花海5",5,5,2700,"UIAtlas/UIIcon/Tiger","dwy_9_m",250,0,0,11900,4));
		}
	}
	public class sceneCell
	{
		///<summary>
		///场景名称
		///<summary>
		public readonly string scenename;
		///<summary>
		///ta调用名称
		///<summary>
		public readonly string tascenename;
		///<summary>
		///场景类型
		///<summary>
		public readonly int scenetype;
		///<summary>
		///场景顺序
		///<summary>
		public readonly int sceneorder;
		///<summary>
		///开启星级
		///<summary>
		public readonly int openstar;
		///<summary>
		///图标
		///<summary>
		public readonly string icon;
		///<summary>
		///场景资源加载
		///<summary>
		public readonly string resourceid;
		///<summary>
		///翻倍系数
		///<summary>
		public readonly int doublenum;
		///<summary>
		///版本开放控制
		///<summary>
		public readonly int israwopen;
		///<summary>
		///起始任务ID
		///<summary>
		public readonly int missionstart;
		///<summary>
		///场景等级
		///<summary>
		public readonly int scenelv;
		///<summary>
		///关联货币
		///<summary>
		public readonly int moneyid;
		public sceneCell(string scenename,string tascenename,int scenetype,int sceneorder,int openstar,string icon,string resourceid,int doublenum,int israwopen,int missionstart,int scenelv,int moneyid){
			this.scenename = scenename;
			this.tascenename = tascenename;
			this.scenetype = scenetype;
			this.sceneorder = sceneorder;
			this.openstar = openstar;
			this.icon = icon;
			this.resourceid = resourceid;
			this.doublenum = doublenum;
			this.israwopen = israwopen;
			this.missionstart = missionstart;
			this.scenelv = scenelv;
			this.moneyid = moneyid;
		}
	}
}