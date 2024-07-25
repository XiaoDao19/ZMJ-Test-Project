using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreBuy : MonoBehaviour
{
    [Header("UIComponent_Button")]
    public Button bt_Buy;
    public Button bt_Cancle;

    [Header("UIComponent_Image")]
    public Image image_Gift;

    [Header("UIComponent_Text")]
    public TextMeshProUGUI text_Title;
    public TextMeshProUGUI text_Info;
    public TextMeshProUGUI text_Cost;

    [Header("Store Slot")]
    private StoreSlot storeSlot;

    [Header("Info")]
    public GameObject giftInfoTips;
    public RectTransform giftInfo;
    public Vector3 originalPos;
    public Vector3 targetPos;
    public Vector2 orignialSize;

    [Header("Level Color")]
    public Color color_Level0;
    public Color color_Level1;
    public Color color_Level2;

    private void Start()
    {
        Init();
    }

    private void Init() 
    {
        bt_Buy.onClick.AddListener(() =>
        {
            storeSlot.Buy();
        });

        bt_Cancle.onClick.AddListener(GiftInfoTipClose);
    }

    private void UpdateView() 
    {
        image_Gift.sprite = storeSlot.image_Gift.sprite;

        text_Title.text = storeSlot.GetGiftName();
        text_Info.text = storeSlot.GetGiftInfo();
        text_Cost.text = storeSlot.text.text;

        switch (storeSlot.GetGiftQuality())
        {
            case 0:
                text_Title.color = color_Level0;
                break;
            case 1:
                text_Title.color = color_Level1;
                break;
            case 2:
                text_Title.color = color_Level2;
                break;
            default:
                break;
        }
    }


    public void GiftInfoTipOpen(Vector3 _startPos, StoreSlot _storeSlot)
    {
        this.storeSlot = _storeSlot;

        UpdateView();

        HideChilds();

        giftInfo.sizeDelta = Vector2.zero;
        giftInfo.localPosition = _startPos;
        targetPos = _startPos;
        giftInfo.gameObject.SetActive(true);

        giftInfo.DOSizeDelta(orignialSize, 0.3f).OnComplete(ShowChilds);
        giftInfo.DOLocalMove(originalPos, 0.3f);
    }

    public void GiftInfoTipClose()
    {
        HideChilds();

        giftInfo.DOSizeDelta(Vector3.zero, 0.3f);
        giftInfo.DOLocalMove(targetPos, 0.3f);
    }

    private void HideChilds() 
    {
        for (int i = 0; i < giftInfo.childCount; i++)
        {
            giftInfo.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void ShowChilds() 
    {
        for (int i = 0; i < giftInfo.childCount; i++)
        {
            giftInfo.GetChild(i).gameObject.SetActive(true);
        }
    }
}
