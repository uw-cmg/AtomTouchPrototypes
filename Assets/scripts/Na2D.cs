using UnityEngine;
using System.Collections;

public class Na2D : Atom2D{
	public override void Awake(){
		base.Awake();
		radius = 116f;//pico meters
		SetUp();
	}
	// Use this for initialization
	void Start () {
		charge = 1;
		if(UIControl.self != null){
			btn = UIControl.self.clBtn;
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
