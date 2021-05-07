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
        if (other.CompareTag("bola"))
        {
            other.gameObject.SetActive(false);
            bolas--;
            print("menos 1");
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
        if (other.CompareTag("bolaB"))0 
        {
            other.gameObject.transform.position = new Vector3(0.0f,1.0f,0.0f);
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            print("Ups");
        }

    }
}
