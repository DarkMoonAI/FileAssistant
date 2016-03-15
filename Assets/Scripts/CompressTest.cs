using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;
using SevenZip.Compression.LZMA;
using System;
public class CompressTest : MonoBehaviour {
	public Button btn_compress;
	public Button btn_decompress;
	// Use this for initialization
	void Start () {
		UnityAction btnAction = new UnityAction (OnClick_compress);
		UnityAction btnAction1 = new UnityAction (OnClick_decompress);
		btn_compress.onClick.AddListener(btnAction);
		btn_decompress.onClick.AddListener(btnAction1);
	}
	void OnClick_decompress(){
		DecompressFileLZMA (Application.dataPath+"/test.zip",Application.dataPath+"/test.txt");
	}
	void OnClick_compress(){
		CompressFileLZMA (Application.dataPath+"/test.txt",Application.dataPath+"/test.zip");
	}
	private static void CompressFileLZMA(string inFile, string outFile)
	{
		SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
		FileStream input = new FileStream(inFile, FileMode.Open);
		FileStream output = new FileStream(outFile, FileMode.Create);

		// Write the encoder properties
		coder.WriteCoderProperties(output);

		// Write the decompressed file size.
		output.Write(BitConverter.GetBytes(input.Length), 0, 8);

		// Encode the file.
		coder.Code(input, output, input.Length, -1, null);
		output.Flush();
		output.Close();
		input.Close();
	}
	private static void DecompressFileLZMA(string inFile, string outFile)
	{
		SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
		FileStream input = new FileStream(inFile, FileMode.Open);
		FileStream output = new FileStream(outFile, FileMode.Create);

		// Read the decoder properties
		byte[] properties = new byte[5];
		input.Read(properties, 0, 5);

		// Read in the decompress file size.
		byte [] fileLengthBytes = new byte[8];
		input.Read(fileLengthBytes, 0, 8);
		long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

		// Decompress the file.
		coder.SetDecoderProperties(properties);
		coder.Code(input, output, input.Length, fileLength, null);
		output.Flush();
		output.Close();
		input.Close();
	}
}
