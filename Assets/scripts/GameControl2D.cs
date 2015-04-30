using UnityEngine;
using System.Collections;

public class GameControl2D : MonoBehaviour {
	public static GameControl2D self;
	public GameObject atomToAdd;
	public float timeAllowed;
	public float timeRemaining;
	public int gameState;
	public float score;
	public int totalAtomsUsed;

	public enum GameState{
		Running,
		AddingAtom,
		Ended,
		Win,
		Lose
	};
	void Awake(){
		self = this;
		if(Application.loadedLevelName == "main"){
			timeAllowed = 60.0f;
		}else if(Application.loadedLevelName == "ConnectMonsters"){
			timeAllowed = 180.0f;//3 minutes
		}else{
			timeAllowed = 900.0f;//5 minutes
		}
		
	}
	// Use this for initialization
	void Start () {
		score = 0.0f;
		totalAtomsUsed = 0;
		timeRemaining = timeAllowed;
		gameState = (int)GameState.Running;
	}
	
	void Update(){
		timeRemaining -= Time.deltaTime;
		UIControl.self.UpdateTimer(timeRemaining);
		if(timeRemaining <= 0){
			if(Application.loadedLevelName == "main"){
				UIControl.self.EndGame(false);
				return;
			}else if (Application.loadedLevelName == "ConnectMonsters"){
				gameState = (int)GameState.Ended;

			}
			
		}

		if(gameState == (int)GameState.Running){
			
		}else if(gameState == (int)GameState.AddingAtom){
			UpdateAtomPositionWithMouse();

			atomToAdd.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			if(!Application.isMobilePlatform){
				if(Input.GetMouseButtonDown(0)){
					FinishAddingAtom();
				}
			}else{
				if(Input.GetMouseButtonUp(0)){
					FinishAddingAtom();
				}
			}
			
			
			//if on mobile: mouse up
			
		}else if (gameState == (int)GameState.Ended){
			
			if(Time.timeScale == 0){
				return;
			}
			OnGameEnded();
			Time.timeScale = 0;
			
		}
		
		
		
	}
	public void UpdateScoreBy(float offset){
		score += offset;
		UIControl.self.UpdateScore();
	}
	//score calculation, etc
	void OnGameEnded(){
		/*
		foreach(MonsterAtomConnection mac in MonsterAtomManager.self.atomConnections){
			if(mac.HasPath()){
				score += 100.0f;
			}
		}
		*/
		//check total number of atoms used
		MonsterAtomManager.self.EndAllCoroutines();
		score += 100.0f;
		score -= 5.0f * totalAtomsUsed;
		UIControl.self.UpdateScore();
		
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
		//for diff prototypes
		if(AtomPhysics2D.self != null){
			AtomPhysics2D.self.Ions.Add(atomToAdd);
		}else if (AtomPhysicsWithMonsters.self != null){
			AtomPhysicsWithMonsters.self.Ions.Add(atomToAdd);
		}
		if(Application.loadedLevelName == "ConnectMonsters"){
			//Atom2D.remainingStock -= 1;
			if(atom is Cu2D){
				AtomStaticData.CuRemainingStock -= 1;
				AtomStaticData.totalRemainingStock -= 1;
			}else if(atom is Na2D){
				AtomStaticData.NaRemainingStock -= 1;
				AtomStaticData.totalRemainingStock -= 1;
			}else if(atom is Cl2D){
				AtomStaticData.ClRemainingStock -= 1;
				AtomStaticData.totalRemainingStock -= 1;
			}
		}
		UIControl.self.EnableAtomBtns();
		UIControl.self.UpdateAtomBtnWithStock(atom);
		if(Application.loadedLevelName == "ConnectMonsters"){
			if(AtomStaticData.totalRemainingStock <= 0){
				//end of game
				gameState = (int)GameState.Ended;
			}else{
				gameState = (int)GameState.Running;
			}
		}else{
			gameState = (int)GameState.Running;
		}
		
	}
	public void CreateAtom(GameObject prefab){
		//Debug.Log("creating atom");
		Vector2 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Quaternion curRotation = Quaternion.Euler(0, 0, 0);
		GameObject atom = Instantiate(prefab, spawnPos, curRotation) as GameObject;
		atom.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		atom.GetComponent<Rigidbody2D>().isKinematic = false;
		SetGameStateAddingAtom(atom);
		totalAtomsUsed += 1;

	}
	public void SetGameStateAddingAtom(GameObject atom){
		atomToAdd = atom;
		gameState = (int)GameState.AddingAtom;
	}
}
