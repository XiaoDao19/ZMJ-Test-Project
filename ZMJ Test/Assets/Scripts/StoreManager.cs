using System.Collections.Generic;
using UnityEngine;
using PIXEL.FrameWork;
using PIXEL.Git.Tools;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class StoreManager : SingletonPatternMono<StoreManager>
{
    [Header("Slot")]
    public GameObject slotPrefab;
    public RectTransform content;
    private List<StoreSlot> slots;

    [Header("Gift Data")]
    public PlayerInfoData playerInfoData;

    [Header("Gift Tips")]
    public GameObject giftInfoTips;

    [Header("UIComponent_Text")]
    public TextMeshProUGUI text_MoneyUsd;
    public TextMeshProUGUI text_MoneyEur;
    public TextMeshProUGUI text_MoneyCny;

    [Header("UIComponent_Button")]
    public Button bt_Close;

    [Header("Buy Panel")]
    public GameObject buyPanel;
    public StoreBuy storeBuy;

    [Header("Money Used Tip")]
    public RectTransform posUsd;
    public RectTransform posEur;
    public RectTransform posCny;

    [Header("Money Color")]
    public Color color_Copper;
    public Color color_Silver;
    public Color color_Gold;

    [Header("Camera")]
    public Camera camera;

    public delegate void Buy(float _money, Currency _currency);
    public Buy buyGift;

    private void Start()
    {
        Init();   
    }

    private void Init() 
    {
        playerInfoData = JsonReader.Instance.playerInfoData;

        GiftSort();

        UpdateMoney();

        slots = new List<StoreSlot>();

        for (int i = 0; i < playerInfoData.playerGifts.Count; i++)
        {
            StoreSlot currentSlot = Manager_ObjectPool.GetTargetObject("StoreSlot").GetComponent<StoreSlot>();

            RectTransform currentSlotRect = currentSlot.GetComponent<RectTransform>();
            currentSlotRect.SetParent(content);
            currentSlotRect.localScale = Vector3.one;
            currentSlotRect.localPosition = Vector3.zero;

            currentSlot.UpdateModel(playerInfoData.playerGifts[i]);
            slots.Add(currentSlot);
        }

        buyGift += BuyGift;

        bt_Close.onClick.AddListener(() => 
        { 
            StartCoroutine(JsonReader.Instance.SavePlayerInfoJson());

            gameObject.SetActive(false);
        });
    }

    private void GiftSort() 
    {
        playerInfoData.playerGifts = playerInfoData.playerGifts.OrderBy(giftData => giftData.giftBuyTimes).ThenBy(giftData => giftData.giftQuality).ToList();
    }

    private void UpdateMoney() 
    {
        text_MoneyUsd.text = JsonReader.Instance.playerInfoData.copper.ToString();
        text_MoneyEur.text = JsonReader.Instance.playerInfoData.silver.ToString();
        text_MoneyCny.text = JsonReader.Instance.playerInfoData.gold.ToString();
    }

    private void BuyGift(float _cost, Currency _currency)
    {
        bool isBought = true;

        switch (_currency)
        {
            case Currency.copper:
                if (JsonReader.Instance.playerInfoData.copper >= _cost)
                    JsonReader.Instance.playerInfoData.copper -= _cost;
                else
                    isBought = false;
                break;
            case Currency.sliver:
                if (JsonReader.Instance.playerInfoData.silver >= _cost)
                    JsonReader.Instance.playerInfoData.silver -= _cost;
                else
                    isBought = false;
                break;
            case Currency.gold:
                if (JsonReader.Instance.playerInfoData.gold >= _cost)
                    JsonReader.Instance.playerInfoData.gold -= _cost;
                else
                    isBought = false;
                break;
            default:
                break;
        }

        if (isBought == true)
            MoneyUsedTip(_cost, _currency);

        UpdateMoney();
    }

    private void MoneyUsedTip(float _cost, Currency _currency) 
    {
        RectTransform father = null;

        switch (_currency)
        {
            case Currency.copper:
                father = posUsd;
                break;
            case Currency.sliver:
                father = posEur;
                break;
            case Currency.gold:
                father = posCny;
                break;
            default:
                break;
        }

        GameObject text = Manager_ObjectPool.GetTargetObject("MoneyUsedTip");
        RectTransform textRect = text.GetComponent<RectTransform>();
        textRect.SetParent(father);
        textRect.localScale = Vector3.one;
        textRect.localPosition = new Vector3(0, 9, 0);

        textRect.DOLocalMoveY(20,1f);

        TextMeshProUGUI textTmp = text.GetComponent<TextMeshProUGUI>();
        textTmp.text = _cost.ToString();
        textTmp.DOFade(0, 1f);
    }

    public Vector2 GetPos(Vector2 vector2) 
    {
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(), vector2, camera, out localPosition);
        return localPosition;
    }

    public void BuyOpen(Vector3 _startPos,StoreSlot _storeSlot) 
    {
        storeBuy.GiftInfoTipOpen(_startPos, _storeSlot);
    }

    public void BuyClose()
    {
        storeBuy.GiftInfoTipClose();
    }
}
