﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtomStaticData : MonoBehaviour {
	public static Dictionary<string, AtomUIData> AtomDataMap;
	public string[] atomNames; 
	public int[] startStocks = {3,10,5,5,7,5,7};
	public AtomUIData uiDataPrefab;

	public static int totalRemainingStock;
	public static void DecrementStock(string name){
		AtomDataMap[name].remainingStock -= 1;
		totalRemainingStock -= 1;
	}
	void Awake(){
		AtomDataMap = new Dictionary<string, AtomUIData>();
		if(Application.loadedLevelName == "ConnectMonsters"){
			atomNames = new string[]{"Al","Cl","Cu","O","N","Na","K"}; 
			if(atomNames.Length != startStocks.Length){
				Debug.Log("atomNames and startStocks should have same lengths!");
				Application.Quit();
			}
			for(int i=0; i < atomNames.Length;i++){
				string atomName = atomNames[i];
				if(!AtomDataMap.ContainsKey(atomName)){
					AtomUIData atomUIData = Instantiate(uiDataPrefab) as AtomUIData;
					atomUIData.CreateSelf(atomName, startStocks[i]);
					totalRemainingStock += startStocks[i];
					AtomDataMap.Add(atomName, atomUIData);
				}else{
					Debug.Log(atomName + " already exists!");
				}
				
			}
		}else if(Application.loadedLevelName == "main"){
			atomNames = new string[]{"Cl","Cu","Na"}; 
			totalRemainingStock = int.MaxValue;
			for(int i=0; i < atomNames.Length;i++){
				string atomName = atomNames[i];
				if(!AtomDataMap.ContainsKey(atomName)){
					AtomUIData atomUIData = Instantiate(uiDataPrefab) as AtomUIData;
					atomUIData.CreateSelf(atomName, int.MaxValue);
					
					AtomDataMap.Add(atomName, atomUIData);
				}else{
					Debug.Log(atomName + " already exists!");
				}
				
			}
		}
	}
}
