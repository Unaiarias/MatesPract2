using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlay : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Menu");
    }
}
