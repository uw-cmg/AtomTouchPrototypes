using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//for cosmos riff and connect monsters
public class MonsterAtom2D : Atom2D {
	
	void Awake(){
		base.Awake();
		Atom2D[] atomComps = gameObject.GetComponents<Atom2D>();
		if(atomComps.Length == 1){
			if(charge == 0){
				charge = 6;
			}
		}

		if(Application.loadedLevelName == "ConnectMonsters"){
			InitColorsByCharge();
			//if is not anchorable (aka. fixed monster)
			if(GetComponent<AnchorAtom>() == null){
				GetComponent<SpriteRenderer>().color = normalColor;
			}
			pathHighlighter = transform.Find("PathHighlighter").gameObject;
			
		}
	}
	public void InitColorsByCharge(){
		if(charge > 0){
			normalColor = new Color(67f/255f, 70f/255f, 130f/255f, 1f);
			highlightColor = new Color(131f/255f, 138f/255f, 255f/255f, 1f);
		}else{
			normalColor = new Color(195f/255f, 174f/255f, 140f/255f, 1f);
			highlightColor = new Color(255f/255f, 228f/255f, 183f/255f, 1f);
		}
	}
	// Use this for initialization
	void Start () {
		//base.Start();
		
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
