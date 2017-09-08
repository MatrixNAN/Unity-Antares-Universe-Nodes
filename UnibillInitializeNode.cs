using UnityEngine;
using Antares.Vizio.Runtime;
using System.Collections.Generic;
using System.IO;
using Unibill.Demo;

[VisualLogicBlock("Unibill Initialize", "Smart Blocks/Unibill")]
public class UnibillInitializeNode : LogicBlock
{
	[Parameter(VariableType.Out, typeof(bool))]
	public Variable result;
	
	[Parameter(VariableType.Out, typeof(string))]
	public Variable Text;

	public override void OnInitializeDefaultData()
	{
		RegisterOutputTrigger("OnBillReady");
		RegisterOutputTrigger("OnTransactionsRestored");
		RegisterOutputTrigger("OnCancelled");
		RegisterOutputTrigger("OnFailed");
		RegisterOutputTrigger("OnPurchased");
		RegisterOutputTrigger("Error");
	}
	
	[EntryTrigger]
	public void In()
	{
		if (UnityEngine.Resources.Load ("unibillInventory") == null) 
		{
			Text.Value = "You must define your purchasable inventory within the inventory editor!";
			// this.gameObject.SetActive(false);
			result.Value = false;
			ActivateTrigger("Error");
		}
		else
		{
	        // We must first hook up listeners to Unibill's events.
	        Unibiller.onBillerReady += onBillerReady;
	        Unibiller.onTransactionsRestored += onTransactionsRestored;
	        Unibiller.onPurchaseCancelled += onCancelled;
			Unibiller.onPurchaseFailed += onFailed;
	        Unibiller.onPurchaseComplete += onPurchased;
	
	        // Now we're ready to initialise Unibill.
	        Unibiller.Initialise();
			
			result.Value = true;
		}		
	}
	
	    /// <summary>
    /// This will be called when Unibill has finished initialising.
    /// </summary>
    private void onBillerReady(UnibillState state) 
	{
        Text.Value = "onBillerReady:" + state;
		ActivateTrigger("OnBillReady");
    }

    /// <summary>
    /// This will be called after a call to Unibiller.restoreTransactions().
    /// </summary>
    private void onTransactionsRestored (bool success) 
	{
        Text.Value = "Transactions restored.";
		ActivateTrigger("OnTransactionsRestored");
    }

    /// <summary>
    /// This will be called when a purchase completes.
    /// </summary>
    private void onPurchased(PurchasableItem item) 
	{
        Text.Value = "Purchase OK: " + item.Id;
        Text.Value += string.Format ("{0} has now been purchased {1} times.",
                                 item.name,
                                 Unibiller.GetPurchaseCount(item));
		ActivateTrigger("OnPurchased");
    }

    /// <summary>
    /// This will be called if a user opts to cancel a purchase
    /// after going to the billing system's purchase menu.
    /// </summary>
    private void onCancelled(PurchasableItem item) 
	{
        Text.Value = "Purchase cancelled: " + item.Id;
		ActivateTrigger("OnCancelled");
    }
    
    /// <summary>
    /// This will be called is an attempted purchase fails.
    /// </summary>
    private void onFailed(PurchasableItem item) 
	{
		Text.Value = "Purchase failed: " + item.Id;
		ActivateTrigger("OnFailed");
    }
}
