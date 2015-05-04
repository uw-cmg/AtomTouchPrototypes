using UnityEngine;
using System.Collections;

public class AtomStaticData : MonoBehaviour {
	public static int CuStartStock;
	public static int NaStartStock;
	public static int ClStartStock;
	public static int OStartStock;

	public static int CuRemainingStock;
	public static int NaRemainingStock;
	public static int ClRemainingStock;
	public static int ORemainingStock;

	public static int totalRemainingStock;
	void Awake(){
		CuStartStock = 10;
		NaStartStock = 15;
		ClStartStock = 20;
		OStartStock = 10;

		CuRemainingStock = CuStartStock;
		NaRemainingStock = NaStartStock;
		ClRemainingStock = ClStartStock;
		ORemainingStock = OStartStock;
		
		totalRemainingStock = CuStartStock + NaStartStock + ClStartStock + OStartStock;
	}
}
