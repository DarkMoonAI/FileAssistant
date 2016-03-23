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
		voidDelegateList.Add (OnClick_compressDir);
		foreach (UIButtonBase btn in btns) {
			if (btn.flag < voidDelegateList.Count) {
				btn.onClickDelegate += voidDelegateList [btn.flag];
			} else {
				DebugTool.Instance.Log (btn.name+"'s flag error");
			}
		}
		DebugTool.Instance.Log ("Init Buttons");
	}
	void DeinitButtons(){
		foreach (UIButtonBase btn in btns) {
			if (btn.flag < voidDelegateList.Count) {
				btn.onClickDelegate -= voidDelegateList [btn.flag];
			} else {
				DebugTool.Instance.Log (btn.name+"'s flag error");
			}
		}
		voidDelegateList.Clear ();
		DebugTool.Instance.Log ("Deinit Buttons");
	}
	void OnClick_compress(){
		#if UNITY_EDITOR
		CompressFileGZIP (Application.dataPath + compressTarget.targetName, Application.dataPath + compressTarget.resultName,1);
		#else
		CompressFileGZIP (Application.dataPath +"/.."+compressTarget.targetName, Application.dataPath +"/.."+ compressTarget.resultName,1);
		#endif
	}
	void OnClick_decompress(){
		#if UNITY_EDITOR
		DecompressFileGZIP (Application.dataPath + decompressTarget.targetName, Application.dataPath + decompressTarget.resultName);
		#else
		DecompressFileGZIP (Application.dataPath +"/.." + decompressTarget.targetName, Application.dataPath +"/.." + decompressTarget.resultName);
		#endif
	}
	void OnClick_compressDir(){
		#if UNITY_EDITOR
		CompressDirGZIP(Application.dataPath +compressDirTarget.targetName ,Application.dataPath +compressDirTarget.resultName,1);
		#else
		CompressDirGZIP(Application.dataPath +"/.." +compressDirTarget.targetName ,Application.dataPath +"/.."+compressDirTarget.resultName,1);
		#endif
	}
	#region GZIP
	private static void CompressFileGZIP(string FileToZip, string ZipedFile, int CompressionLevel)
	{
		CompressOrDecompressTool.Instance.compressFileWithGzip (FileToZip, ZipedFile, CompressionLevel);
	}
	private static void DecompressFileGZIP(string zipFilePath, string unZipDir){
		CompressOrDecompressTool.Instance.decompressFileWithGzip (zipFilePath,unZipDir);
	}
	private static void CompressDirGZIP(string DirToZip, string ZipedFile, int CompressionLevel){
		CompressOrDecompressTool.Instance.ZipDir (DirToZip, ZipedFile, CompressionLevel);
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