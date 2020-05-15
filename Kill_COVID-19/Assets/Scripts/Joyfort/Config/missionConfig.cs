using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class missionConfig
	{
		private missionConfig(){ 
		}
		private static missionConfig _inst;
		public static missionConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new missionConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,missionCell> AllData;
		public missionCell getCell(string key){
			missionCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public missionCell getCell(int key){
			missionCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 262;
		private void InitData(){
			this.AllData = new Dictionary<string,missionCell> ();
			this.AllData.Add("1",new missionCell(2,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1001",1,10,"3002",1,0));
			this.AllData.Add("2",new missionCell(3,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1001",2,2,"3002",1,0));
			this.AllData.Add("3",new missionCell(4,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1001",3,2,"3002",1,0));
			this.AllData.Add("4",new missionCell(5,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,10,"3002",1,0));
			this.AllData.Add("5",new missionCell(6,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,2,"3002",1,0));
			this.AllData.Add("6",new missionCell(7,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,16,"3003",1,0));
			this.AllData.Add("7",new missionCell(8,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,10,"3002",1,0));
			this.AllData.Add("8",new missionCell(9,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"0",2,12,"3003",1,0));
			this.AllData.Add("9",new missionCell(10,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1001",1,25,"3003",1,0));
			this.AllData.Add("10",new missionCell(11,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1001",2,3,"3003",1,0));
			this.AllData.Add("11",new missionCell(12,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1001",3,3,"3003",1,0));
			this.AllData.Add("12",new missionCell(13,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1001",1,5,"3003",0,0));
			this.AllData.Add("13",new missionCell(14,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,25,"3003",1,0));
			this.AllData.Add("14",new missionCell(15,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,3,"3004",1,0));
			this.AllData.Add("15",new missionCell(16,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,23,"3004",1,0));
			this.AllData.Add("16",new missionCell(17,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,25,"3004",1,0));
			this.AllData.Add("17",new missionCell(18,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"1",2,18,"3004",1,0));
			this.AllData.Add("18",new missionCell(19,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1002",1,10,"3004",1,0));
			this.AllData.Add("19",new missionCell(20,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1002",2,2,"3004",1,0));
			this.AllData.Add("20",new missionCell(21,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1002",3,2,"3004",1,0));
			this.AllData.Add("21",new missionCell(22,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1002",1,10,"3004",0,0));
			this.AllData.Add("22",new missionCell(23,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,50,"3005",1,0));
			this.AllData.Add("23",new missionCell(24,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,7,"3005",1,0));
			this.AllData.Add("24",new missionCell(25,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,31,"3005",1,0));
			this.AllData.Add("25",new missionCell(26,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,50,"3005",1,0));
			this.AllData.Add("26",new missionCell(27,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"2",2,15,"3005",1,0));
			this.AllData.Add("27",new missionCell(28,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1003",1,10,"3005",1,0));
			this.AllData.Add("28",new missionCell(29,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1003",2,2,"3006",1,0));
			this.AllData.Add("29",new missionCell(30,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1003",3,2,"3006",1,0));
			this.AllData.Add("30",new missionCell(31,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1003",1,10,"3006",0,0));
			this.AllData.Add("31",new missionCell(32,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,75,"3006",1,0));
			this.AllData.Add("32",new missionCell(33,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,11,"3006",1,0));
			this.AllData.Add("33",new missionCell(34,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,36,"3007",1,0));
			this.AllData.Add("34",new missionCell(35,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,100,"3007",1,0));
			this.AllData.Add("35",new missionCell(36,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"3",2,4,"3007",1,0));
			this.AllData.Add("36",new missionCell(37,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1001",1,150,"3007",1,0));
			this.AllData.Add("37",new missionCell(38,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1001",2,8,"3007",1,0));
			this.AllData.Add("38",new missionCell(39,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1001",3,13,"3008",1,0));
			this.AllData.Add("39",new missionCell(40,"Ui_Text_97","观看发布广告视频{0}次","Ui_Text_97",5,"Add_Double_Advert",1,1,"3008",0,0));
			this.AllData.Add("40",new missionCell(41,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1003",1,100,"3008",1,0));
			this.AllData.Add("41",new missionCell(42,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1003",2,6,"3008",1,0));
			this.AllData.Add("42",new missionCell(43,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1003",3,9,"3008",1,0));
			this.AllData.Add("43",new missionCell(44,"Ui_Text_98","观看更多游客视频{0}次","Ui_Text_98",5,"Add_Tourist_Advert",1,1,"3008",0,0));
			this.AllData.Add("44",new missionCell(45,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"0",2,75,"3009",1,0));
			this.AllData.Add("45",new missionCell(46,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1002",1,150,"3009",1,0));
			this.AllData.Add("46",new missionCell(47,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1002",2,8,"3009",1,0));
			this.AllData.Add("47",new missionCell(48,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1002",3,14,"3009",1,0));
			this.AllData.Add("48",new missionCell(49,"Ui_Text_99","观看快速售票广告{0}次","Ui_Text_99",5,"Add_Ticket_Advert",1,1,"3009",0,0));
			this.AllData.Add("49",new missionCell(50,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,150,"3009",1,0));
			this.AllData.Add("50",new missionCell(51,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,15,"3009",1,0));
			this.AllData.Add("51",new missionCell(52,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,46,"3009",1,0));
			this.AllData.Add("52",new missionCell(53,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,150,"3009",1,0));
			this.AllData.Add("53",new missionCell(54,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"4",2,2,"3010",1,0));
			this.AllData.Add("54",new missionCell(55,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1004",1,50,"3010",1,0));
			this.AllData.Add("55",new missionCell(56,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1004",2,4,"3010",1,0));
			this.AllData.Add("56",new missionCell(57,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1004",3,6,"3011",1,0));
			this.AllData.Add("57",new missionCell(58,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1004",1,10,"3011",0,0));
			this.AllData.Add("58",new missionCell(59,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,200,"3012",1,0));
			this.AllData.Add("59",new missionCell(60,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,17,"3012",1,0));
			this.AllData.Add("60",new missionCell(61,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,55,"3012",1,0));
			this.AllData.Add("61",new missionCell(62,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,200,"3012",1,0));
			this.AllData.Add("62",new missionCell(63,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"5",2,3,"3012",1,0));
			this.AllData.Add("63",new missionCell(64,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1003",1,200,"3012",1,0));
			this.AllData.Add("64",new missionCell(65,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1003",2,10,"3012",1,0));
			this.AllData.Add("65",new missionCell(66,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1003",3,19,"3012",1,0));
			this.AllData.Add("66",new missionCell(67,"Ui_Text_100","观看快速观光广告{0}次","Ui_Text_100",5,"Add_Visit_Advert",1,1,"3012",0,0));
			this.AllData.Add("67",new missionCell(68,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1001",1,300,"3012",1,0));
			this.AllData.Add("68",new missionCell(69,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1001",2,14,"3013",1,0));
			this.AllData.Add("69",new missionCell(70,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1001",3,27,"3013",1,0));
			this.AllData.Add("70",new missionCell(71,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1004",1,150,"3013",1,0));
			this.AllData.Add("71",new missionCell(72,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1004",2,8,"3014",1,0));
			this.AllData.Add("72",new missionCell(73,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1004",3,14,"3014",1,0));
			this.AllData.Add("73",new missionCell(74,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,250,"3014",1,0));
			this.AllData.Add("74",new missionCell(75,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,20,"3014",1,0));
			this.AllData.Add("75",new missionCell(76,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,66,"3014",1,0));
			this.AllData.Add("76",new missionCell(77,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1005",1,100,"3014",1,0));
			this.AllData.Add("77",new missionCell(78,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1005",2,6,"3015",1,0));
			this.AllData.Add("78",new missionCell(79,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1005",3,10,"3015",1,0));
			this.AllData.Add("79",new missionCell(80,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1005",1,10,"3015",0,0));
			this.AllData.Add("80",new missionCell(81,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1003",1,300,"3015",1,0));
			this.AllData.Add("81",new missionCell(82,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1003",2,14,"3015",1,0));
			this.AllData.Add("82",new missionCell(83,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1003",3,27,"3016",1,0));
			this.AllData.Add("83",new missionCell(84,"Ui_Text_97","观看发布广告视频{0}次","Ui_Text_97",5,"Add_Double_Advert",1,1,"3016",0,0));
			this.AllData.Add("84",new missionCell(85,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,300,"3017",1,0));
			this.AllData.Add("85",new missionCell(86,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"5",2,20,"3016",1,0));
			this.AllData.Add("86",new missionCell(87,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,300,"3016",1,0));
			this.AllData.Add("87",new missionCell(88,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,22,"3017",1,0));
			this.AllData.Add("88",new missionCell(89,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,76,"3017",1,0));
			this.AllData.Add("89",new missionCell(90,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1002",1,400,"3017",1,0));
			this.AllData.Add("90",new missionCell(91,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1002",2,15,"3017",1,0));
			this.AllData.Add("91",new missionCell(92,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1002",3,36,"3017",1,0));
			this.AllData.Add("92",new missionCell(93,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1005",1,200,"3018",1,0));
			this.AllData.Add("93",new missionCell(94,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1005",2,10,"3018",1,0));
			this.AllData.Add("94",new missionCell(95,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1005",3,18,"3018",1,0));
			this.AllData.Add("95",new missionCell(96,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,350,"3018",1,0));
			this.AllData.Add("96",new missionCell(97,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"6",2,3,"3019",1,0));
			this.AllData.Add("97",new missionCell(98,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1011",1,50,"3019",1,0));
			this.AllData.Add("98",new missionCell(99,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1011",2,4,"3019",1,0));
			this.AllData.Add("99",new missionCell(100,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1011",3,5,"3019",1,0));
			this.AllData.Add("100",new missionCell(101,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1001",1,500,"3019",1,0));
			this.AllData.Add("101",new missionCell(102,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1001",2,15,"3019",1,0));
			this.AllData.Add("102",new missionCell(103,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1001",3,41,"3020",1,0));
			this.AllData.Add("103",new missionCell(104,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1005",1,250,"3020",1,0));
			this.AllData.Add("104",new missionCell(105,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1005",2,12,"3020",1,0));
			this.AllData.Add("105",new missionCell(106,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1005",3,23,"3020",1,0));
			this.AllData.Add("106",new missionCell(107,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1002",1,500,"3020",1,0));
			this.AllData.Add("107",new missionCell(108,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1002",2,15,"3021",1,0));
			this.AllData.Add("108",new missionCell(109,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1002",3,41,"3021",1,0));
			this.AllData.Add("109",new missionCell(110,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,400,"3021",1,0));
			this.AllData.Add("110",new missionCell(111,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,28,"3021",1,0));
			this.AllData.Add("111",new missionCell(112,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,95,"3022",1,0));
			this.AllData.Add("112",new missionCell(113,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,418,"3022",1,0));
			this.AllData.Add("113",new missionCell(114,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"7",2,2,"3022",1,0));
			this.AllData.Add("114",new missionCell(115,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1012",1,75,"3022",1,0));
			this.AllData.Add("115",new missionCell(116,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1012",2,5,"3023",1,0));
			this.AllData.Add("116",new missionCell(117,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1012",3,7,"3023",1,0));
			this.AllData.Add("117",new missionCell(118,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1012",1,10,"3023",0,0));
			this.AllData.Add("118",new missionCell(119,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1001",1,610,"3023",1,0));
			this.AllData.Add("119",new missionCell(120,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1001",2,15,"3023",1,0));
			this.AllData.Add("120",new missionCell(121,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,450,"3023",1,0));
			this.AllData.Add("121",new missionCell(122,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"5",2,75,"3024",1,0));
			this.AllData.Add("122",new missionCell(123,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1013",1,50,"3024",1,0));
			this.AllData.Add("123",new missionCell(124,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1013",2,4,"3024",1,0));
			this.AllData.Add("124",new missionCell(125,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1013",3,6,"3024",1,0));
			this.AllData.Add("125",new missionCell(126,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1013",1,10,"3024",0,0));
			this.AllData.Add("126",new missionCell(127,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1003",1,600,"3025",1,0));
			this.AllData.Add("127",new missionCell(128,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1003",2,15,"3025",1,0));
			this.AllData.Add("128",new missionCell(129,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,500,"3025",1,0));
			this.AllData.Add("129",new missionCell(130,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,32,"3025",1,0));
			this.AllData.Add("130",new missionCell(131,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,111,"3025",1,0));
			this.AllData.Add("131",new missionCell(132,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,500,"3025",1,0));
			this.AllData.Add("132",new missionCell(133,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"4",2,100,"3026",1,0));
			this.AllData.Add("133",new missionCell(134,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"6",2,30,"3026",1,0));
			this.AllData.Add("134",new missionCell(135,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1014",1,50,"3026",1,0));
			this.AllData.Add("135",new missionCell(136,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1014",2,4,"3026",1,0));
			this.AllData.Add("136",new missionCell(137,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1014",3,6,"3026",1,0));
			this.AllData.Add("137",new missionCell(138,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1014",1,10,"3026",0,0));
			this.AllData.Add("138",new missionCell(139,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1005",1,500,"3027",1,0));
			this.AllData.Add("139",new missionCell(140,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1005",2,15,"3027",1,0));
			this.AllData.Add("140",new missionCell(141,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1005",3,41,"3027",1,0));
			this.AllData.Add("141",new missionCell(142,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1015",1,25,"3028",1,0));
			this.AllData.Add("142",new missionCell(143,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1015",2,3,"3028",1,0));
			this.AllData.Add("143",new missionCell(144,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1015",3,3,"3028",1,0));
			this.AllData.Add("144",new missionCell(145,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1015",1,10,"3028",0,0));
			this.AllData.Add("145",new missionCell(146,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1001",1,800,"3028",1,0));
			this.AllData.Add("146",new missionCell(147,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1001",2,15,"3028",1,0));
			this.AllData.Add("147",new missionCell(148,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1013",1,200,"3029",1,0));
			this.AllData.Add("148",new missionCell(149,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1013",2,10,"3029",1,0));
			this.AllData.Add("149",new missionCell(150,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1013",3,18,"3029",1,0));
			this.AllData.Add("150",new missionCell(151,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1002",1,800,"3029",1,0));
			this.AllData.Add("151",new missionCell(152,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1002",2,15,"3029",1,0));
			this.AllData.Add("152",new missionCell(153,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,600,"3030",1,0));
			this.AllData.Add("153",new missionCell(154,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,37,"3030",1,0));
			this.AllData.Add("154",new missionCell(155,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,130,"3030",1,0));
			this.AllData.Add("155",new missionCell(156,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,600,"3030",1,0));
			this.AllData.Add("156",new missionCell(157,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,37,"3031",1,0));
			this.AllData.Add("157",new missionCell(158,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,133,"3031",1,0));
			this.AllData.Add("158",new missionCell(159,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1003",1,800,"3031",1,0));
			this.AllData.Add("159",new missionCell(160,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1003",2,15,"3031",1,0));
			this.AllData.Add("160",new missionCell(161,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1003",2,15,"3031",1,0));
			this.AllData.Add("161",new missionCell(162,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1016",1,75,"3031",1,0));
			this.AllData.Add("162",new missionCell(163,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1016",2,5,"3031",1,0));
			this.AllData.Add("163",new missionCell(164,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1016",3,8,"3031",1,0));
			this.AllData.Add("164",new missionCell(165,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1016",1,10,"3031",0,0));
			this.AllData.Add("165",new missionCell(166,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1012",1,400,"3032",1,0));
			this.AllData.Add("166",new missionCell(167,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1012",2,15,"3032",1,0));
			this.AllData.Add("167",new missionCell(168,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1012",3,34,"3032",1,0));
			this.AllData.Add("168",new missionCell(169,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1017",1,50,"3033",1,0));
			this.AllData.Add("169",new missionCell(170,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1017",2,4,"3033",1,0));
			this.AllData.Add("170",new missionCell(171,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1017",3,6,"3033",1,0));
			this.AllData.Add("171",new missionCell(172,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1017",1,10,"3033",0,0));
			this.AllData.Add("172",new missionCell(173,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1005",1,700,"3033",1,0));
			this.AllData.Add("173",new missionCell(174,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1005",2,15,"3034",1,0));
			this.AllData.Add("174",new missionCell(175,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,700,"3034",1,0));
			this.AllData.Add("175",new missionCell(176,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,42,"3034",1,0));
			this.AllData.Add("176",new missionCell(177,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,149,"3034",1,0));
			this.AllData.Add("177",new missionCell(178,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,700,"3034",1,0));
			this.AllData.Add("178",new missionCell(179,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"6",2,83,"3034",1,0));
			this.AllData.Add("179",new missionCell(180,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1018",1,25,"3035",1,0));
			this.AllData.Add("180",new missionCell(181,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1018",2,3,"3035",1,0));
			this.AllData.Add("181",new missionCell(182,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1018",3,5,"3035",1,0));
			this.AllData.Add("182",new missionCell(183,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1018",1,10,"3035",0,0));
			this.AllData.Add("183",new missionCell(184,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1012",1,500,"3035",1,0));
			this.AllData.Add("184",new missionCell(185,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1012",2,15,"3035",1,0));
			this.AllData.Add("185",new missionCell(186,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1012",3,41,"3035",1,0));
			this.AllData.Add("186",new missionCell(187,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1005",1,800,"3035",1,0));
			this.AllData.Add("187",new missionCell(188,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1005",2,15,"3035",1,0));
			this.AllData.Add("188",new missionCell(189,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1016",1,250,"3036",1,0));
			this.AllData.Add("189",new missionCell(190,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1016",2,12,"3036",1,0));
			this.AllData.Add("190",new missionCell(191,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1016",3,22,"3036",1,0));
			this.AllData.Add("191",new missionCell(192,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1019",1,50,"3037",1,0));
			this.AllData.Add("192",new missionCell(193,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1019",2,4,"3037",1,0));
			this.AllData.Add("193",new missionCell(194,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1019",3,5,"3037",1,0));
			this.AllData.Add("194",new missionCell(195,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1019",1,10,"3037",0,0));
			this.AllData.Add("195",new missionCell(196,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,800,"3038",1,0));
			this.AllData.Add("196",new missionCell(197,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,47,"3038",1,0));
			this.AllData.Add("197",new missionCell(198,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,163,"3038",1,0));
			this.AllData.Add("198",new missionCell(199,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,800,"3038",1,0));
			this.AllData.Add("199",new missionCell(200,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"6",2,110,"3039",1,0));
			this.AllData.Add("200",new missionCell(201,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1020",1,10,"3039",1,0));
			this.AllData.Add("201",new missionCell(202,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1020",2,2,"3039",1,0));
			this.AllData.Add("202",new missionCell(203,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1020",3,3,"3039",1,0));
			this.AllData.Add("203",new missionCell(204,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1020",1,10,"3039",0,0));
			this.AllData.Add("204",new missionCell(205,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1018",1,200,"3039",1,0));
			this.AllData.Add("205",new missionCell(206,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1018",2,10,"3040",1,0));
			this.AllData.Add("206",new missionCell(207,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1018",3,18,"3040",1,0));
			this.AllData.Add("207",new missionCell(208,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1014",1,500,"3040",1,0));
			this.AllData.Add("208",new missionCell(209,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1014",2,15,"3040",1,0));
			this.AllData.Add("209",new missionCell(210,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1014",3,41,"3040",1,0));
			this.AllData.Add("210",new missionCell(211,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1013",1,600,"3040",1,0));
			this.AllData.Add("211",new missionCell(212,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1013",2,15,"3041",1,0));
			this.AllData.Add("212",new missionCell(213,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1016",1,400,"3041",1,0));
			this.AllData.Add("213",new missionCell(214,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1016",2,15,"3041",1,0));
			this.AllData.Add("214",new missionCell(215,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1016",3,34,"3042",1,0));
			this.AllData.Add("215",new missionCell(216,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1019",1,200,"3042",1,0));
			this.AllData.Add("216",new missionCell(217,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1019",2,10,"3042",1,0));
			this.AllData.Add("217",new missionCell(218,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1019",3,18,"3042",1,0));
			this.AllData.Add("218",new missionCell(219,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,900,"3042",1,0));
			this.AllData.Add("219",new missionCell(220,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,52,"3042",1,0));
			this.AllData.Add("220",new missionCell(221,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,900,"3043",1,0));
			this.AllData.Add("221",new missionCell(222,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"7",2,100,"3043",1,0));
			this.AllData.Add("222",new missionCell(223,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1007",1,75,"3043",1,0));
			this.AllData.Add("223",new missionCell(224,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1007",2,5,"3043",1,0));
			this.AllData.Add("224",new missionCell(225,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1007",3,8,"3044",1,0));
			this.AllData.Add("225",new missionCell(226,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1007",1,10,"3044",0,0));
			this.AllData.Add("226",new missionCell(227,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1008",1,25,"3044",1,0));
			this.AllData.Add("227",new missionCell(228,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1008",2,3,"3044",1,0));
			this.AllData.Add("228",new missionCell(229,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1008",3,3,"3044",1,0));
			this.AllData.Add("229",new missionCell(230,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1008",1,10,"3045",0,0));
			this.AllData.Add("230",new missionCell(231,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1016",1,500,"3045",1,0));
			this.AllData.Add("231",new missionCell(232,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1016",2,15,"3045",1,0));
			this.AllData.Add("232",new missionCell(233,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1016",3,41,"3045",1,0));
			this.AllData.Add("233",new missionCell(234,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1019",1,300,"3045",1,0));
			this.AllData.Add("234",new missionCell(235,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1019",2,14,"3045",1,0));
			this.AllData.Add("235",new missionCell(236,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1019",3,27,"3046",1,0));
			this.AllData.Add("236",new missionCell(237,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1007",1,250,"3046",1,0));
			this.AllData.Add("237",new missionCell(238,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1007",2,12,"3046",1,0));
			this.AllData.Add("238",new missionCell(239,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1007",3,21,"3046",1,0));
			this.AllData.Add("239",new missionCell(240,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1007",1,10,"3047",1,0));
			this.AllData.Add("240",new missionCell(241,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1009",2,2,"3047",1,0));
			this.AllData.Add("241",new missionCell(242,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1009",3,3,"3047",1,0));
			this.AllData.Add("242",new missionCell(243,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1009",1,10,"3047",0,0));
			this.AllData.Add("243",new missionCell(244,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,1000,"3047",1,0));
			this.AllData.Add("244",new missionCell(245,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,58,"3047",1,0));
			this.AllData.Add("245",new missionCell(246,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1020",1,300,"3048",1,0));
			this.AllData.Add("246",new missionCell(247,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1020",2,14,"3048",1,0));
			this.AllData.Add("247",new missionCell(248,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1020",3,26,"3048",1,0));
			this.AllData.Add("248",new missionCell(249,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1008",1,350,"3048",1,0));
			this.AllData.Add("249",new missionCell(250,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1008",2,11,"3049",1,0));
			this.AllData.Add("250",new missionCell(251,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1008",3,21,"3049",1,0));
			this.AllData.Add("251",new missionCell(252,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1018",1,700,"3049",1,0));
			this.AllData.Add("252",new missionCell(253,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1018",2,15,"3049",1,0));
			this.AllData.Add("253",new missionCell(254,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1018",3,41,"3049",1,0));
			this.AllData.Add("254",new missionCell(255,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1010",1,25,"3049",1,0));
			this.AllData.Add("255",new missionCell(256,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1010",2,3,"3049",1,0));
			this.AllData.Add("256",new missionCell(257,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1010",3,4,"3049",1,0));
			this.AllData.Add("257",new missionCell(258,"Ui_Text_96","{0}迎接{1}观光游客","Ui_Text_96",4,"1013",1,10,"3050",0,0));
			this.AllData.Add("258",new missionCell(259,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1013",1,900,"3050",1,0));
			this.AllData.Add("259",new missionCell(260,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1013",2,15,"3050",1,0));
			this.AllData.Add("260",new missionCell(261,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1012",1,1000,"3050",1,0));
			this.AllData.Add("261",new missionCell(262,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1012",2,15,"3051",1,0));
			this.AllData.Add("262",new missionCell(263,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,1100,"3055",1,0));
		}
	}
	public class missionCell
	{
		///<summary>
		///后一个任务ID
		///<summary>
		public readonly int nextid;
		///<summary>
		///任务名翻译
		///<summary>
		public readonly string nametranslate;
		///<summary>
		///名称备注
		///<summary>
		public readonly string nameremarks;
		///<summary>
		///任务描述翻译
		///<summary>
		public readonly string description;
		///<summary>
		///任务类型
		///<summary>
		public readonly int tasktype;
		///<summary>
		///任务参数1
		///<summary>
		public readonly string taskparam1;
		///<summary>
		///任务参数2
		///<summary>
		public readonly int taskparam2;
		///<summary>
		///进度要求
		///<summary>
		public readonly int need;
		///<summary>
		///任务奖励
		///<summary>
		public readonly string reward;
		///<summary>
		///是否跳转
		///<summary>
		public readonly int skip;
		///<summary>
		///场景
		///<summary>
		public readonly int scene;
		public missionCell(int nextid,string nametranslate,string nameremarks,string description,int tasktype,string taskparam1,int taskparam2,int need,string reward,int skip,int scene){
			this.nextid = nextid;
			this.nametranslate = nametranslate;
			this.nameremarks = nameremarks;
			this.description = description;
			this.tasktype = tasktype;
			this.taskparam1 = taskparam1;
			this.taskparam2 = taskparam2;
			this.need = need;
			this.reward = reward;
			this.skip = skip;
			this.scene = scene;
		}
	}
}