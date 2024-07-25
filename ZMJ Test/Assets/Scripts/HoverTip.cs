using UnityEngine;

public class HoverTip : MonoBehaviour
{
    private Camera cam;
    private Vector3 min, max;
    private RectTransform rect;
    private float offset = 10f;

    private float posX;
    private float posY;

    void Start()
    {
        cam = Camera.main;
        rect = GetComponent<RectTransform>();
        min = new Vector3(0, 0, 0);
        max = new Vector3(cam.pixelWidth, cam.pixelHeight, 0);
    }

    void Update()
    {
        Vector3 position = new Vector3(Input.mousePosition.x + rect.rect.width, Input.mousePosition.y - (rect.rect.height / 2 + offset), 0f);

        if (position.x > max.x)
        {
            posX = Input.mousePosition.x - rect.rect.width;
        }
        else
        {
            posX = Mathf.Clamp(position.x, min.x, max.x) - rect.rect.width;
        }

        if (position.y < rect.rect.height / 2)
        {
            posY = Input.mousePosition.y;
        }
        else
        {
            posY = Mathf.Clamp(position.y, min.y + rect.rect.height / 2, max.y) - rect.rect.height / 2;
        }

        transform.position = new Vector3(posX, posY, transform.position.z); 
    }
}