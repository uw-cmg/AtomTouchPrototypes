using UnityEngine;
using System.Collections;

public class Condition : MonoBehaviour {
	public enum ConditionType{
		Heat,
		Catalyst
	};
	public int type;
	public Vector2 relativePosToReactor;
	// Use this for initialization
	void Awake(){
		relativePosToReactor = new Vector2(-0.05f, -1f);
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
