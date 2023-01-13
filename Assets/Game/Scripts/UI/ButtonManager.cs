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
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            string droppedGameObjectSpriteName = dropped.GetComponent<Image>().sprite.name;
            string droppedGameObjectType = PlayerInventoryManager.Instance.ReturnItemTypeByName(droppedGameObjectSpriteName);
            RuntimeAnimatorController droppedGameObjectAnimator = PlayerInventoryManager.Instance.ReturnItemAnimatorByName(droppedGameObjectSpriteName);

            if (transform.parent.CompareTag("Eq Inventory Button"))
            {
                if ((name == "Item Head Slot" && droppedGameObjectType == "Head") ||
                (name == "Item Body Slot" && droppedGameObjectType == "Body") ||
                (name == "Item Feet Slot" && droppedGameObjectType == "Feet"))
                {
                    DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                    draggableItem.parentAfterDrag = transform;
                    transform.parent.GetChild(3).gameObject.SetActive(false);
                    AnimationController.Instance.SetRightAnimatorForClothes(droppedGameObjectAnimator, droppedGameObjectType, true);
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                if (draggableItem.parentAfterDrag.parent.CompareTag("Eq Inventory Button"))
                {
                    AnimationController.Instance.SetRightAnimatorForClothes(droppedGameObjectAnimator, droppedGameObjectType, false);
                }
                draggableItem.parentAfterDrag = transform;

                return;
            }
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
        else if (CompareTag("Shopping Cart Button") && !Player.Instance.isOnInventory)
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
    }
}