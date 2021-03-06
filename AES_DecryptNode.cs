using UnityEngine;
using Antares.Vizio.Runtime;
using System.Security.Cryptography;
using System;
using System.Text;

[VisualLogicBlock("AES Decrypt", "Smart Blocks/Encryption")]
public class AES_DecryptNode : LogicBlock
{
	[Parameter(VariableType.In, typeof(string))]
	public Variable EncryptedText;
	
	[Parameter(VariableType.In, typeof(string))]
	public Variable Key;
	
	[Parameter(VariableType.Out, typeof(bool))]
	public Variable result;
	
	[Parameter(VariableType.Out, typeof(string))]
	public Variable DecryptedText;
	
	private string inEncryptedText = "";
	private string inKey = "12345678901234567890123456789012";	

	public override void OnInitializeDefaultData()
	{
		RegisterOutputTrigger("Exit");
		result.Value = false;
		DecryptedText.Value = "";
		Key.Value = "12345678901234567890123456789012";
	}
	
	[EntryTrigger]
	public void In()
	{
		inEncryptedText = EncryptedText.Value.ToString();
		inKey = Key.Value.ToString();
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes (inKey);
		// AES-256 key
		byte[] toEncryptArray = Convert.FromBase64String (inEncryptedText);
		RijndaelManaged rDel = new RijndaelManaged();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		// http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
		rDel.Padding = PaddingMode.PKCS7;
		// better lang support
		ICryptoTransform cTransform = rDel.CreateDecryptor ();
		byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
		DecryptedText.Value = UTF8Encoding.UTF8.GetString (resultArray);
		
		result.Value = true;
		ActivateTrigger("Exit");
	}
}
