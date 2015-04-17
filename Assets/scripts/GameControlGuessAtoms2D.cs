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
	public float score;
	public static bool started = false;
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
		allowedGuessTime = 5.0f;
		maxWaitingWhenUpdate = 3.0f;
	}
	// Use this for initialization
	void Start () {
		//init
		state = (int)State.PlayerGuessing;
		UIControllerGuess2D.self.ToggleTimerPanels((int)State.PlayerGuessing);
		remainingGuessTime = allowedGuessTime;
		remainingWaitingTime = maxWaitingWhenUpdate;
		clickedTarget = null;
		score = 0.0f;
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.005f;
	}
	public void CalcScore(){
		foreach(AtomGuessTarget2D atom in atomsToGuess){
			float dist = Vector3.Distance(
				atom.renderedAtom.transform.position,
				atom.predictedAtom.transform.position
			);
			dist = Mathf.Max(dist, 0.001f);
			score += 1f/dist;
		}
	}
	// Update is called once per frame
	void Update () {
		if(state == (int)State.UpdatingAtomPositions){
			
			//calculate how accurate the guess is : distance 
			//bewtween new atom position and rendered atom position

			//wait for 3 seconds

			UIControllerGuess2D.self.UpdateWaitTime();
			if(remainingWaitingTime <= 0){
				remainingWaitingTime = maxWaitingWhenUpdate;
				state = (int)State.PlayerGuessing;
				UIControllerGuess2D.self.ToggleTimerPanels((int)State.PlayerGuessing);
				//hide predicted atom. previous positions, and arrows
				foreach(AtomGuessTarget2D atom in atomsToGuess){
					atom.predictedAtom.GetComponent<Renderer>().enabled = false;
					atom.renderedPrevious.GetComponent<Renderer>().enabled = false;
					atom.arrow.SetActive(false);
				}

			}else{
				remainingWaitingTime -= Time.deltaTime;
			}
			
		}else if(state == (int)State.PlayerGuessing){
			//on click
			CheckMouseClick();
			UIControllerGuess2D.self.UpdateTimer();
			//update remaining time
			if(remainingGuessTime <= 0){
				state = (int)State.UpdatingAtomPositions;
				UIControllerGuess2D.self.ToggleTimerPanels((int)State.UpdatingAtomPositions);

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
					atom.arrow.SetActive(true);
					//draw arrow
					atom.ConnectArrow();
				}
				foreach(GameObject g in AtomPhysicsGuessGame2D.self.Ions){
					AtomGuess2D atom = g.GetComponent<AtomGuess2D>();
					if(atom is AtomGuessTarget2D)continue;
					atom.renderedObj.transform.position = g.transform.position;	
				}
				CalcScore();
				UIControllerGuess2D.self.UpdateScore();
			}else{
				remainingGuessTime -= Time.deltaTime;
			}
		}
		started = true;
	}
	void ShowPredictedAtom(Vector2 pos){
		clickedTarget.predictedAtom.transform.position = pos;
		clickedTarget.predictedAtom.GetComponent<Renderer>().enabled = true;
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
		AtomGuessTarget2D atom = hit.GetComponent<RenderedAtomGuess2D>().guessTarget;
		//if hit is not an atom
		if(atom == null){
			Debug.Log("atom is null");
			return;
		}
		clickedTarget = (AtomGuessTarget2D)atom;
		return;
		
	}
}
