using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GiftTips : MonoBehaviour
{
    [Header("Info")]
    private string giftName;
    private string giftInfo;
    private int quality;

    [Header("Needs Component")]
    public TextMeshProUGUI text_Name;
    public TextMeshProUGUI text_Info;

    [Header("Level Color")]
    public Color color_Level0;
    public Color color_Level1;
    public Color color_Level2;

    public void UpdateModel(string _name, string _info, int _level) 
    {
        this.giftName = _name;
        this.giftInfo = _info;
        this.quality = _level;

        UpdateVeiw();
    }

    private void UpdateVeiw() 
    {
        text_Name.text = giftName;
        text_Info.text = giftInfo;

        switch (quality)
        {
            case 0:
                text_Name.color = color_Level0;
                text_Info.color = color_Level0;
                break;
            case 1:
                text_Name.color = color_Level1;
                text_Info.color = color_Level1;
                break;
            case 2:
                text_Name.color = color_Level2;
                text_Info.color = color_Level2;
                break;
            default:
                break;
        }
    }
}
