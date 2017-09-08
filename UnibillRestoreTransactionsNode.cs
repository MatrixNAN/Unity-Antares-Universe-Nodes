using UnityEngine;
using Antares.Vizio.Runtime;
using System.Collections.Generic;
using System.IO;
using Unibill.Demo;

[VisualLogicBlock("Unibill Restore Transactions", "Smart Blocks/Unibill")]
public class UnibillRestoreTransactionsNode : LogicBlock
{
	public override void OnInitializeDefaultData()
	{
		RegisterOutputTrigger("Exit");
	}
	
	[EntryTrigger]
	public void In()
	{
        Unibiller.restoreTransactions();
		ActivateTrigger("Exit");
	}
}
