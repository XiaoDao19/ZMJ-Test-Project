using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class StoreSlot : MonoBehaviour,IPointerClickHandler
{
    [Header("Gift Info")]
    private GiftData giftData;
    private Sprite giftImageSprite;

    [Header("Need Component")]
    public Button bt_Slot;
    public Image image_Gift;
    public TextMeshProUGUI text;

    [Header("Sell Out")]
    public GameObject sellOut;

    [Header("Cursor State")]
    private bool isCursorIn;
    private bool buyTimeEnough = true;
    private bool moneyEnough = false;

    private void Start()
    {
        Init();
    }

    private void Init() 
    {
        bt_Slot.onClick.AddListener(() =>
        {
            Buy();
        });
    }

    public void UpdateModel(GiftData _giftData) 
    {
        this.giftData = _giftData;
        giftImageSprite = JsonReader.Instance.LoadGiftSprite(giftData.giftSprite);

        UpdateView();
    }

    private void UpdateView() 
    {
        image_Gift.sprite = giftImageSprite;

        string color = null;

        switch (giftData.currency)
        {
            case Currency.copper:
                color = StoreManager.Instance.color_Copper.ToHexString();
                break;
            case Currency.sliver:
                color = StoreManager.Instance.color_Silver.ToHexString();
                break;
            case Currency.gold:
                color = StoreManager.Instance.color_Gold.ToHexString();
                break;
            default:
                break;
        }

        text.text = giftData.giftPrice.ToString() + "<color=#" + color + ">" + giftData.GetCurrency() + "</color>";

        if (giftData.giftBuyTimes <= 0)
        {
            sellOut.SetActive(true);

            StoreManager.Instance.giftInfoTips.SetActive(false);

            buyTimeEnough = false;
        }
    }

    private void Controller_Buy()
    {
        giftData.giftBuyTimes -= 1;

        UpdateView();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (buyTimeEnough == false)
        {
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            StoreManager.Instance.BuyOpen(StoreManager.Instance.GetPos(Input.mousePosition), this);
        }  
    }

    public void Buy() 
    {
        moneyEnough = false;

        switch (giftData.currency)
        {
            case Currency.copper:
                if (JsonReader.Instance.playerInfoData.copper >= giftData.giftPrice)
                    moneyEnough = true;
                break;
            case Currency.sliver:
                if (JsonReader.Instance.playerInfoData.silver >= giftData.giftPrice)
                    moneyEnough = true;
                break;
            case Currency.gold:
                if (JsonReader.Instance.playerInfoData.gold >= giftData.giftPrice)
                    moneyEnough = true;
                break;
            default:
                break;
        }

        if (moneyEnough == false)
            return;

        StoreManager.Instance.buyGift?.Invoke(giftData.giftPrice, giftData.currency);

        Controller_Buy();

        StoreManager.Instance.BuyClose();
    }

    public string GetGiftName() 
    {
        return giftData.giftName;
    }

    public string GetGiftInfo()
    {
        return giftData.giftInfo;
    }

    public int GetGiftQuality() 
    {
        return giftData.giftQuality;
    }
}
