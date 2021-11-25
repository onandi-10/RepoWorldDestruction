using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour
{
    public Text vida;
    public int health = 100;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        vida.text = "Vida" + health.ToString();

        if (health < 0)
        {
           SceneManager.LoadScene("Final");
        }
    }
}