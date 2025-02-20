using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float speedBoost = 2f; // Factor de aumento de velocidad
    public float duration = 5f; // Duración del powerup

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Llamamos al método que maneja el aumento de velocidad del jugador
            other.GetComponent<PlayerController>().ActivateSpeedBoost(speedBoost, duration);

            // Destruimos el powerup una vez que se recoge
            Destroy(gameObject);
        }
    }
}
