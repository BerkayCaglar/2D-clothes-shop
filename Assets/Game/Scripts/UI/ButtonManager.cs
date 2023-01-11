using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public Item item;
    public Texture2D cursorTex;
    public GameObject ClothesShopCanvas;
    private void Start()
    {
        if (ClothesShopCanvas == null)
        {
            ClothesShopCanvas = GameObject.Find("Clothes Shop Canvas");
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cursorTex != null)
        {
            Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }
    public void Clicked()
    {
        if (CompareTag("Clothes Shop Item"))
        {
            bool result = PlayerInventoryManager.Instance.AddItem(item);
            if (result)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<Image>().raycastTarget = false;

                transform.parent.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else if (CompareTag("Shopping Cart Button"))
        {
            PlayerInventoryManager.Instance.CheckPlayerInventoryUIIsActive();
            ClothesShopCanvas.SetActive(true);
            Player.Instance.isOnShopInventory = true;
        }
        else if (CompareTag("Close Shop Button"))
        {
            ClothesShopCanvas.SetActive(false);
            Player.Instance.isOnShopInventory = false;
        }
        /*
        else if (CompareTag("Player Inventory Button"))
        {
            transform.parent.GetChild(2).gameObject.SetActive(true);
        }
        else if (name == "Equip")
        {
            transform.parent.gameObject.SetActive(false);
        }
        else if (name == "Cancel")
        {
            transform.parent.gameObject.SetActive(false);
        }
        */
    }
}