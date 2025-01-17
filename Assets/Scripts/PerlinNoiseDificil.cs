using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class PerlinNoiseDificil : MonoBehaviour
{
    public int width = 35;
    public int heigth = 35;
    public float scale = 20f;
    public float offsetX = 100f;
    public float offsetY = 100f;

    private float timeAux = 0.0f;

    private int valor1;
    private int valor;

    public GameObject Objeto;
    public GameObject Objeto2;
    public GameObject Spawner;
    public GameObject PowerUp;

    public float MiejeY;

    public float MiejeSpawn;

    void Start()
    {
        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);

        timeAux = Time.time;

        GenerateObstacles();


    }


    void Update()
    {

        if (Time.time - timeAux >= 1.2f)
        {
            //acciones
            GenerateObstacles();
            timeAux = Time.time;

        }
        offsetY++;

    }

    void GenerateObstacles()
    {

        for (int x = 0; x < width; x += 5)
        {

            int y = 0;
            // for (int y=0; y< heigth; y++)
            //{
            float color = CalculateColor(x, y);


            if (color < 0.4f)
            {

                valor1 = Random.Range(0, 100);

                if (valor1 <= 50f)
                {
                    GameObject clon = Instantiate(Objeto, new Vector3(x, 0, y), Quaternion.identity) as GameObject;


                }
                else if (valor1 >= 51f)
                {
                    GameObject clon = Instantiate(Objeto2, new Vector3(x, 0, y), Quaternion.identity) as GameObject;
                }

            }

            if (color > 0.4f)
            {
                valor = Random.Range(0, 100);

                if (valor < 27f)
                {
                    GameObject clon = Instantiate(Spawner, new Vector3(x, 0, y), Quaternion.identity) as GameObject;


                }
                else if (valor > 86f)
                {
                    GameObject clon = Instantiate(PowerUp, new Vector3(x, MiejeY, y), Quaternion.identity) as GameObject;
                }


            }

            //}
        }

    }

    float CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / heigth * scale + offsetY;

        float value = Mathf.PerlinNoise(xCoord, yCoord);
        return value;



    }
}
