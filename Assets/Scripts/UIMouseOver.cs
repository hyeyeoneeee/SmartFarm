using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(item.itemName);
        //Debug.Log(item.itemDescription);
        ToolTip.instance.SetupTooltip(item.itemName, item.itemDescription);
        ToolTip.instance.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("¸¶¿ì½º Å»Ãâ");
        ToolTip.instance.gameObject.SetActive(false);
    }
}
