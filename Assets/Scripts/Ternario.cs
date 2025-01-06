using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ternario : MonoBehaviour
{

    public int Resultado;

    int[] numeros = new int[9];

    public GameObject[] cubos = new GameObject[9];
    public Material materialCorrecto, materialIncorrecto;

    public void Dificil()
    {
        SceneManager.LoadScene("MiniJuegoDificil");
    }
    void Start()
    {
        

        int LimitInf = 0;
        int LimitSup = 8;

        
        numeros[0] = 1;
        numeros[1] = 2;
        numeros[2] = 3;
        numeros[3] = 4;
        numeros[4] = 5;
        numeros[5] = 6;
        numeros[6] = 7;
        numeros[7] = 8;
        numeros[8] = 9;

        
        for (int i = 0; LimitInf <= LimitSup; i++)
        {
           
            int Minf = LimitInf + (LimitSup - LimitInf) / 3;
            int Msup = LimitSup - (LimitSup - LimitInf) / 3;

            
            if (numeros[Minf] == Resultado)
            {
                cubos[Minf].GetComponent<Renderer>().material = materialCorrecto;
                Debug.Log("Valor encontrado en Minf: " + Minf);
                return; 
            }
            
            else if (numeros[Msup] == Resultado)
            {
                cubos[Msup].GetComponent<Renderer>().material = materialCorrecto;
                Debug.Log("Valor encontrado en Msup: " + Msup);
                return; 
            }
            
            else if (Resultado < numeros[Minf])
            {
                cubos[Minf].GetComponent<Renderer>().material = materialIncorrecto;
                LimitSup = Minf - 1;
            }
           
            else if (Resultado > numeros[Msup])
            {
                cubos[Msup].GetComponent<Renderer>().material = materialIncorrecto;
                LimitInf = Msup + 1;
            }
         
            else
            {
                LimitInf = Minf + 1;
                LimitSup = Msup - 1;
            }

            
            if (LimitInf > LimitSup)
            {
                Debug.Log("Valor no encontrado.");
            }
        }
    }      
    
}
