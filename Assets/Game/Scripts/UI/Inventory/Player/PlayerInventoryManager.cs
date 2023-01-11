using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInventoryManager : MonoBehaviour
{
    #region Singleton
    public static PlayerInventoryManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public int itemSpace = 11;
    public List<Item> items = new List<Item>();

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= itemSpace)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            bool result = Player.Instance.UpdateCash(-item.price);
            if (result)
            {
                items.Add(item);
                return true;
            }
        }
        return false;
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
    public void SetActiveCanvas(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public bool ReturnCanvasIsActive()
    {
        return gameObject.activeSelf;
    }

    [System.Obsolete]
    public void PutItemsOnPlayerInventoryUI()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                GameObject child = transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
                for (int j = 0; j < child.transform.childCount; j++)
                {
                    GameObject childChild = child.transform.GetChild(j).gameObject;
                    if (childChild.transform.childCount > 0)
                    {
                        Image image = childChild.transform.GetChild(0).GetComponent<Image>();
                        image.sprite = items[i].icon;
                        image.enabled = true;
                    }
                    else
                    {
                        Button button = childChild.GetComponent<Button>();
                        button.interactable = true;
                    }
                }
            }
        }
    }
    public void CheckPlayerInventoryUIIsActive()
    {
        if (PlayerInventoryManager.Instance.ReturnCanvasIsActive())
        {
            PlayerInventoryManager.Instance.SetActiveCanvas(false);
            Player.Instance.isOnInventory = false;
        }
    }
}
