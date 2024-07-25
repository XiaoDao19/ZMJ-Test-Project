using System;
using UnityEngine;

public enum Currency 
{
    copper,sliver,gold
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
        switch (giftCurrency)
        {
            case 0:
                currency = Currency.copper;
                break;
            case 1:
                currency = Currency.sliver;
                break;
            case 2:
                currency = Currency.gold;
                break;
            default:
                break;
        }
    }

    public string GetCurrency() 
    {
        switch (currency)
        {
            case Currency.copper:
                return "Í­";
            case Currency.sliver:
                return "Òø";
            case Currency.gold:
                return "½ð";
            default:
                break;
        }

        return "Ç®";
    }
}
