using UnityEngine;
using System.Collections;

public class UIImageForScroll : MonoBehaviour {
	public voidDelegate onValueChangeDelegate;
	public int flag;
	// Use this for initialization
	void Start () {
	
	}
	public void onValueChange(){
		if (onValueChangeDelegate != null) {
			onValueChangeDelegate ();
		}
		DebugTool.Instance.Log ("scrollItem(flag:"+flag+") has been changed");
	}
}
