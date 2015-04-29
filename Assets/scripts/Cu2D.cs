using UnityEngine;
using System.Collections;

public class Cu2D : Atom2D {

	// Use this for initialization
	void Start () {
		charge = 2;
		if(UIControl.self != null){
			btn = UIControl.self.cuBtn;
		}
		if(Application.loadedLevelName == "ConnectMonsters"){
			normalColor = new Color(236f/255f, 94f/255f, 129f/255f, 1f);
			GetComponent<SpriteRenderer>().color = normalColor;
			pathHighlighter = transform.Find("PathHighlighter").gameObject;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
