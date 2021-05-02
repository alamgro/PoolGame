using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taco : MonoBehaviour
{
    public Rigidbody whiteBallRb; //Rigidbody de la bola blanca
    public float shootForce; //Fuerza de tiro

    private Camera cam; //Main Camera

    void Start()
    {
        cam = Camera.main;       
        //whiteBall.GetComponent<Rigidbody>().AddForce(whiteBall.transform.forward.normalized * shootForce, ForceMode.Force);
        print("Game started :D");
    }

    void Update()
    {
        transform.LookAt(whiteBallRb.transform, Vector3.up); //Mirar con el taco hacia la bola blanca
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //ScreenPointToRay convierte una coordenada de la pantalla a un rayo
        //Dispara el rayo
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = new Vector3(hit.point.x, 1f, hit.point.z); //Mueve el taco a la posición del mouse, pero Y siempre es fija
        }

        if (Input.GetMouseButtonDown(0))
        {
            whiteBallRb.velocity = Vector3.zero; //Ponemos el velocity de la bola blanca en 0 cada que tiramos
            whiteBallRb.transform.rotation = transform.rotation; //rotar la bola en la misma dirección que apunta el taco
            whiteBallRb.AddForce(whiteBallRb.transform.forward.normalized * shootForce * whiteBallRb.mass, ForceMode.Force);
        }
    }
}
