using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public static Player self;
	public GameObject holding;
	void Awake(){
		self = this;
	}
	// Use this for initialization
	void Start () {
		holding = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(holding != null){
			//is reactor
			if(holding.GetComponent<Reactor>() != null){
				//handle mouse lick
				//need to click in empty space
				HandleAddingFacility(null);
			}else if(holding.GetComponent<Collector>() != null){
				//need to click on a reactor
				//HandleAddingFacility()
			}
		}
	}

	void HandleAddingFacility(GameObject raycastObject){

	}
}
