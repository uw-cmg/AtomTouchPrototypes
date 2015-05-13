using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterAtomManager : MonoBehaviour {
	public static MonsterAtomManager self;
	public List<AtomCollision> monsterAnchors;
	public List<MonsterAtomConnection> atomConnections;
	private Stack<AtomCollision> atomPath;
	public int totalConnections;
	void Awake(){
		self = this;
		totalConnections = 0;
		atomConnections = new List<MonsterAtomConnection>();
		monsterAnchors = new List<AtomCollision>();
		atomPath = new Stack<AtomCollision>();
		GameObject[] monsterAnchorArr = GameObject.FindGameObjectsWithTag("MonsterAnchor");
		foreach(GameObject g in monsterAnchorArr){
			monsterAnchors.Add(g.GetComponent<AtomCollision>());
		}
		GameObject[] connectionObjs = GameObject.FindGameObjectsWithTag("MonsterAtomConnection");
		foreach(GameObject g in connectionObjs){
			atomConnections.Add(g.GetComponent<MonsterAtomConnection>());
		}
	}
	IEnumerator CheckMonsterAtomConnections(){
		while(true){
			yield return new WaitForSeconds(0.04f);
			totalConnections = 0;
			foreach(MonsterAtomConnection mac in atomConnections){
				AtomCollision maStart = mac.start;
				AtomCollision maEnd = mac.end;
				//clear visited flags
				foreach(AtomCollision ma2D in monsterAnchors){
					ma2D.visitState = (int)AtomCollision.DFSState.unvisited;
				}
				foreach(GameObject atom in AtomPhysicsWithMonsters.self.Ions){
					atom.GetComponent<AtomCollision>().visitState 
						= (int)AtomCollision.DFSState.unvisited;
				}
				//DFS
				bool areConnected = BacktrackMonstersMonsters(ref maStart, ref maEnd);
				if(areConnected){
					/*
					Debug.DrawLine(maStart.transform.position, maEnd.transform.position,
						Color.white, 2.0f);

					Debug.Log(maStart.gameObject.name + " and " + maEnd.gameObject.name + " are connected!");
					*/
					if(!mac.HasPath()){
						//from not having path to having path, plus score
						GameControl2D.self.UpdateScoreBy(100.0f);
					}
					
					//clear old path rendering
					mac.ClearPath();
					bool pathBroken = false;
					//if has path, copy path from atom path to mac.path
					while(atomPath.Count > 0 && !pathBroken){
						AtomCollision atomNode = atomPath.Pop();
						if(atomNode == null){
							//this happens when an atom moves out of viewport
							//and has been destroyed
							//which means the path is now broken
							pathBroken = true;
						}else{
							mac.path.Add(atomNode);
							atomNode.GetComponent<Atom2D>().pathHighlighter.SetActive(true);
						}
						
					}
					if(pathBroken){
						Debug.Log("path broken");
						mac.ClearPath();
						mac.taskToggle.isOn = false;
					}else{
						mac.taskToggle.isOn = true;
						totalConnections += 1;
					}
					
				}else{
					//if had path last frame but no longer has it
					if(mac.HasPath()){
						//from having path to not having path
						//minus score
						GameControl2D.self.UpdateScoreBy(-100.0f);
						totalConnections -= 1;
					}
					//no path, clear last path,restore normal atom colors
					mac.ClearPath();
					mac.taskToggle.isOn = false;
				}
			}
			if(totalConnections >= atomConnections.Count){
				GameControl2D.self.gameState = (int)GameControl2D.GameState.Ended;
			}
		}
		
	}
	//dfs with backtracking wrapper
	bool BacktrackMonstersMonsters(ref AtomCollision maStart, ref AtomCollision maEnd){
		atomPath.Clear();
		atomPath.Push(maStart);
		return MonstersAreConnected(ref maStart, ref maEnd);
	}
	//Awwww sweet DFS with backtracking, for real
	bool MonstersAreConnected(ref AtomCollision maStart, ref AtomCollision maEnd){
		if(maStart == maEnd){
			return true;
		}
		maStart.visitState = (int)AtomCollision.DFSState.visiting;

		foreach(AtomCollision neighbour in maStart.neighbours){
			AtomCollision neighbourRef = neighbour;

			if(neighbourRef.visitState == (int)AtomCollision.DFSState.unvisited){
				atomPath.Push(neighbourRef);
				if(MonstersAreConnected(ref neighbourRef, ref maEnd)){
					return true;
				}
			}
		}
		maStart.visitState = (int)AtomCollision.DFSState.visited;
		AtomCollision curr;
		while(atomPath.Count > 0){
			curr = atomPath.Pop();
			if(curr == maStart){
				break;
			}
		}
		
		return false;

	}
	public void EndAllCoroutines(){
		StopCoroutine(CheckMonsterAtomConnections());
	}
	// Use this for initialization
	void Start () {
		StartCoroutine(CheckMonsterAtomConnections());
	}
	
}
