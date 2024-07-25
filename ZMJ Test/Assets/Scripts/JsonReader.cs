using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using PIXEL.FrameWork;
using UnityEditor;
using System.Linq;

public class JsonReader : SingletonPatternMono<JsonReader>
{
    public string jsonFileName_GiftPack;
    public string jsonFileName_PlayerInfo;

    private string jsonFilePath;

    public PlayerInfoData playerInfoData;

    private void Awake()
    {
        Init();

        ReadPlayerInfoJson(out playerInfoData);

        UpdateGiftConfigures();

        foreach (GiftData giftData in playerInfoData.playerGifts)
        {
            giftData.ConvertCurrency();
        }
    }

    private void Init() 
    {
        jsonFilePath = Application.streamingAssetsPath;
    }

    private void UpdateGiftConfigures() 
    {
        string filePath = System.IO.Path.Combine(jsonFilePath, jsonFileName_GiftPack + ".json");

        string jsonContent = System.IO.File.ReadAllText(filePath, Encoding.UTF8);

        GiftPack pack = JsonUtility.FromJson<GiftPack>(jsonContent);

        foreach (GiftData giftData in pack.giftConfigures)
        {
            bool contained = false;

            for (int i = 0; i < playerInfoData.playerGifts.Count; i++)
            {
                if (playerInfoData.playerGifts[i].giftId == giftData.giftId)
                {
                    contained = true;
                    break;
                }
            }

            if (contained == false)
            {
                playerInfoData.playerGifts.Add(giftData);
            }
        }
    }

    public void ReadPlayerInfoJson(out PlayerInfoData _playerInfoData) 
    {
        string filePath = System.IO.Path.Combine(jsonFilePath, jsonFileName_PlayerInfo + ".json");

        string jsonContent = System.IO.File.ReadAllText(filePath, Encoding.UTF8);

        _playerInfoData = JsonUtility.FromJson<PlayerInfoData>(jsonContent);
    }

    public IEnumerator SavePlayerInfoJson()
    {
        GiftSort();

        string jsonData = JsonUtility.ToJson(playerInfoData, true);

        string filePath = System.IO.Path.Combine(jsonFilePath, jsonFileName_PlayerInfo + ".json");

        File.WriteAllText(filePath, jsonData, Encoding.UTF8);

        yield return null;
    }

    private void GiftSort()
    {
        playerInfoData.playerGifts = playerInfoData.playerGifts.OrderBy(giftData => giftData.giftId).ToList();
    }

    #region Not Used
    //private void readgiftpackjson()
    //{
    //    string filepath = system.io.path.combine(jsonfilepath, jsonfilename_giftpack + ".json");

    //    string jsoncontent = system.io.file.readalltext(filepath, encoding.utf8);

    //    giftpack pack = jsonutility.fromjson<giftpack>(jsoncontent);

    //    foreach (giftdata giftdata in pack.giftconfigures)
    //    {
    //        giftdatas.add(giftdata);

    //        if (playerinfodata.playergifts.contains(giftdata) == false)
    //        {
    //            playerinfodata.playergifts.add(giftdata);
    //        }
    //    }

    //    saveplayerinfojson();
    //}

    //public ienumerator savegiftpackdata(list<giftdata> _giftdatas) 
    //{
    //    giftpack giftpack = new giftpack();
    //    giftpack.giftconfigures = _giftdatas.toarray();

    //    string jsondata = jsonutility.tojson(giftpack);

    //    string filepath = system.io.path.combine(jsonfilepath, jsonfilename_giftpack + ".json");

    //    file.writealltext(filepath, jsondata, encoding.utf8);

    //    yield return null;
    //}
    #endregion

    public Sprite LoadGiftSprite(string _giftSpriteName) 
    {
        return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Assets/Textures/Icon/" + _giftSpriteName + ".png");
    }
}
