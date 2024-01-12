using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI descriptionTxt;

    public static ToolTip instance;

    private void Start()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }

    public void SetupTooltip(string name, string des)
    {
        this.gameObject.SetActive(true);
        nameTxt.text = name;
        descriptionTxt.text = des;
    }
}
