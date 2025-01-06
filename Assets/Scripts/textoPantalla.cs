using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textoPantalla : MonoBehaviour
{
    public TMP_Text texto;
    int vidas;
    // Start is called before the first frame update
    void Start()
    {
        vidas = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //texto.text = "Vidas: "+vidas;
        texto.text = "Tiempo: "+Time.time.ToString("F0");
    }
}
