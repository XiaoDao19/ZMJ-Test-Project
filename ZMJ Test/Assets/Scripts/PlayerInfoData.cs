using System.Collections.Generic;

[System.Serializable]
public class PlayerInfoData
{
    public float copper;   
    public float silver;   
    public float gold;

    public List<GiftData> playerGifts;

    public void GetData(float _copper, float _silver, float _gold) 
    {
        this.copper = _copper;
        this.silver = _silver;
        this.gold = _gold;
    }
}
