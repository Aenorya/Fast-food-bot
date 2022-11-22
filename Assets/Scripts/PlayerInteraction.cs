using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform hand;
    private bool holdsSomething = false;
    public GameObject objectInHand;

    public GameObject currentInteractive;

    private void OnTriggerStay(Collider other)
    {
        currentInteractive = other.gameObject;
        if (currentInteractive.CompareTag("Processor"))
        {
            Processor processor = currentInteractive.GetComponent<Processor>();

            if (holdsSomething && processor.state == ProcessorStates.Idle)
            {
                Ingredient potentialIngredient = objectInHand.GetComponent<Ingredient>();

                if (potentialIngredient != null)
                {
                    UIManager.instance.ShowInteraction("to put "+potentialIngredient.data.label);
                }
            }
            else if (processor.state == ProcessorStates.WaitingToCook)
            {
                UIManager.instance.ShowInteraction("to cook");
            }
            else if (processor.state == ProcessorStates.Done)
            {
                UIManager.instance.ShowInteraction("to pick up " + processor.recipeToCook.result.label);
            }
        }
        else if (currentInteractive.CompareTag("Bank"))
        {
            if (!holdsSomething)
            {
                UIManager.instance.ShowInteraction("to take " + currentInteractive.GetComponent<IngredientBank>().ingredient.label);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        UIManager.instance.HideInteraction();
    }

    public void HoldObject(GameObject toHold)
    {
        objectInHand = Instantiate(toHold, hand);
        holdsSomething = true;
    }

    public void ReleaseObject()
    {
        Destroy(objectInHand);
        holdsSomething = false;
    }

    public void Interact()
    {
        if(currentInteractive == null) return;
        if (currentInteractive.CompareTag("Processor"))
        {
            Processor processor = currentInteractive.GetComponent<Processor>();
            if (holdsSomething && processor.state == ProcessorStates.Idle)
            {
                Ingredient potentialIngredient = objectInHand.GetComponent<Ingredient>();

                if (potentialIngredient != null)
                {
                    processor.AddIngredient(potentialIngredient.data);
                    ReleaseObject();
                }
            }
            else if(processor.state == ProcessorStates.WaitingToCook)
            {
                processor.Cook();
            }
            else if (processor.state == ProcessorStates.Done)
            {
                processor.GiveFood();
            }
        }
        else if (currentInteractive.CompareTag("Bank"))
        {
            if (!holdsSomething)
            {
                HoldObject(currentInteractive.GetComponent<IngredientBank>().ingredient.prefab);
            }
        }
        UIManager.instance.HideInteraction();
    }
}
