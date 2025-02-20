using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager MyInstance;

    public TextMeshProUGUI ScoreText;
    public int Score;

    public GameObject GameoverPanel;
    public GameObject StartGamePanel;

    // Agregamos las variables del temporizador
    public TextMeshProUGUI TimerText; // Para mostrar el temporizador en la UI
    public float tiempoLimite = 60f;  // Tiempo en segundos
    private float tiempoRestante;

    private void Awake()
    {
        MyInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        StartGamePanel.SetActive(true);
        GameoverPanel.SetActive(false);
        tiempoRestante = tiempoLimite;  // Inicializa el tiempo restante con el tiempo límite
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString();

        if (Time.timeScale == 1) // Solo actualiza el temporizador si el juego está en marcha
        {
            // Resta el tiempo transcurrido desde el último frame
            tiempoRestante -= Time.deltaTime;

            // Muestra el tiempo restante en el UI
            TimerText.text = Mathf.Max(0, tiempoRestante).ToString("F2");  // Muestra el tiempo con 2 decimales

            // Si el tiempo llega a 0, termina el juego
            if (tiempoRestante <= 0)
            {
                TerminarJuego();
            }
        }
    }

    public void StartGame()
    {
        StartGamePanel.SetActive(false);
        Time.timeScale = 1;  // Inicia el juego
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void TerminarJuego()
    {
        // Aquí puedes llamar a la función que muestre el Game Over o termine el juego
        GameoverPanel.SetActive(true);
        Time.timeScale = 0;  // Pausa el juego
    }
}
