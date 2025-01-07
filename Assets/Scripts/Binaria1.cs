using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Binaria1 : MonoBehaviour
{
    private enum Turn { None, Player, Computer }
    private Turn currentTurn = Turn.None;
    private int targetNumber;           // Número aleatorio generado por el host (entre 0 y 9)
    private int low = 0;                // Límite inferior para la búsqueda binaria
    private int high = 9;               // Límite superior para la búsqueda binaria

    public GameObject[] cubos;          // Referencia a los cubos (0-9)
    private Color colorVerde = Color.green;
    private Color colorRojo = Color.red;
    private Color colorAmarillo = Color.yellow;
    private Color colorBlanco = Color.white; // Color inicial de los cubos

    private HashSet<int> selectedNumbers = new HashSet<int>(); // Números ya seleccionados

    public void PlayGameMedium()
    {
        SceneManager.LoadScene("MiniJuegoMedio");
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (currentTurn == Turn.Player)
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
        low = 0;                   // Reiniciar límite inferior
        high = 9;                  // Reiniciar límite superior
        selectedNumbers.Clear();   // Limpiar los números seleccionados
        InitializeCubes();         // Inicializar y mostrar los cubos
    }

    // ========================
    // Turno del Jugador
    // ========================
    void HandlePlayerTurn()
    {
        int number = CheckKeyInput();
        if (number != -1) // Si el jugador presiona un número
        {
            if (!selectedNumbers.Contains(number)) // Verificar si el número no ha sido seleccionado
            {
                Debug.Log($"El jugador seleccionó: {number}");

                selectedNumbers.Add(number); // Registrar el número como seleccionado

                // Verificar si el número coincide con el objetivo
                if (number == targetNumber)
                {
                    CambiarColorCubo(number, colorVerde);
                    Debug.Log("¡Correcto! El jugador adivinó el número. ¡Fin del juego!");
                    EndGame();
                }
                else
                {
                    CambiarColorCubo(number, colorRojo);
                    Debug.Log("Número incorrecto. Turno de la computadora...");
                    currentTurn = Turn.Computer;
                    Invoke("HandleComputerTurn", 1.0f); // Retraso antes del turno de la computadora
                }
            }
            else
            {
                Debug.Log($"El número {number} ya fue seleccionado. Intenta con otro número.");
            }
        }
    }

    // ========================
    // Turno de la Computadora (Búsqueda Binaria mejorada)
    // ========================
    void HandleComputerTurn()
    {
        if (low > high)
        {
            Debug.Log("Error: límites inválidos en la búsqueda binaria.");
            EndGame();
            return;
        }

        int guess = -1;

        // Ajustar los límites y evitar números repetidos
        while (low <= high)
        {
            guess = (low + high) / 2;

            if (selectedNumbers.Contains(guess))
            {
                Debug.Log($"El número {guess} ya fue seleccionado. Ajustando límites...");

                // Ajustar límites dinámicamente si el número ya fue seleccionado
                if (guess < targetNumber)
                {
                    low = guess + 1;
                }
                else
                {
                    high = guess - 1;
                }
            }
            else
            {
                break; // Salir del bucle si el número no está repetido
            }
        }

        if (low > high) // Verificar si no quedan números válidos
        {
            Debug.Log("La computadora no tiene más números válidos para seleccionar.");
            EndGame();
            return;
        }

        Debug.Log($"La computadora seleccionó: {guess}");
        selectedNumbers.Add(guess); // Registrar el número como seleccionado

        // Verificar si el número coincide con el objetivo
        if (guess == targetNumber)
        {
            CambiarColorCubo(guess, colorAmarillo);
            Debug.Log("¡La computadora adivinó el número! ¡Fin del juego!");
            EndGame();
        }
        else
        {
            CambiarColorCubo(guess, colorRojo);

            // Ajustar los límites según el resultado
            if (guess < targetNumber)
            {
                Debug.Log($"El número {guess} es menor que el objetivo.");
                low = guess + 1; // Actualizar límite inferior
            }
            else
            {
                Debug.Log($"El número {guess} es mayor que el objetivo.");
                high = guess - 1; // Actualizar límite superior
            }

            Debug.Log("Turno del jugador...");
            currentTurn = Turn.Player; // Volver al turno del jugador
        }
    }

    // ========================
    // Generar un Número Aleatorio
    // ========================
    int GenerateRandomNumber()
    {
        int randomNumber = Random.Range(0, 10); // Genera un número entre 0 y 9
        Debug.Assert(randomNumber >= 0 && randomNumber <= 9, "El número generado está fuera del rango 0-9.");
        return randomNumber;
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
        Invoke("StartGame", 2.0f); // Reiniciar el juego después de 2 segundos
    }
}