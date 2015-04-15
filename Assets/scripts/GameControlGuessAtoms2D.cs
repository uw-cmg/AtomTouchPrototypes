using UnityEngine;
using System.Collections;

public class GameControlGuessAtoms2D : MonoBehaviour {
	public static GameControlGuessAtoms2D self;
	public int state;
	public enum State{
		PlayerGuessing,
		UpdatingAtomPositions
	};
	void Awake(){
		self = this;
	}
	// Use this for initialization
	void Start () {
		//init
		state = (int)State.PlayerGuessing;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
