using UnityEngine;
using System.Collections;

public class InputFieldScript : MonoBehaviour {
	//instead of parsing, this makes my life easier
	public string name;
	public float amountStored;
	void Start(){
		amountStored = 0.0f;
	}
	
}
