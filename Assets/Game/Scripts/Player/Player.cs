using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static Player Instance;
    public bool isOnFade { get; set; }
    public bool isOnDialogue { get; set; }
    public bool isOnShopInventory { get; set; }
    public bool isOnInventory { get; set; }
    public bool isInZone { get; set; }
    public bool isDraggingItem { get; set; }
    public int cash { get; set; } = 4000;
    private SpriteRenderer HeadSocketSpriteRenderer, BodySocketSpriteRenderer, FeetSocketSpriteRenderer;
    private void Awake()
    {
        DontDestroyThePlayerOnLoad();
    }
    private void Start()
    {
        InvokeCashUpdate(this.cash);
        FindPlayerSpriteSockets();
    }
    private void FindPlayerSpriteSockets()
    {
        HeadSocketSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        BodySocketSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        FeetSocketSpriteRenderer = transform.GetChild(2).GetComponent<SpriteRenderer>();
    }
    private void DontDestroyThePlayerOnLoad()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public bool UpdateCash(int cash)
    {
        if (-cash > this.cash)
        {
            UIManager.Instance.UpdateUI(this.cash, true);
            return false;
        }
        this.cash += cash;
        InvokeCashUpdate(this.cash);
        return true;
    }

    public void InvokeCashUpdate(int cash)
    {
        UIManager.Instance.UpdateUI(cash);
    }
}
