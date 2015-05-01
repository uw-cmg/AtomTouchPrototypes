using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	public float period;
	private float timeInPeriod;
	void Awake(){
		period = 8.0f;
	}
	// Use this for initialization
	void Start () {
		timeInPeriod = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		MoveUpDown();
	}	
	void MoveUpDown(){
		timeInPeriod += Time.deltaTime;
		if(timeInPeriod >= period){
			timeInPeriod = 0.0f;
		}
		float radian = timeInPeriod* (2*Mathf.PI/period);
		transform.position = new Vector3(transform.position.x, 5*Mathf.Sin(radian));
	}
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.name != "targetAtomNonAnchor"){
			return;
		}
		//win
		GameControl2D.self.gameState = (int)GameControl2D.GameState.Ended;
		UIControl.self.EndGame(true);
	}
}
