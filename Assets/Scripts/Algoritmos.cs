using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Algoritmos : MonoBehaviour
{
    public int Resultado = 9;


    int [] numeros = new int[9];

    public GameObject[] cubos = new GameObject[9];
    public Material materialCorrecto, materialIncorrecto;

    public void Facil()
    {
        SceneManager.LoadScene("MiniJuegoFacil");
    }

    void Start()
    {
        

        numeros[0] = 1;
        numeros[1] = 2;
        numeros[2] = 3;
        numeros[3] = 4;
        numeros[4] = 5;
        numeros[5] = 6;
        numeros[6] = 7;
        numeros[7] = 8;
        numeros[8] = 9;
        

        for (int i = 0; i < numeros.Length; i++)
        {
            if (numeros[i] == Resultado)
            {
                cubos[i].GetComponent<Renderer>().material = materialCorrecto;
                Debug.Log(numeros[i]);

            }
            else
            {
                cubos[i].GetComponent<Renderer>().material = materialIncorrecto;
                Debug.Log("Incorrecto");
            }
        }
    }

    
    void Update()
    {
       



    }





}
