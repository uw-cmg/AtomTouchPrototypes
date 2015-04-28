using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterAtomManager : MonoBehaviour {
	public static MonsterAtomManager self;
	public List<MonsterAtom2D> monsterAnchors;
	public List<MonsterAtomConnection> atomConnections;

	void Awake(){
		self = this;
		atomConnections = new List<MonsterAtomConnection>();
		monsterAnchors = new List<MonsterAtom2D>();
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
			bool areConnected = MonstersAreConnected(ref maStart, ref maEnd);
			if(areConnected){
				Debug.DrawLine(maStart.transform.position, maEnd.transform.position,
					Color.white, 2.0f);
				Debug.Log(maStart.gameObject.name + " and " + maEnd.gameObject.name + " are connected!");
			}
			

		}
	}
	//DFS
	bool MonstersAreConnected(ref Atom2D maStart, ref Atom2D maEnd){
		if(maStart == maEnd)return true;
		maStart.visited = true;
		foreach(Atom2D neighbour in maStart.neighbours){
			Atom2D neighbourRef = neighbour;
			if(!neighbour.visited){
				if(MonstersAreConnected(ref neighbourRef, ref maEnd)){
					return true;
				}
			}
		}
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
