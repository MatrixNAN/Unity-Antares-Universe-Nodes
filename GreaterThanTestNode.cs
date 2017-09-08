using UnityEngine;
using Antares.Vizio.Runtime;
using System.Collections.Generic;
using System.IO;
using Unibill.Demo;

[VisualLogicBlock("Greater Than Test", "Smart Blocks/Tests")]
public class GreaterThanTestNode : LogicBlock
{
	[Parameter(VariableType.In, typeof(int))]
	public Variable Num1;
	
	[Parameter(VariableType.In, typeof(int))]
	public Variable Num2;
		
	[Parameter(VariableType.Out, typeof(int))]
	public Variable result;
		
	public override void OnInitializeDefaultData()
	{
		RegisterOutputTrigger("True");
		RegisterOutputTrigger("False");
	}
	
	[EntryTrigger]
	public void In()
	{
		if ((int) Num1.Value > (int) Num2.Value)
		{
			result.Value = true;
			ActivateTrigger("True");
		}
		else
		{
			result.Value = false;
			ActivateTrigger("False");			
		}
	}
}
