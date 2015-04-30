using UnityEngine;
using System.Collections;

public class Na2D : Atom2D{
	
	// Use this for initialization
	void Start () {
		charge = 1;
		if(UIControl.self != null){
			btn = UIControl.self.clBtn;
		}
		if(Application.loadedLevelName == "ConnectMonsters"){
			GetComponent<SpriteRenderer>().color = normalColor;	
			pathHighlighter = transform.Find("PathHighlighter").gameObject;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
