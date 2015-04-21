using UnityEngine;
using System.Collections;

public class UIControlSandbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickReactor(GameObject prefab){
		Player.self.holding = prefab;
	}
}
