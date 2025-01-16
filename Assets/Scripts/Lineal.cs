using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lineal : MonoBehaviour
{
    private enum Turn { None, Player, Computer }
    private Turn currentTurn = Turn.None;
    private int targetNumber;           // Número aleatorio generado por el host (entre 0 y 9)
    private int currentIndex = 0;       // Índice de búsqueda lineal de la computadora

    public GameObject[] cubos;          // Referencia a los cubos (0-9)
    private Color colorVerde = Color.green;
    private Color colorRojo = Color.red;
    private Color colorAmarillo = Color.yellow;
    private Color colorBlanco = Color.white; // Color inicial de los cubos

    private HashSet<int> selectedNumbers = new HashSet<int>(); // Números ya seleccionados

    public void PlayGameEasy()
    {
        SceneManager.LoadScene("MiniJuegoFacil");
    }
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (currentTurn == Turn.Player) // Turno del jugador
            HandlePlayerTurn();
    }

    // ========================
    // Iniciar el Juego
    // ========================
    void StartGame()
    {
        targetNumber = GenerateRandomNumber(); // Generar un número aleatorio entre 0 y 9
        Debug.Log($"El número objetivo (secreto) es: {targetNumber}"); // Solo para pruebas
        Debug.Log("El primero que adivine el número gana.");

        currentTurn = Turn.Player; // El jugador comienza
        currentIndex = 0;          // Reiniciar el índice de la computadora
        selectedNumbers.Clear();   // Limpiar los números seleccionados
        InitializeCubes();         // Inicializar y mostrar los cubos
    }

    // ========================
    // Reiniciar el Juego
    // ========================
    void RestartGame()
    {
        Debug.Log("Reiniciando el juego...");
        StartGame();
    }

    // ========================
    // Generar un Número Aleatorio
    // ========================
    int GenerateRandomNumber()
    {
        int randomNumber = Random.Range(0, 10);
        Debug.Assert(randomNumber >= 0 && randomNumber <= 9, "El número generado está fuera del rango 0-9.");
        return randomNumber;
    }

    // ========================
    // Turno del Jugador
    // ========================
    void HandlePlayerTurn()
    {
        int number = CheckKeyInput();
        if (number != -1 && !selectedNumbers.Contains(number)) // Si el jugador selecciona un número válido y no repetido
        {
            Debug.Log($"El jugador seleccionó: {number}");

            selectedNumbers.Add(number); // Registrar el número como seleccionado

            // Verificar si el número coincide con el objetivo
            if (number == targetNumber)
            {
                CambiarColorCubo(number, colorVerde);
                Debug.Log("¡Correcto! El jugador adivinó el número. ¡Fin del juego!");
                EndGame();

                SceneManager.LoadScene("TerrenoFacil");
            }
            else
            {
                CambiarColorCubo(number, colorRojo);
                Debug.Log("Número incorrecto. Turno de la computadora...");
                currentTurn = Turn.Computer;
                Invoke("HandleComputerTurn", 1.0f); // Retraso antes del turno de la computadora
            }
        }
        else if (number != -1)
        {
            Debug.Log($"El número {number} ya fue seleccionado. Intenta con otro número.");
        }
    }

    // ========================
    // Turno de la Computadora
    // ========================
    void HandleComputerTurn()
    {
        // Encontrar el siguiente número no seleccionado
        while (selectedNumbers.Contains(currentIndex) && currentIndex < cubos.Length)
        {
            currentIndex++;
        }

        if (currentIndex < cubos.Length) // Asegurarse de que hay números disponibles
        {
            Debug.Log($"La computadora seleccionó: {currentIndex}");

            selectedNumbers.Add(currentIndex); // Registrar el número como seleccionado

            // Verificar si el número coincide con el objetivo
            if (currentIndex == targetNumber)
            {
                CambiarColorCubo(currentIndex, colorAmarillo);
                Debug.Log("¡La computadora adivinó el número! ¡Fin del juego!");
                EndGame();

                SceneManager.LoadScene("Inicio");
            }
            else
            {
                CambiarColorCubo(currentIndex, colorRojo);
                Debug.Log("La computadora falló. Turno del jugador...");
                currentIndex++; // Avanza al siguiente número
                currentTurn = Turn.Player; // Vuelve al turno del jugador
            }
        }
        else
        {
            Debug.Log("No hay más números disponibles. ¡Empate!");
            EndGame();
        }
    }

    // ========================
    // Métodos Auxiliares
    // ========================
    int CheckKeyInput()
    {
        for (int i = 0; i <= 9; i++) // Detectar teclas numéricas
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) || Input.GetKeyDown(KeyCode.Keypad0 + i))
                return i; // Retorna el número presionado
        }
        return -1; // Ninguna tecla presionada
    }

    void CambiarColorCubo(int index, Color color)
    {
        if (index >= 0 && index < cubos.Length)
        {
            Renderer renderer = cubos[index].GetComponent<Renderer>();
            if (renderer != null)
                renderer.material.color = color;
        }
    }

    void InitializeCubes()
    {
        for (int i = 0; i < cubos.Length; i++)
        {
            cubos[i].SetActive(true); // Asegurarse de que todos los cubos estén activos
            CambiarColorCubo(i, colorBlanco); // Establecer el color inicial de los cubos a blanco
        }
    }

    void EndGame()
    {
        Debug.Log("El juego ha terminado.");
        currentTurn = Turn.None; // Detener el flujo del juego temporalmente
        Invoke("RestartGame", 2.0f); // Reiniciar el juego después de 2 segundos
    }
}