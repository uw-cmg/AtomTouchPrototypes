﻿using UnityEngine;
using System.Collections;

public class Cu2D : Atom2D {
	public override void Awake(){
		base.Awake();
		if(Application.loadedLevelName == "ConnectMonsters"){
			GetComponent<SpriteRenderer>().color = normalColor;
			pathHighlighter = transform.Find("PathHighlighter").gameObject;
		}
	}
	// Use this for initialization
	void Start () {
		charge = 2;
		if(UIControl.self != null){
			btn = UIControl.self.cuBtn;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
