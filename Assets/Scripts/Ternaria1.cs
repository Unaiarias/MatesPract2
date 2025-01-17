using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ternaria1 : MonoBehaviour
{
    private enum Turn { None, Player, Computer }
    private Turn currentTurn = Turn.None;
    private int targetNumber;           // Número aleatorio generado por el host (entre 0 y 9)
    private int low = 0;                // Límite inferior para la búsqueda ternaria
    private int high = 9;               // Límite superior para la búsqueda ternaria
    private int mid1, mid2;             // Divisores en la búsqueda ternaria
    private bool checkMid1 = true;      // Alternar entre mid1 y mid2

    public GameObject[] cubos;          // Referencia a los cubos (0-9)
    private Color colorVerde = Color.green;
    private Color colorRojo = Color.red;
    private Color colorAmarillo = Color.yellow;
    private Color colorBlanco = Color.white; // Color inicial de los cubos

    private HashSet<int> selectedNumbers = new HashSet<int>(); // Números ya seleccionados

    public void PlayGameHard()
    {
        SceneManager.LoadScene("MiniJuegoDificil");
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
        low = 0;                   // Reiniciar límite inferior
        high = 9;                  // Reiniciar límite superior
        checkMid1 = true;          // Iniciar con mid1
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
        if (number != -1)
        {
            if (!selectedNumbers.Contains(number)) // Verificar si el número ya fue seleccionado
            {
                Debug.Log($"El jugador seleccionó: {number}");

                selectedNumbers.Add(number); // Registrar el número como seleccionado

                // Verificar si el número coincide con el objetivo
                if (number == targetNumber)
                {
                    CambiarColorCubo(number, colorVerde);
                    Debug.Log("¡Correcto! El jugador adivinó el número. ¡Fin del juego!");
                    EndGame();

                    SceneManager.LoadScene("TerrenoDificil");
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
    // Turno de la Computadora (Búsqueda Ternaria)
    // ========================
    void HandleComputerTurn()
    {
        if (low > high)
        {
            Debug.Log("Error: límites inválidos en la búsqueda ternaria.");
            EndGame();
            return;
        }

        // Calcular los dos puntos intermedios
        mid1 = low + (high - low) / 3;
        mid2 = high - (high - low) / 3;

        if (checkMid1)
        {
            // Asegurarse de que mid1 no esté seleccionado
            while (selectedNumbers.Contains(mid1) && mid1 <= high)
            {
                Debug.Log($"mid1: {mid1} ya fue seleccionado. Ajustando...");
                mid1++;
            }

            if (mid1 <= high)
            {
                Debug.Log($"La computadora seleccionó mid1: {mid1}");
                selectedNumbers.Add(mid1); // Registrar el número como seleccionado
                CambiarColorCubo(mid1, colorRojo);

                if (mid1 == targetNumber)
                {
                    CambiarColorCubo(mid1, colorAmarillo);
                    Debug.Log("¡La computadora adivinó el número en mid1! ¡Fin del juego!");
                    EndGame();

                    SceneManager.LoadScene("Inicio");

                    return;
                }
            }

            checkMid1 = false; // Cambiar al próximo turno para seleccionar mid2
        }
        else
        {
            // Asegurarse de que mid2 no esté seleccionado
            while (selectedNumbers.Contains(mid2) && mid2 >= low)
            {
                Debug.Log($"mid2: {mid2} ya fue seleccionado. Ajustando...");
                mid2--;
            }

            if (mid2 >= low)
            {
                Debug.Log($"La computadora seleccionó mid2: {mid2}");
                selectedNumbers.Add(mid2); // Registrar el número como seleccionado
                CambiarColorCubo(mid2, colorRojo);

                if (mid2 == targetNumber)
                {
                    CambiarColorCubo(mid2, colorAmarillo);
                    Debug.Log("¡La computadora adivinó el número en mid2! ¡Fin del juego!");
                    EndGame();

                    SceneManager.LoadScene("Inicio");

                    return;
                }
            }

            // Ajustar los límites de búsqueda
            if (targetNumber < mid1)
            {
                high = mid1 - 1; // Eliminar los números mayores o iguales a mid1
            }
            else if (targetNumber > mid2)
            {
                low = mid2 + 1; // Eliminar los números menores o iguales a mid2
            }
            else
            {
                low = mid1 + 1;
                high = mid2 - 1; // Foco entre mid1 y mid2
            }

            Debug.Log($"Nuevos límites: low = {low}, high = {high}");
            checkMid1 = true; // Volver a mid1 en el próximo turno
        }

        // Pasar el turno al jugador después de evaluar
        currentTurn = Turn.Player;
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