using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactua : MonoBehaviour
{
    public int numerodeEscena;

    public GameObject Texto;

    private bool lugar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && lugar == true)
        {
            SceneManager.LoadScene(numerodeEscena);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Texto.SetActive(true);
            lugar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Texto.SetActive(false);
            lugar = false;
        }
    }
}
