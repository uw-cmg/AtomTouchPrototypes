﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AtomUIData : MonoBehaviour {
	public string atomName;
	public int startStock;
	public int remainingStock;
	public Button btn;
	public Text btnText;

	public void CreateSelf(string name, int startStock){
		atomName = name;
		this.startStock = startStock; 
		remainingStock = this.startStock;

		btn = UIControl.self.atomMenuPanel.transform.Find(name).gameObject.GetComponent<Button>();
		btnText = btn.GetComponentInChildren<Text>();
	}
}
