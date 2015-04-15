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
	}
	
	// Update is called once per frame
	void Update () {
		if(state == (int)State.UpdatingAtomPositions){
			//renderedAtoms <- ghostAtoms
			foreach(AtomGuessTarget2D atom in atomsToGuess){
				//show actual atom new position
				atom.GetComponent<Renderer>().enabled = true;
			}
			//calculate how accurate the guess is : distance 
			//bewtween new atom position and rendered atom position

			//wait for 3 seconds

			//hide rendered atom
			foreach(AtomGuessTarget2D atom in atomsToGuess){
				atom.renderedAtom.GetComponent<Renderer>().enabled = false;
				atom.predictedAtom.GetComponent<Renderer>().enabled = false;
			}
			if(remainingWaitingTime <= 0){
				remainingWaitingTime = maxWaitingWhenUpdate;
				state = (int)State.PlayerGuessing;
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
		if(!Input.GetMouseButton(0))return;

		Vector2 mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if(clickedTarget != null){
			//draw transparent atom
			ShowPredictedAtom(mouseInWorld);
			//draw arrow
			CreateConnectingArrow(mouseInWorld);
			clickedTarget = null;
			return;
		}
		RaycastHit2D hitInfo;
		hitInfo = Physics2D.Raycast(
			mouseInWorld, 
			Camera.main.gameObject.transform.forward, 
			1.0f,
			LayerMask.NameToLayer("Atom")
			);
		//if does not hit anything
		if(hitInfo.collider == null){
			Debug.Log("no hit");
			return;
		}
		GameObject hit = hitInfo.collider.gameObject;
		Debug.Log(hit.name);
		AtomGuessTarget2D atom = hit.GetComponent<AtomGuessTarget2D>();
		//if hit is not an atom
		if(atom == null){
			Debug.Log("atom is null");
			return;
		}
		clickedTarget = (AtomGuessTarget2D)atom;
		return;
		
	}
}
