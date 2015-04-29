using UnityEngine;
using System.Collections;

public class Cl2D : Atom2D {

	// Use this for initialization
	void Start () {
		charge = -1;
		if(UIControl.self != null){
			btn = UIControl.self.clBtn;
		}
		if(Application.loadedLevelName == "ConnectMonsters"){
			normalColor = new Color(94f/255f, 138f/255f, 99f/255f, 1f);
			GetComponent<SpriteRenderer>().color = normalColor;
		}
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
