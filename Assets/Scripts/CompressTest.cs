using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;
using SevenZip.Compression.LZMA;
using System;
using ICSharpCode.SharpZipLib.Checksums;  
using ICSharpCode.SharpZipLib.Zip;
public class CompressTest : MonoBehaviour {
	public Button btn_compress;
	public Button btn_decompress;
	public compressOrDecompressTargetClass compressTarget;
	public compressOrDecompressTargetClass decompressTarget;
	// Use this for initialization
	void Start () {
		UnityAction btnAction = new UnityAction (OnClick_compress);
		UnityAction btnAction1 = new UnityAction (OnClick_decompress);
		btn_compress.onClick.AddListener(btnAction);
		btn_decompress.onClick.AddListener(btnAction1);
	}
	void OnClick_decompress(){
		DecompressFileGZIP (Application.dataPath + decompressTarget.targetName, Application.dataPath + decompressTarget.resultName);
	}
	void OnClick_compress(){
		CompressFileGZIP (Application.dataPath + compressTarget.targetName, Application.dataPath + compressTarget.resultName,1);
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