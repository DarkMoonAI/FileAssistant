using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class MessageShowText : MonoBehaviour {
	private string m_Message;
	private Text text;
	void Awake(){
		if (DebugTool.Instance!=null) {
			DebugTool.Instance.addMessageText (this);
		}
	}
	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void addMessage(string str){
		m_Message = m_Message+" \n"+str;
	}
	public void showAndClear(){
		text.text = m_Message;
		m_Message = "";
	}
	void OnDisable(){
		if (DebugTool.Instance!=null) {
			DebugTool.Instance.removeMessageText (this);
		}
	}
}
