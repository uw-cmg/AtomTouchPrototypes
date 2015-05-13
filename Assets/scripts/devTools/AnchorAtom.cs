using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (Atom2D))]

public class AnchorAtom : MonoBehaviour {
	public float lastClickTime;
	public bool clickIsForAddingAtom;
	public Atom2D atomComp;
	public MonsterAtom2D monsterAtomComp;
	public Vector2 fixedPos;
	public bool isFixed; 
	// Use this for initialization
	void Start () {
		isFixed = false;
		clickIsForAddingAtom = true;
		lastClickTime = -1.0f;
		atomComp = GetComponent<Atom2D>();
		monsterAtomComp = gameObject.AddComponent<MonsterAtom2D>() as MonsterAtom2D;
		fixedPos = transform.position;
		monsterAtomComp.charge = atomComp.charge;
		/*
		if(atomComp.charge > 0){
			monsterAtomComp.charge = 6;
		}else{
			monsterAtomComp.charge = -6;
		}
		*/
		monsterAtomComp.InitColorsByCharge();
		monsterAtomComp.enabled = false;
		atomComp.enabled = true;

	}
	
	void Update(){
		
		if(isFixed){
			transform.position = fixedPos;
		}else{
			fixedPos = transform.position;
		}
		
	}
	void OnMouseDown(){
		if(clickIsForAddingAtom){
			clickIsForAddingAtom = false;
			lastClickTime = -1.0f;
		}else if(lastClickTime <= 0){
			lastClickTime = Time.time;
		}else{ 
			if(Time.time - lastClickTime < 0.3f){
				//Debug.Log("double click");
				ToggleAnchor();
				lastClickTime = -1.0f;
			}else{
				lastClickTime = Time.time;
			}
			
		}
		
	}
	void ToggleAnchor(){
		if(gameObject.tag == "Atom"){
			gameObject.tag = "MonsterAnchor";

			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			//GetComponent<Rigidbody2D>().isKinematic = true;
			//GetComponent<Atom2D>().totalForce = Vector2.zero;
			monsterAtomComp.enabled = true;
			atomComp.enabled = false;
			isFixed = true;
			GetComponent<SpriteRenderer>().color = monsterAtomComp.normalColor;
			
		}else if(gameObject.tag == "MonsterAnchor"){
			gameObject.tag = "Atom";

			//GetComponent<Rigidbody2D>().isKinematic = false;
			//GetComponent<Rigidbody2D>().WakeUp();
			monsterAtomComp.enabled = false;
			atomComp.enabled = true;
			isFixed = false;
			GetComponent<SpriteRenderer>().color = atomComp.normalColor;
		}	
	}
}
