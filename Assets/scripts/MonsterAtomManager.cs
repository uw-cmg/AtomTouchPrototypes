using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterAtomManager : MonoBehaviour {
	public static MonsterAtomManager self;
	public List<MonsterAtom2D> monsterAnchors;
	public List<MonsterAtomConnection> atomConnections;
	private Stack<Atom2D> atomPath;
	void Awake(){
		self = this;
		atomConnections = new List<MonsterAtomConnection>();
		monsterAnchors = new List<MonsterAtom2D>();
		atomPath = new Stack<Atom2D>();
		Debug.Log("atom path init");
		GameObject[] monsterAnchorArr = GameObject.FindGameObjectsWithTag("MonsterAnchor");
		foreach(GameObject g in monsterAnchorArr){
			monsterAnchors.Add(g.GetComponent<MonsterAtom2D>());
		}
		GameObject[] connectionObjs = GameObject.FindGameObjectsWithTag("MonsterAtomConnection");
		foreach(GameObject g in connectionObjs){
			atomConnections.Add(g.GetComponent<MonsterAtomConnection>());
		}
	}
	void CheckMonsterAtomConnections(){
		foreach(MonsterAtomConnection mac in atomConnections){
			Atom2D maStart = mac.start;
			Atom2D maEnd = mac.end;
			//clear visited flags
			foreach(MonsterAtom2D ma2D in monsterAnchors){
				ma2D.visited = false;
			}
			foreach(GameObject atom in AtomPhysicsWithMonsters.self.Ions){
				atom.GetComponent<Atom2D>().visited = false;
			}
			//DFS
			bool areConnected = BacktrackMonstersMonsters(ref maStart, ref maEnd);
			if(areConnected){
				Debug.DrawLine(maStart.transform.position, maEnd.transform.position,
					Color.white, 2.0f);
				Debug.Log(maStart.gameObject.name + " and " + maEnd.gameObject.name + " are connected!");
				
				while(atomPath.Count > 0){
					Atom2D atomNode = atomPath.Pop();
					mac.path.Push(atomNode);
					atomNode.GetComponent<SpriteRenderer>().color = atomNode.pathColor;
				}
			}else{
				//no path, clear last path,restore normal atom colors
				while(mac.path.Count > 0){
					Atom2D node = mac.path.Pop();
					node.GetComponent<SpriteRenderer>().color = node.normalColor;
				}
			}
		}
	}
	//dfs with backtracking wrapper
	bool BacktrackMonstersMonsters(ref Atom2D maStart, ref Atom2D maEnd){
		atomPath.Clear();
		atomPath.Push(maStart);
		return MonstersAreConnected(ref maStart, ref maEnd);
	}
	//Awwww sweet DFS, for real
	bool MonstersAreConnected(ref Atom2D maStart, ref Atom2D maEnd){
		if(maStart == maEnd){
			atomPath.Push(maEnd);
			return true;
		}
		maStart.visited = true;

		//Queue<Atom2D> path = new Queue<Atom2D>();
		foreach(Atom2D neighbour in maStart.neighbours){
			Atom2D neighbourRef = neighbour;

			if(!neighbourRef.visited){
				atomPath.Push(neighbourRef);
				if(MonstersAreConnected(ref neighbourRef, ref maEnd)){
					return true;
				}
			}
		}
		if(atomPath.Count > 0)
			atomPath.Pop();
		return false;

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckMonsterAtomConnections();
	}
}
