using UnityEngine;
using Antares.Vizio.Runtime;
using System.Collections.Generic;
using System.IO;
using Unibill.Demo;

[VisualLogicBlock("Unibill Purchase", "Smart Blocks/Unibill")]
public class UnibillPurchaseNode : LogicBlock
{
	[Parameter(VariableType.In, typeof(int))]
	public Variable Index;
	
	private PurchasableItem[] items;
	
	public override void OnInitializeDefaultData()
	{
		RegisterOutputTrigger("Exit");
	}
	
	[EntryTrigger]
	public void In()
	{
		items = Unibiller.AllPurchasableItems;
		Unibiller.initiatePurchase(items[(int) Index.Value]);
		ActivateTrigger("Exit");
	}
}
