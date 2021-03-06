using UnityEngine;
using Antares.Vizio.Runtime;

[VisualLogicBlock("Trigger On Exit Tag Compare", "Smart Blocks/Triggers")]
public class TriggerExitTagNode : LogicBlock
{
	[Parameter(VariableType.In, typeof(string))]
	public Variable TagName;
	
	[Parameter(VariableType.Out, typeof(bool))]
	public Variable result;
	
	[Parameter(VariableType.Out, typeof(string))]
	public Variable ColliderGameObject;
	
	[Parameter(VariableType.Out, typeof(string))]
	public Variable ColliderTagName;
	
	private string inTagName = "";
	
	public override void OnInitializeDefaultData()
	{
		RegisterOutputTrigger("True");
		RegisterOutputTrigger("False");
	}
	
	void OnTriggerExit(Collider col)
	{
		ColliderGameObject.Value = col.gameObject;
		ColliderTagName.Value = col.gameObject.tag;
		inTagName = TagName.Value.ToString();

		if (col.gameObject.tag == inTagName)
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
