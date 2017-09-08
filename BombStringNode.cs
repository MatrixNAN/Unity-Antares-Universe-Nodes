using UnityEngine;
using Antares.Vizio.Runtime;

[VisualLogicBlock("Bomb String", "Smart Blocks/TokenBomb")]
public class BombStringNode : LogicBlock
{
	[Parameter(VariableType.In, typeof(int))]
	public Variable B;
	
	[Parameter(VariableType.In, typeof(int))]
	public Variable O;
	
	[Parameter(VariableType.In, typeof(int))]
	public Variable M;
	
	[Parameter(VariableType.Out, typeof(bool))]
	public Variable result;
	
	[Parameter(VariableType.Out, typeof(string))]
	public Variable BombString;
	
	private int inB, inO, inM;
	private string bombStr = "";
	private string bLetter1 = "";
	private string oLetter = "";
	private string mLetter = "";
	private string bLetter2 = "";
	
	public override void OnInitializeDefaultData()
	{
		RegisterOutputTrigger("Exit");
	}
	
	[EntryTrigger]
	public void In()
	{
		inB = (int) B.Value;
		inO = (int) O.Value;
		inM = (int) M.Value;
		bombStr = "";
		bLetter1 = "";
		oLetter = "";
		mLetter = "";
		bLetter2 = "";
		
		if ( inB >= 2 && inO > 0 && inM > 0)
		{
			result.Value = true;
			bombStr = "BOMB";
		}
		else
		{
			result.Value = false;
			
			if ( inB > 1 )
			{
				bLetter1 = "B";
				bLetter2 = "B";
			}
			else if ( inB > 0 )
			{
				bLetter1 = "B";
			}
			
			if ( inO > 0 )
			{
				oLetter = "O";
			}
			
			if ( inM > 0 )
			{
				mLetter = "M";
			}
		}
		bombStr = bLetter1 + oLetter + mLetter + bLetter2;
		BombString.Value = bombStr;
		ActivateTrigger("Exit");
	}
}
