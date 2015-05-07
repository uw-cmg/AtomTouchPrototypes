using UnityEngine;
using System.Collections;

public class AnchorAtom : MonoBehaviour {
	public float lastClickTime;
	public bool clickIsForAddingAtom;
	// Use this for initialization
	void Start () {
		clickIsForAddingAtom = true;
		lastClickTime = -1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		if(clickIsForAddingAtom){
			clickIsForAddingAtom = false;
			lastClickTime = -1.0f;
		}else if(lastClickTime <= 0){
			lastClickTime = Time.time;
		}else if(Time.time - lastClickTime < 0.3f){
			Debug.Log("double click");
			ToggleAnchor();
			lastClickTime = -1.0f;
		}
		
	}
	void ToggleAnchor(){
		if(gameObject.tag == "Atom"){
			gameObject.tag = "AnchorAtom";
			//AtomPhysics2D.self.Ions.Remove(gameObject);
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().isKinematic = true;
		}else if(gameObject.tag == "AnchorAtom"){
			gameObject.tag = "Atom";
			//AtomPhysics2D.self.Ions.Add(gameObject);
			GetComponent<Rigidbody2D>().isKinematic = false;
		}	
	}
}
