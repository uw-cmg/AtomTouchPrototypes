using UnityEngine;
using System.Collections;

public class O2D : Atom2D{
	public override void Awake(){
		base.Awake();
		radius = 126f;//pico meters
		if(Application.loadedLevelName == "ConnectMonsters"){
			GetComponent<SpriteRenderer>().color = normalColor;	
			pathHighlighter = transform.Find("PathHighlighter").gameObject;
			float scaledRadius = radius / 1000 * 4;
			transform.localScale = new Vector3(scaledRadius, scaledRadius, scaledRadius);
			if(gameObject.tag != "AtomScriptLoader"){
				cc = GetComponent<CircleCollider2D>();
			}
		}
	}
	// Use this for initialization
	void Start () {
		charge = -2;
		if(UIControl.self != null){
			//btn = UIControl.self.oBtn;
		}
		
		
	}
}
