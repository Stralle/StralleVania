using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour
{
    [Tooltip ("Game units per second")]
    [SerializeField]
    float m_scrollRate = 0.2f;

    void Update()
    {
        float yMove = m_scrollRate * Time.deltaTime;
        transform.Translate(new Vector2(0f, yMove));
    }
}
