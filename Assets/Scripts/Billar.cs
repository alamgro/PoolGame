using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            print(other + "chicas");

        }
        if (other.CompareTag("bolasGra"))
        {
            other.gameObject.SetActive(false);
            bolas--;
            print(other + "grandes");

        }

        if (other.CompareTag("bola8") && bolas == 0)
        {
            other.gameObject.SetActive(false);
            print("Ganaste");

        }
        if(other.CompareTag("bola8") && bolas > 0)
        {
            other.gameObject.SetActive(false);
            print("Perdiste");
        }
        if (other.CompareTag("bolaB"))
        {
            other.gameObject.transform.position = Vector3.up;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            print("Ups");
        }

    }
}
