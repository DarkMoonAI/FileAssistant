using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using ICSharpCode.SharpZipLib.Checksums;  
using ICSharpCode.SharpZipLib.Zip;
public class CompressTest : MonoBehaviour {
	public List<UIButtonBase> btns;
	public UIButtonBase btn_compressDir;
	public compressOrDecompressTargetClass compressTarget;
	public compressOrDecompressTargetClass decompressTarget;
	public compressOrDecompressTargetClass compressDirTarget;
	private List<voidDelegate> voidDelegateList; 
	// Use this for initialization
	void Start () {
		InitButtons ();
	}
	void OnDisable(){
		DeinitButtons ();
	}
	void InitButtons(){
		voidDelegateList = new List<voidDelegate> ();
		voidDelegateList.Add (OnClick_compress);
		voidDelegateList.Add (OnClick_decompress);
		foreach (UIButtonBase btn in btns) {
			btn.onClickDelegate += voidDelegateList [btn.flag];
		}
		Debug.Log ("Init Buttons");
	}
	void DeinitButtons(){
		foreach (UIButtonBase btn in btns) {
			btn.onClickDelegate -= voidDelegateList [btn.flag];
		}
		voidDelegateList.Clear ();
		Debug.Log ("Deinit Buttons");
	}
	void OnClick_compress(){
		CompressFileGZIP (Application.dataPath + compressTarget.targetName, Application.dataPath + compressTarget.resultName,1);
	}
	void OnClick_decompress(){
		DecompressFileGZIP (Application.dataPath + decompressTarget.targetName, Application.dataPath + decompressTarget.resultName);
	}
	#region GZIP
	private static void CompressFileGZIP(string FileToZip, string ZipedFile, int CompressionLevel)
	{
		CompressOrDecompressTool.Instance.compressFileWithGzip (FileToZip, ZipedFile, CompressionLevel);
	}
	private static void DecompressFileGZIP(string zipFilePath, string unZipDir){
		CompressOrDecompressTool.Instance.decompressFileWithGzip (zipFilePath,unZipDir);
	}
	#endregion 
}
[Serializable]
public class compressOrDecompressTargetClass{
	public string targetName;
	public string resultName;
	public bool isDecompress;
	public compressOrDecompressTargetClass(string targetName,string resultName,bool isDecompress){
		this.targetName = targetName;
		this.resultName = resultName;
		this.isDecompress = isDecompress;
	}
}