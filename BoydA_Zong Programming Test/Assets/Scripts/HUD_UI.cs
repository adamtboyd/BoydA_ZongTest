//Code by Adam Boyd for Zong Programming Test

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HUD_UI : MonoBehaviour
{
    public TextMeshProUGUI victoryText;
    public GameObject mainPanel;

    public void ToggleUI(bool bSetStateToActive)
    {
        mainPanel.SetActive(bSetStateToActive);
    }

    public void ShowSuccessMessage(string variableText)
    {
        victoryText.gameObject.SetActive(true);
        victoryText.text = "Stone depositied in box " + variableText;
        gameObject.SetActive(true);
    }
}
