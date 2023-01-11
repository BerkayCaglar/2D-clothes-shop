using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    private List<TMP_Text> cashTextsUI = new List<TMP_Text>();
    [SerializeField]
    private GameObject CashUI;
    public static UIManager Instance { get; set; }
    void Awake()
    {
        Player.Instance.onValueChangedCallback += UpdateUI;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {

    }
    public void UpdateUI(int cash, bool isNotEnoughCash)
    {
        if (isNotEnoughCash)
        {
            if (CashUI == null)
            {
                CashUI = GameObject.Find("Cash Parent");
            }
            CashUI.GetComponent<Animator>().Play("Not_Enough_Cash");
            return;
        }
        foreach (TMP_Text text in cashTextsUI)
        {
            if (text != null)
            {
                text.text = cash.ToString();
            }
        }
    }
    public void GetCashTextsUI()
    {
        foreach (GameObject textObject in GameObject.FindGameObjectsWithTag("Cash Text UI"))
        {
            if (textObject != null)
            {
                cashTextsUI.Add(textObject.GetComponent<TMP_Text>());
            }
        }
        if (CashUI == null)
        {
            CashUI = GameObject.Find("Cash Parent");
        }
    }
}