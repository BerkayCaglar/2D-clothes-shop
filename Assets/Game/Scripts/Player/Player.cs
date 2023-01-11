using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }
    public bool isOnFade { get; set; }
    public bool isOnDialogue { get; set; }
    public bool isOnShopInventory { get; set; }
    public bool isOnInventory { get; set; }
    public bool isInZone { get; set; }
    public int cash = 1000;

    public delegate void OnValueChanged(int cash, bool isNotEnoughCash = false);
    public OnValueChanged onValueChangedCallback;
    private void Awake()
    {
        DontDestroyThePlayerOnLoad();
    }
    private void Start()
    {
        InvokeCashUpdate(this.cash);
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
            onValueChangedCallback.Invoke(this.cash, true);
            return false;
        }
        this.cash += cash;
        InvokeCashUpdate(this.cash);
        return true;
    }

    public void InvokeCashUpdate(int cash)
    {
        onValueChangedCallback.Invoke(cash);
    }
}
