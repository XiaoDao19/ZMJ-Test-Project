using PIXEL.Game.General;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    private Button bt_Store;
    private GameObject ui_Store;

    private void Start()
    {
        bt_Store = GameObject.Find("Button_Store").GetComponent<Button>();

        ui_Store = GameObject.Find("UI_Store");
        ui_Store.SetActive(false);

        bt_Store.onClick.AddListener(() => { ui_Store.SetActive(!ui_Store.activeSelf); });
    }
}
