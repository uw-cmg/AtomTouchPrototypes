using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIControllerGuess2D : MonoBehaviour {
	public static UIControllerGuess2D self;
	public Text timerText; 
	public Text remainingWaitTimeText;
	public GameObject timerPanel;
	public GameObject waitTimePanel;

	//called when game state switches happen
	public void ToggleTimerPanels(int nextState){
		if(nextState == (int)GameControlGuessAtoms2D.State.UpdatingAtomPositions){
			timerPanel.SetActive(false);
			waitTimePanel.SetActive(true);
			
		}else if(nextState == (int)GameControlGuessAtoms2D.State.PlayerGuessing){
			timerPanel.SetActive(true);
			waitTimePanel.SetActive(false);
		}
	}
	public void UpdateWaitTime(){
		float t = Mathf.Max(0.0f, GameControlGuessAtoms2D.self.remainingWaitingTime);
		remainingWaitTimeText.text = t.ToString("0");
	}
	public void UpdateTimer(){
		float t = Mathf.Max(0.0f, GameControlGuessAtoms2D.self.remainingGuessTime);
		timerText.text = t.ToString("0.0");
	}
	void Awake(){
		self = this;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
