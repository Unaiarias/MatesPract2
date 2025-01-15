using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISalir : MonoBehaviour
{
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salió");
    }
}
