using UnityEngine;
using System.Collections;

public class O2D : Atom2D{
	public override void Awake(){
		base.Awake();
		radius = 140f;//pico meters
		SetUp();
	}
	// Use this for initialization
	void Start () {
		charge = -2;
		if(UIControl.self != null){
			//btn = UIControl.self.oBtn;
		}
		
		
	}
}
