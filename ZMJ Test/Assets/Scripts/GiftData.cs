using System;
using UnityEngine;

public enum Currency 
{
    Í­,Òø,½ð
}

[System.Serializable]
public class GiftData
{
    public long giftId;
    public string giftName;
    public int giftQuality;
    public string giftSprite;
    public string giftInfo;
    public float giftPrice;
    public int giftCurrency;
    public int giftBuyTimes;

    [Header("Not Save")]
    public Currency currency; 

    public void ConvertCurrency() 
    {
        currency = (Currency)giftCurrency;
    }
}
