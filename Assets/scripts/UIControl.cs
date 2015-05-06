using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
	public static UIControl self;
	public GameObject endGamePanel;
	public GameObject tryAgainBtn;
	public GameObject nextLevelBtn;
	public GameObject atomMenuPanel;
	public Text scoreText;
	public Text scoreTextInStat;// score in the end of game stat
	public Text numberOfConnections;//in end of game stat
	public Text numberOfUsedAtomsText;//in end of game stat

	[HideInInspector]public Button cuBtn;
	[HideInInspector]public Button naBtn;
	[HideInInspector]public Button clBtn;
	[HideInInspector]public Button oBtn;
	[HideInInspector]public Button alBtn;

	[HideInInspector]public Text cuBtnText;
	[HideInInspector]public Text naBtnText;
	[HideInInspector]public Text clBtnText;
	[HideInInspector]public Text oBtnText;
	[HideInInspector]public Text alBtnText;
	public Text endGameText;
	public Text timerText;
	void Awake(){
		self = this;
		endGamePanel.SetActive(false);
		//find buttons
		Transform atomMenuPanelTransform = atomMenuPanel.transform;
		cuBtn = atomMenuPanelTransform.Find("Cu").gameObject.GetComponent<Button>();
		naBtn = atomMenuPanelTransform.Find("Na").gameObject.GetComponent<Button>();
		clBtn = atomMenuPanelTransform.Find("Cl").gameObject.GetComponent<Button>();

		if(Application.loadedLevelName == "ConnectMonsters"){
			oBtn = atomMenuPanelTransform.Find("O").gameObject.GetComponent<Button>();
			alBtn = atomMenuPanelTransform.Find("Al").gameObject.GetComponent<Button>();
		}
	}
	// Use this for initialization
	void Start () {
		if(Application.loadedLevelName == "ConnectMonsters"){
			cuBtnText = cuBtn.GetComponentInChildren<Text>();
			naBtnText = naBtn.GetComponentInChildren<Text>();
			clBtnText = clBtn.GetComponentInChildren<Text>();
			oBtnText = oBtn.GetComponentInChildren<Text>();
			alBtnText = alBtn.GetComponentInChildren<Text>();

			cuBtnText.text = "Cu (" + AtomStaticData.CuRemainingStock + ")";
			naBtnText.text = "Na (" + AtomStaticData.NaRemainingStock + ")";
			clBtnText.text = "Cl (" + AtomStaticData.ClRemainingStock + ")";
			oBtnText.text = "O (" + AtomStaticData.ORemainingStock + ")";
			alBtnText.text = "Al (" + AtomStaticData.AlRemainingStock + ")";
		}
	}
	//for connect monsters
	public void OnGameEnded(){
		//update game stats
		scoreTextInStat.text = GameControl2D.self.score.ToString("0.0");
		numberOfConnections.text 
			= MonsterAtomManager.self.totalConnections + "/" 
			+ MonsterAtomManager.self.atomConnections.Count; 
		numberOfUsedAtomsText.text = GameControl2D.self.totalAtomsUsed.ToString("0");

		endGamePanel.SetActive(true);
	}
	public void OnHoverConnectionEntry(MonsterAtomConnection mac){
		mac.ShowPath();
		//mac.start.GetComponent<SpriteRenderer>().color = Color.white;
		//mac.end.GetComponent<SpriteRenderer>().color = Color.white;
	}
	public void OnLeaveConnectionEntry(MonsterAtomConnection mac){
		mac.HidePath();
		//mac.start.GetComponent<SpriteRenderer>().color = mac.start.normalColor;
		//mac.end.GetComponent<SpriteRenderer>().color = mac.end.normalColor;
	}
	public void UpdateTimer(float timeRemaining){
		timerText.text = Mathf.Max(0.0f, timeRemaining).ToString("0.0");
	}

	public void OnAddAtom(GameObject prefab){
		//fade that atom button
		//DisableAtomBtn(prefab.GetComponent<Atom2D>());
		GameControl2D.self.CreateAtom(prefab);

	}
	public void EnableAtomBtns(bool enable = true){
		if(Application.loadedLevelName != "ConnectMonsters"){
			cuBtn.interactable = enable;
			naBtn.interactable = enable;
			clBtn.interactable = enable;
			//oBtn.interactable = enable; //uncomment this only if other scenes have o btn too
		}else{
			if(GameControl2D.self.gameState == (int)GameControl2D.GameState.AddingAtom){
				cuBtn.interactable = false;
				naBtn.interactable = false;
				clBtn.interactable = false;
				oBtn.interactable = false;
				alBtn.interactable = false;
				return;
			}
			if(AtomStaticData.CuRemainingStock <= 0){
				cuBtn.interactable = false;
			}else{
				cuBtn.interactable = true;
			}

			if(AtomStaticData.NaRemainingStock <= 0){
				naBtn.interactable = false;
			}else{
				naBtn.interactable = true;
			}

			if(AtomStaticData.ClRemainingStock <= 0){
				clBtn.interactable = false;
			}else{
				clBtn.interactable = true;
			}

			if(AtomStaticData.ORemainingStock <= 0){
				oBtn.interactable = false;
			}else{
				oBtn.interactable = true;
			}
			if(AtomStaticData.AlRemainingStock <= 0){
				alBtn.interactable = false;
			}else{
				alBtn.interactable = true;
			}
		}
		
	}
	public void UpdateScore(){
		scoreText.text = GameControl2D.self.score.ToString("0.0");
	}
	public void UpdateAtomBtnWithStock(Atom2D newlyAddedAtom){
		if(Application.loadedLevelName == "ConnectMonsters"){

			//update button text with name + remaining stock
			if(newlyAddedAtom is Cu2D){
				cuBtnText.text = "Cu (" + AtomStaticData.CuRemainingStock+ ")";
				if(AtomStaticData.CuRemainingStock <= 0){
					cuBtn.interactable = false;
				}
			}else if (newlyAddedAtom is Na2D){
				naBtnText.text = "Na (" + AtomStaticData.NaRemainingStock + ")";
				if(AtomStaticData.NaRemainingStock <= 0){
					naBtn.interactable = false;
				}
			}else if (newlyAddedAtom is Cl2D){
				clBtnText.text = "Cl (" + AtomStaticData.ClRemainingStock + ")";
				if(AtomStaticData.ClRemainingStock <= 0){
					clBtn.interactable = false;
				}
			}else if (newlyAddedAtom is O2D){
				oBtnText.text = "O (" + AtomStaticData.ORemainingStock + ")";
				if(AtomStaticData.ORemainingStock <= 0){
					oBtn.interactable = false;
				}
			}else if(newlyAddedAtom is Al2D){
				alBtnText.text = "Al (" + AtomStaticData.AlRemainingStock + ")";
				if(AtomStaticData.AlRemainingStock <= 0){
					alBtn.interactable = false;
				}
			}

		}
	}
	public void OnClickAtomBtn(){
		EnableAtomBtns(false);
	}
	//for gooey
	public void EndGame(bool win){
		if(win){
			endGameText.text = "You Won nyo >w<!";
			tryAgainBtn.SetActive(false);
			nextLevelBtn.SetActive(true);
		}else{
			endGameText.text = "You Lost nio QvQ!";
			tryAgainBtn.SetActive(true);
			nextLevelBtn.SetActive(false);
		}
		
		endGamePanel.SetActive(true);
	}
	public void OnClickQuit(){
		Application.Quit();
	}
	public void OnClickTryAgain(){
		Application.LoadLevel("main");
	}

}
