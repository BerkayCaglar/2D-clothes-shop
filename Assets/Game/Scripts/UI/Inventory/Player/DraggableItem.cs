using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent.parent.CompareTag("Eq Inventory Button"))
        {
            transform.parent.parent.GetChild(3).gameObject.SetActive(true);
        }
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        Player.Instance.isDraggingItem = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        if (transform.parent.parent.CompareTag("Eq Inventory Button"))
        {
            transform.parent.parent.GetChild(3).gameObject.SetActive(false);
        }
        image.raycastTarget = true;
        Player.Instance.isDraggingItem = false;
    }
}
