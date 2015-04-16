using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//game control for prototype #8
public class GameControlGuessAtoms2D : MonoBehaviour {
	public static GameControlGuessAtoms2D self;
	public int state;
	public float allowedGuessTime; //in seconds
	public float remainingGuessTime;
	public float maxWaitingWhenUpdate;
	public float remainingWaitingTime;

	public AtomGuessTarget2D clickedTarget;
	//assigned in inspector
	public List<AtomGuessTarget2D> atomsToGuess;

	public enum State{
		PlayerGuessing,
		UpdatingAtomPositions
	};
	void Awake(){
		Time.timeScale = 0;
		self = this;
		allowedGuessTime = 10.0f;
		maxWaitingWhenUpdate = 3.0f;
	}
	// Use this for initialization
	void Start () {
		//init
		state = (int)State.PlayerGuessing;
		remainingGuessTime = allowedGuessTime;
		remainingWaitingTime = maxWaitingWhenUpdate;
		clickedTarget = null;
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(state == (int)State.UpdatingAtomPositions){
			
			//calculate how accurate the guess is : distance 
			//bewtween new atom position and rendered atom position

			//wait for 3 seconds

			
			if(remainingWaitingTime <= 0){
				remainingWaitingTime = maxWaitingWhenUpdate;
				state = (int)State.PlayerGuessing;
				//hide predicted atom
				foreach(AtomGuessTarget2D atom in atomsToGuess){
					atom.predictedAtom.GetComponent<Renderer>().enabled = false;
					atom.renderedPrevious.GetComponent<Renderer>().enabled = true;
				}
			}else{
				remainingWaitingTime -= Time.deltaTime;
			}
			
		}else if(state == (int)State.PlayerGuessing){
			//on click
			CheckMouseClick();
			//update remaining time
			if(remainingGuessTime <= 0){
				state = (int)State.UpdatingAtomPositions;
				remainingGuessTime = allowedGuessTime;
				//renderedAtoms <- ghostAtoms
			foreach(AtomGuessTarget2D atom in atomsToGuess){
				//show actual atom new position
				//current position becomes last position
				Vector2 oldPos = atom.renderedAtom.transform.position;
				atom.renderedPrevious.transform.position = oldPos;
				//ghost atom position becomes current position
				atom.renderedAtom.transform.position = atom.transform.position;
				atom.renderedAtom.GetComponent<Renderer>().enabled = true;
				atom.renderedPrevious.GetComponent<Renderer>().enabled = true;
			}
			
			}else{
				remainingGuessTime -= Time.deltaTime;
			}
		}
	}
	void ShowPredictedAtom(Vector2 pos){
		clickedTarget.predictedAtom.transform.position = pos;
		clickedTarget.predictedAtom.GetComponent<Renderer>().enabled = true;
	}
	void CreateConnectingArrow(Vector2 pos){

	}
	//raycast to see if mouse clicked on a target
	void CheckMouseClick(){
		if(!Input.GetMouseButtonDown(0))return;
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 9.0f;
		Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mousePos);
		
		if(clickedTarget != null){
			//draw transparent atom
			ShowPredictedAtom(mouseInWorld);
			//draw arrow
			CreateConnectingArrow(mouseInWorld);
			clickedTarget = null;
			return;
		}
		RaycastHit2D hitInfo;
		Vector3 dir = Camera.main.transform.forward;
		dir.Normalize();
		//Debug.Log(dir);
		Debug.Log(mouseInWorld);
		hitInfo = Physics2D.Raycast(
			mouseInWorld, 
			new Vector3(0,0,1),
			10.0f,
			LayerMask.GetMask("AtomRendered")
			);
		

		//if does not hit anything
		if(hitInfo.collider == null){
			Debug.Log("no hit");
			return;
		}
		GameObject hit = hitInfo.collider.gameObject;
		Debug.Log(hit.name);
		AtomGuessTarget2D atom = hit.transform.parent.gameObject.GetComponent<AtomGuessTarget2D>();
		//if hit is not an atom
		if(atom == null){
			Debug.Log("atom is null");
			return;
		}
		clickedTarget = (AtomGuessTarget2D)atom;
		return;
		
	}
}
