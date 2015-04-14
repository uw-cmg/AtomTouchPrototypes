using UnityEngine;
using System.Collections;

public class GameControl2D : MonoBehaviour {
	public static GameControl2D self;
	public GameObject atomToAdd;
	public float timeAllowed;
	public float timeRemaining;
	public int gameState;
	public enum GameState{
		Running,
		AddingAtom,
		Win,
		Lose
	};
	void Awake(){
		self = this;
		timeAllowed = 60.0f;
	}
	// Use this for initialization
	void Start () {
		timeRemaining = timeAllowed;
		gameState = (int)GameState.Running;
	}
	
	void Update(){
		timeRemaining -= Time.deltaTime;
		UIControl.self.UpdateTimer(timeRemaining);
		if(timeRemaining <= 0){
			UIControl.self.EndGame(false);
		}

		if(gameState == (int)GameState.Running){
			
		}else if(gameState == (int)GameState.AddingAtom){
			UpdateAtomPositionWithMouse();

			atomToAdd.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			if(!Application.isMobilePlatform){
				if(Input.GetMouseButtonDown(0)){
					FinishAddingAtom();
					gameState = (int)GameState.Running;
				}
			}else{
				if(Input.GetMouseButtonUp(0)){
					FinishAddingAtom();
					gameState = (int)GameState.Running;
				}
			}
			
			
			//if on mobile: mouse up
			
		}
		
		
		
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
			atomToAdd.GetComponent<CircleCollider2D>().radius
		);
		if(touchingOtherAtoms.Length > 1){
			return;
		}else if(touchingOtherAtoms.Length > 0){
			if(touchingOtherAtoms[0] != atomToAdd.GetComponent<CircleCollider2D>())
				return;
		}
		atomToAdd.transform.position = atomPos;
	}
	//register atom and stuff
	void FinishAddingAtom(){
		atomToAdd.name = "Atom" + atomToAdd.GetInstanceID().ToString();
		//add to NaCl list
		Atom2D atom = atomToAdd.GetComponent<Atom2D>();
		atom.Kick();
		AtomPhysics2D.self.Ions.Add(atomToAdd);
		UIControl.self.EnableAtomBtns();
	}
	public void CreateAtom(GameObject prefab){
		Debug.Log("creating atom");
		Vector2 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Quaternion curRotation = Quaternion.Euler(0, 0, 0);
		GameObject atom = Instantiate(prefab, spawnPos, curRotation) as GameObject;
		atom.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		atom.GetComponent<Rigidbody2D>().isKinematic = false;
		SetGameStateAddingAtom(atom);

	}
	public void SetGameStateAddingAtom(GameObject atom){
		atomToAdd = atom;
		gameState = (int)GameState.AddingAtom;
	}
}
