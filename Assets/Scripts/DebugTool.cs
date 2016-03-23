using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DebugTool : MonoBehaviour {
	private List<string> debugMessageList;
	public bool MessageCollectEnabled=false;
	public List<MessageShowText> m_MessageShowTextList;
	private static DebugTool m_instance;
	public static DebugTool Instance{
		get{return m_instance;}
	}
	void Awake(){
		m_instance = this;
	}
	void Start(){
		debugMessageList = new List<string> ();
		m_MessageShowTextList = new List<MessageShowText> ();
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
		if (MessageCollectEnabled) {
			debugMessageList.Add (str);
			showAllMessage ();
		}
			
		Debug.Log (str);
	}
	public void clear(){
		MessageCollectEnabled = false;
		debugMessageList.Clear ();
		Log ("DebugTool stop");
	}
	public void showAllMessage(){
		foreach (MessageShowText mst in m_MessageShowTextList) {
			foreach (string str in debugMessageList) {
				mst.addMessage (str);
			}
			mst.showAndClear ();
		}
	}
	public void addMessageText(MessageShowText mst){
		m_MessageShowTextList.Add (mst);
	}
	public void removeMessageText(MessageShowText mst){
		m_MessageShowTextList.Remove (mst);
	}
}
