using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject interactionPanel;
    public TextMeshProUGUI interactionText;
    public static UIManager instance;

    private void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ShowInteraction(string interaction)
    {
        Debug.Log("Press E " + interaction);
        interactionPanel.SetActive(true);
        interactionText.text = interaction;
    }

    public void HideInteraction()
    {
        Debug.Log("Don't press E ");
        interactionPanel.SetActive(false);
    }
}
