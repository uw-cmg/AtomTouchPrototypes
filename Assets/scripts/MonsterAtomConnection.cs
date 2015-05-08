using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MonsterAtomConnection : MonoBehaviour {
	public MonsterAtom2D start;
	public MonsterAtom2D end;
	public List<Atom2D> path;
	public Toggle taskToggle; //assigned in inspector

	public bool HasPath(){
		return path.Count > 0;
	}
	public void ShowPath(){
		start.GetComponent<SpriteRenderer>().color = start.highlightColor;
		end.GetComponent<SpriteRenderer>().color = end.highlightColor;
		foreach(Atom2D atomNode in path){
			atomNode.GetComponent<SpriteRenderer>().color = atomNode.highlightColor;
		}
	}
	public void HidePath(){
		start.GetComponent<SpriteRenderer>().color = start.normalColor;
		end.GetComponent<SpriteRenderer>().color = end.normalColor;
		foreach(Atom2D atomNode in path){
			atomNode.GetComponent<SpriteRenderer>().color = atomNode.normalColor;
		}
	}
	public void ClearPath(){
		while(path.Count > 0){
			Atom2D node = path[0];
			if(node != null){
				node.pathHighlighter.SetActive(false);
			}
			path.RemoveAt(0);
		}
	}
	// Use this for initialization
	void Start () {
		path = new List<Atom2D>();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
