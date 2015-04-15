using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//game control for prototype #8
public class GameControlGuessAtoms2D : MonoBehaviour {
	public static GameControlGuessAtoms2D self;
	public int state;
	public float allowedGuessTime; //in seconds
	public float remainingGuessTime;
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

	}
	// Use this for initialization
	void Start () {
		//init
		state = (int)State.PlayerGuessing;
		remainingGuessTime = allowedGuessTime;
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
	GameObject CreateAtomAtExpectedPosition(Vector2 pos){
		return null;
	}
	void CreateConnectingArrow(Vector2 pos){

	}
	//raycast to see if mouse clicked on a target
	void CheckMouseClick(){
		if(!Input.GetMouseButtonDown(0))return;

		Vector2 mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if(clickedTarget != null){
			//draw transparent atom
			clickedTarget.expectedNextPeriod = CreateAtomAtExpectedPosition(mouseInWorld);
			//draw arrow
			CreateConnectingArrow(mouseInWorld);
			clickedTarget = null;
			return;
		}
		
		RaycastHit2D hitInfo;
		hitInfo = Physics2D.Raycast(
			mouseInWorld, 
			Camera.main.gameObject.transform.forward, 
			1.0f
			);
		//if does not hit anything
		if(hitInfo.collider == null)return;
		GameObject hit = hitInfo.collider.gameObject;
		AtomGuess2D atom = hit.GetComponent<AtomGuess2D>();
		//if hit is not an atom
		if(atom == null)return;
		//update clickedTarget
		if(atom is AtomGuessTarget2D){
			clickedTarget = (AtomGuessTarget2D)atom;
			return;
		}
	}
}
