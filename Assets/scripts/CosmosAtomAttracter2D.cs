using UnityEngine;
using System.Collections;

public class CosmosAtomAttracter2D : Atom2D {
	public bool held;
	public static CosmosAtomAttracter2D self;
	void Awake(){
		self = this;
	}
	void Start () {
		charge = 2;
		held= false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDrag(){
		UpdateAtomPositionWithMouse();
		
	}
	void UpdateAtomPositionWithMouse(){
		//new atom position this frame
		Vector2 mousePosInViewport =  Camera.main.ScreenToViewportPoint(Input.mousePosition);
		if(!Application.isMobilePlatform){
			mousePosInViewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}else{
			/*
			if(Input.touchCount == 1){
				mousePosInViewport = Camera.main.ScreenToViewportPoint(Input.GetTouch(0));
			}else{
				return;
			}
			*/
			
		}
			
		float viewportX = Mathf.Clamp(mousePosInViewport.x, 0.0f,1.0f);
		float viewportY = Mathf.Clamp(mousePosInViewport.y, 0.0f,1.0f);
		Vector2 atomPos = Camera.main.ViewportToWorldPoint(new Vector2(viewportX, viewportY));
		//check if hits another atom
		Collider2D[] touchingOtherAtoms = Physics2D.OverlapCircleAll(
			atomPos, 
			GetComponent<CircleCollider2D>().radius,
			LayerMask.NameToLayer("Atom")
		);
		if(touchingOtherAtoms.Length > 1){
			return;
		}else if(touchingOtherAtoms.Length > 0){
			if(touchingOtherAtoms[0] != GetComponent<CircleCollider2D>())
				return;
		}
		transform.position = atomPos;
	}
}
