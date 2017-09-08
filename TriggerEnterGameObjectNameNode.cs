using UnityEngine;
using Antares.Vizio.Runtime;

[VisualLogicBlock("Trigger On Enter Game Object Name Compare", "Smart Blocks/Triggers")]
public class TriggerEnterGameObjectNameNode : LogicBlock
{
	[Parameter(VariableType.In, typeof(string))]
	public Variable GameObjectName;
	
	[Parameter(VariableType.Out, typeof(bool))]
	public Variable result;
	
	[Parameter(VariableType.Out, typeof(GameObject))]
	public Variable ColliderGameObject;
	
	[Parameter(VariableType.Out, typeof(string))]
	public Variable ColliderGameObjectName;
	
	private string inGameObjectName = "";
	
	public override void OnInitializeDefaultData()
	{
		RegisterOutputTrigger("True");
		RegisterOutputTrigger("False");
	}
	
	void OnTriggerEnter(Collider col)
	{
		ColliderGameObject.Value = col.gameObject;
		ColliderGameObjectName.Value = col.gameObject.tag;
		inGameObjectName = GameObjectName.Value.ToString();
			
		if (col.gameObject.name == inGameObjectName)
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
