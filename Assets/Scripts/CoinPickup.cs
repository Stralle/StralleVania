using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField]
    AudioClip m_CoinPickupSFX;

    [SerializeField]
    int m_pointsForCoinPickup = 100;

    // States
    bool entered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision " + collision);
        if (!entered)
        {
            AudioSource.PlayClipAtPoint(m_CoinPickupSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddToScore(m_pointsForCoinPickup);
            Destroy(gameObject);
            entered = true;
        }
    }
}
