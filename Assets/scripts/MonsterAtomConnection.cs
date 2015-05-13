using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MonsterAtomConnection : MonoBehaviour {
	public AtomCollision start;
	public AtomCollision end;
	public List<AtomCollision> path;
	public Toggle taskToggle; //assigned in inspector

	public bool HasPath(){
		return path.Count > 0;
	}
	public void ShowPath(){
		start.GetComponent<SpriteRenderer>().color = start.GetComponent<Atom2D>().highlightColor;
		end.GetComponent<SpriteRenderer>().color = end.GetComponent<Atom2D>().highlightColor;
		foreach(AtomCollision atomNode in path){
			atomNode.GetComponent<SpriteRenderer>().color 
				= atomNode.GetComponent<Atom2D>().highlightColor;
		}
	}
	public void HidePath(){
		start.GetComponent<SpriteRenderer>().color = start.GetComponent<Atom2D>().normalColor;
		end.GetComponent<SpriteRenderer>().color = end.GetComponent<Atom2D>().normalColor;
		foreach(AtomCollision atomNode in path){
			atomNode.GetComponent<SpriteRenderer>().color 
				= atomNode.GetComponent<Atom2D>().normalColor;
		}
	}
	public void ClearPath(){
		while(path.Count > 0){
			AtomCollision node = path[0];
			if(node != null){
				node.GetComponent<Atom2D>().pathHighlighter.SetActive(false);
			}
			path.RemoveAt(0);
		}
	}
	// Use this for initialization
	void Start () {
		path = new List<AtomCollision>();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
