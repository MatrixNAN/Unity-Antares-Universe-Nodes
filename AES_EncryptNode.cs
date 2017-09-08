using UnityEngine;
using Antares.Vizio.Runtime;
using System.Security.Cryptography;
using System;
using System.Text;

[VisualLogicBlock("AES Encrypt", "Smart Blocks/Encryption")]
public class AES_EncryptNode : LogicBlock
{
	[Parameter(VariableType.In, typeof(string))]
	public Variable Text;
	
	[Parameter(VariableType.In, typeof(string))]
	public Variable Key;
	
	[Parameter(VariableType.Out, typeof(bool))]
	public Variable result;
	
	[Parameter(VariableType.Out, typeof(string))]
	public Variable EncryptedText;
	
	private string inDecryptedText = "";
	private string inKey = "12345678901234567890123456789012";
	
	public override void OnInitializeDefaultData()
	{
		RegisterOutputTrigger("Exit");
		result.Value = false;
		EncryptedText.Value = "";
		Key.Value = "12345678901234567890123456789012";
	}
	
	[EntryTrigger]
	public void In()
	{
		inDecryptedText = Text.Value.ToString();
		inKey = Key.Value.ToString();
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes (inKey);
		// 256-AES key
		byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes (inDecryptedText);
		RijndaelManaged rDel = new RijndaelManaged ();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		// http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
		rDel.Padding = PaddingMode.PKCS7;
		// better lang support
		ICryptoTransform cTransform = rDel.CreateEncryptor ();
		byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
		EncryptedText.Value = Convert.ToBase64String (resultArray, 0, resultArray.Length);
		
		result.Value = true;
		ActivateTrigger("Exit");
	}
}
