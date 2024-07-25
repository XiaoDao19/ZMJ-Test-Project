using UnityEngine;

public class FrameRateCheck : MonoBehaviour
{

    private Rect rect = new Rect(0, 0, 88, 88);

    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;

    GUIStyle style = new GUIStyle();

    private void Start()
    {
        style.fontSize = 39;
        style.normal.textColor = Color.green;
    }

    void Update()
    {
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }
    }
    private void OnGUI()
    {
        GUI.Label(rect, ((int)m_lastFramerate).ToString(), style);
    }
}
