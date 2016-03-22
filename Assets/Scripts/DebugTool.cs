using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DebugTool : MonoBehaviour {
	private List<string> debugMessageList;
	public bool MessageCollectEnabled=false;
	private static DebugTool m_instance;
	public static DebugTool Instance{
		get{return m_instance;}
	}
	void Awake(){
		m_instance = this;
	}
	void Start(){
		debugMessageList = new List<string> ();
		startCollect ();
	}
	void OnDestroy(){
		clear ();
	}
	public void startCollect(){
		MessageCollectEnabled = true;
		Log ("DebugTool start");
	}
	public void stopCollect(){
		MessageCollectEnabled = false;
	}
	public void Log(string str){
		if(MessageCollectEnabled)
			debugMessageList.Add (str);
		Debug.Log (str);
	}
	public void clear(){
		MessageCollectEnabled = false;
		debugMessageList.Clear ();
		Log ("DebugTool stop");
	}
}
