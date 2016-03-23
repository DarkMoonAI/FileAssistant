﻿using UnityEngine;
using System.Collections;
using ICSharpCode.SharpZipLib.Checksums;  
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System;
using System.Collections.Generic;  
using System.Text;
public class CompressOrDecompressTool : MonoBehaviour {
	private static CompressOrDecompressTool m_instance;
	public static CompressOrDecompressTool Instance{
		get{return m_instance;}
	}
	void Awake(){
		m_instance = this;
	}
	#region compress
	/// <summary>  
	/// 压缩单个文件  
	/// </summary>  
	/// <param name="FileToZip">被压缩的文件名称(包含文件路径)</param>  
	/// <param name="ZipedFile">压缩后的文件名称(包含文件路径)</param>  
	/// <param name="CompressionLevel">压缩率0（无压缩）-9（压缩率最高）</param>  
	/// <param name="BlockSize">缓存大小</param>  
	public bool compressFileWithGzip(string FileToZip, string ZipedFile, int CompressionLevel){
		//如果文件没有找到，则报错   
		if (!System.IO.File.Exists(FileToZip))  
		{  
			DebugTool.Instance.Log("文件：" + FileToZip + "没有找到！");
			return false;
		}  

		if (ZipedFile == string.Empty)  
		{  
			ZipedFile = Path.GetFileNameWithoutExtension(FileToZip) + ".zip";  
		}  

		if (Path.GetExtension(ZipedFile) != ".zip")  
		{  
			ZipedFile = ZipedFile + ".zip";  
		}  

		//被压缩文件名称  
		string filename = FileToZip.Substring(FileToZip.LastIndexOf("/") + 1);  

		System.IO.FileStream StreamToZip = new System.IO.FileStream(FileToZip, System.IO.FileMode.Open, System.IO.FileAccess.Read);  
		System.IO.FileStream ZipFile = System.IO.File.Open(ZipedFile,FileMode.OpenOrCreate);  
		ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);  
		ZipEntry ZipEntry = new ZipEntry(filename);  
		ZipEntry.IsUnicodeText = true;
		ZipStream.PutNextEntry(ZipEntry);  
		ZipStream.SetLevel(CompressionLevel);  
		byte[] buffer = new byte[2048];  
		System.Int32 size = StreamToZip.Read(buffer, 0, buffer.Length);  
		ZipStream.Write(buffer, 0, size);  
		try  
		{  
			while (size < StreamToZip.Length)  
			{  
				int sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);  
				ZipStream.Write(buffer, 0, sizeRead);  
				size += sizeRead;  
			}  
		}  
		catch (System.Exception ex)  
		{    
			DebugTool.Instance.Log(ex.Message);
			return false;
		}  
		finally  
		{  
			ZipStream.Finish();  
			ZipStream.Close();  
			StreamToZip.Close();  
		}  
		return true;
	}

	/// <summary>  
	/// 压缩文件夹的方法  
	/// </summary>  
	/// <param name="DirToZip">压缩目标目录</param> 
	/// <param name="ZipedFile">压缩后所得的文件</param> 
	/// <param name="CompressionLevel">压缩率0（无压缩）-9（压缩率最高）</param>
	public void ZipDir(string DirToZip, string ZipedFile, int CompressionLevel)  
	{  
		if (!System.IO.Directory.Exists (DirToZip)) {
			DebugTool.Instance.Log("文件夹：" + DirToZip + "没有找到！");
			return ;
		}
		DebugTool.Instance.Log (ZipedFile);
		//压缩文件为空时默认与压缩文件夹同一级目录  
		if (ZipedFile == string.Empty)  
		{  
			ZipedFile = DirToZip.Substring(DirToZip.LastIndexOf("/") + 1);  
			ZipedFile = DirToZip.Substring(0, DirToZip.LastIndexOf("/")) +"//"+ ZipedFile+".zip";  
		}  

		if (Path.GetExtension(ZipedFile) != ".zip")  
		{  
			ZipedFile = ZipedFile + ".zip";  
		}  

		using (ZipOutputStream zipoutputstream = new ZipOutputStream(File.Create(ZipedFile)))  
		{  
			zipoutputstream.SetLevel(CompressionLevel);  
			Crc32 crc = new Crc32();  
			Hashtable fileList = CommonTool.Instance.getAllFies(DirToZip);  
			if (fileList != null) {
				foreach (DictionaryEntry item in fileList)  
				{  
					DebugTool.Instance.Log (item.Key.ToString ());
					FileStream fs = File.OpenRead(item.Key.ToString());  
					DebugTool.Instance.Log ("s1");
					byte[] buffer = new byte[fs.Length];  
					fs.Read(buffer, 0, buffer.Length); 
					ZipEntry entry;
					int i;
					if (item.Key.ToString ().EndsWith ("\\")) {
						i = item.Key.ToString ().Substring (0, item.Key.ToString ().Length - 1).LastIndexOf ("\\");
						entry = new ZipEntry(item.Key.ToString ().Substring (i));
					}else{
						string s = item.Key.ToString ();
						i=s.LastIndexOf("\\");
						entry = new ZipEntry(item.Key.ToString().Substring(i));
					}
					//ZipEntry entry = new ZipEntry(item.Key.ToString().Substring(DirToZip.Length + 1));  
					entry.IsUnicodeText = true;
					entry.DateTime = (DateTime)item.Value;  
					entry.Size = fs.Length;  
					fs.Close();  
					crc.Reset();  
					crc.Update(buffer);  
					entry.Crc = crc.Value;  
					zipoutputstream.PutNextEntry(entry);  
					zipoutputstream.Write(buffer, 0, buffer.Length);  
				}  
			}
			zipoutputstream.Close ();
		}  
	}  


	#endregion 

	#region decompress
	// <summary>  
	/// 功能：解压zip格式的文件。  
	/// </summary>  
	/// <param name="zipFilePath">压缩文件路径</param>  
	/// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>  
	/// <param name="err">出错信息</param>  
	/// <returns>解压是否成功</returns> 
	public bool decompressFileWithGzip(string zipFilePath, string unZipDir){
		if (zipFilePath == string.Empty) {   
			DebugTool.Instance.Log ("压缩文件不能为空！");
			return false;
		}  
		if (!File.Exists (zipFilePath)) {   
			DebugTool.Instance.Log ("压缩文件不存在！");
			return false;
		}  
		//解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹  
		if (unZipDir == string.Empty)
			unZipDir = zipFilePath.Replace (Path.GetFileName (zipFilePath), Path.GetFileNameWithoutExtension (zipFilePath));  
		if (!unZipDir.EndsWith ("/"))
			unZipDir += "/";  
		if (!Directory.Exists (unZipDir))
			Directory.CreateDirectory (unZipDir);  
		using (ZipInputStream s = new ZipInputStream (File.OpenRead (zipFilePath))) {  

			ZipEntry theEntry;
			while ((theEntry = s.GetNextEntry ()) != null) {  
				string directoryName = Path.GetDirectoryName (theEntry.Name);  
				string fileName = Path.GetFileName (theEntry.Name);  
				if (directoryName.Length > 0) {  
					Directory.CreateDirectory (unZipDir + directoryName);  
				}  
				if (!directoryName.EndsWith ("/"))
					directoryName += "/";  
				if (fileName != String.Empty) {  
					using (FileStream streamWriter = File.Create (unZipDir + theEntry.Name)) {  

						int size = 2048;  
						byte[] data = new byte[2048];  
						while (true) {  
							size = s.Read (data, 0, data.Length);  
							if (size > 0) {  
								streamWriter.Write (data, 0, size);  
							} else {  
								break;  
							}  
						}  
					}  
				}  
			}
		}
		return true;
	}
	#endregion 
}
