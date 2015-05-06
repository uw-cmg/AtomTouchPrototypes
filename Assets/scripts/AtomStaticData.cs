using UnityEngine;
using System.Collections;

public class AtomStaticData : MonoBehaviour {
	public static int CuStartStock;
	public static int NaStartStock;
	public static int ClStartStock;
	public static int OStartStock;
	public static int AlStartStock;
	public static int KStartStock;
	public static int NStartStock;

	public static int CuRemainingStock;
	public static int NaRemainingStock;
	public static int ClRemainingStock;
	public static int ORemainingStock;
	public static int AlRemainingStock;
	public static int KRemainingStock;
	public static int NRemainingStock;

	public static int totalRemainingStock;
	void Awake(){
		CuStartStock = 10;
		NaStartStock = 15;
		ClStartStock = 20;
		OStartStock = 10;
		AlStartStock = 8;
		KStartStock = 8;
		NStartStock = 5;

		CuRemainingStock = CuStartStock;
		NaRemainingStock = NaStartStock;
		ClRemainingStock = ClStartStock;
		ORemainingStock = OStartStock;
		AlRemainingStock = AlStartStock;
		KRemainingStock = KStartStock;
		NRemainingStock = NStartStock;

		totalRemainingStock = CuStartStock + NaStartStock + ClStartStock 
		+ OStartStock + AlStartStock + KStartStock + NStartStock;
	}
}
