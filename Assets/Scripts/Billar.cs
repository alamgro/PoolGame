using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Billar : MonoBehaviour
{
    private int bolas;
    // Start is called before the first frame update
    void Start()
    {
        bolas = 14;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bolasChi"))
        {
            other.gameObject.SetActive(false);
            bolas--;
            //print(other + "chicas");

        }
        if (other.CompareTag("bolasGra"))
        {
            other.gameObject.SetActive(false);
            bolas--;
            //print(other + "grandes");
        }

        if (other.CompareTag("bola8") && bolas == 0)
        {
            other.gameObject.SetActive(false);
            SceneManager.LoadScene("Looby");
            print("Ganaste");

        }
        if(other.CompareTag("bola8") && bolas > 0)
        {
            other.gameObject.SetActive(false);
            SceneManager.LoadScene("Alex");
            print("Perdiste");
        }
        if (other.CompareTag("bolaB"))
        {
            GameManager.Manager.StopBall(other.GetComponent<Rigidbody>()); //Función para detener la bola
            other.gameObject.transform.position = Vector3.up;
            print("Ups");
        }

    }
}
