using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Binaria : MonoBehaviour
{

    public int Resultado;


    int[] numeros = new int[9];
    
    public GameObject[] cubos = new GameObject[9];

    
    public Material materialCorrecto, materialIncorrecto;

    public void Medio()
    {
        SceneManager.LoadScene("MiniJuegoMedio");
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

        for (int i = 0; i < numeros.Length; i++)
        {
            if (LimitInf < LimitSup)
            {
                int valor = LimitSup + LimitInf;
                int PosCen = valor / 2;

                if (numeros[PosCen] < Resultado)
                {
                    LimitInf = PosCen + 1;
                }

                if (numeros[PosCen] > Resultado)
                {
                    LimitSup = PosCen - 1;
                }

                if (numeros[PosCen] == Resultado)
                {
                    cubos[PosCen].GetComponent<Renderer>().material = materialCorrecto;
                    Debug.Log("CORRECTO");
                    break;
                }
                else
                {
                    cubos[PosCen].GetComponent<Renderer>().material = materialIncorrecto;
                    Debug.Log("INCORRECTO");

                }
            }



        }
           

       
    }
        

    void Update()
    {




    }

}
