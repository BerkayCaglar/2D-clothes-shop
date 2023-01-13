using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += ConfiurgeVariablesOnExitPlayMode;
#endif
    }
    public bool AddItem(Item item)
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
    public string ReturnItemTypeByName(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == itemName)
            {
                return items[i].type;
            }
        }
        return null;
    }
    public RuntimeAnimatorController ReturnItemAnimatorByName(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == itemName)
            {
                return items[i].overrideAnimator;
            }
        }
        return null;
    }
    public void PutItemsOnPlayerInventoryUI()
    {
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null && !items[i].isOnInventory)
                {
                    GameObject child = transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
                    for (int j = 0; j < child.transform.childCount; j++)
                    {
                        GameObject childChild = child.transform.GetChild(j).gameObject;
                        if (childChild.transform.childCount == 0)
                        {
                            CreateAndSetImageObject(childChild, i);
                            break;
                        }
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
    private void CreateAndSetImageObject(GameObject childChild, int i)
    {
        GameObject imageObject = new GameObject("Image");
        Image image = imageObject.AddComponent<Image>();
        DraggableItem draggableItem = imageObject.AddComponent<DraggableItem>();

        imageObject.transform.SetParent(childChild.transform);
        draggableItem.image = image;
        image.sprite = items[i].icon;
        items[i].isOnInventory = true;
        imageObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.4f, 1f);
    }
    /// <summary>
    /// Reset variables when the application is closed.
    /// </summary>
    void OnApplicationQuit()
    {
        for (int i = 0; i < itemSpace; i++)
        {
            if (items[i] != null)
            {
                items[i].isOnInventory = false;
                items[i].isSold = false;
            }
        }
    }
#if UNITY_EDITOR
    /// <summary>
    /// Reset variables when the application is closed. (EDITOR ONLY)
    /// </summary>
    /// <param name="state"></param>
    private void ConfiurgeVariablesOnExitPlayMode(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null)
                {
                    items[i].isOnInventory = false;
                    items[i].isSold = false;
                }
            }
        }
    }
#endif
}
