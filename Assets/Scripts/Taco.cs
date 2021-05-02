using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taco : MonoBehaviour
{
    public Rigidbody whiteBallRb;
    public float shootForce;

    private Camera cam;

    IEnumerator Start()
    {
        cam = Camera.main;
        yield return new WaitForSeconds(1f);
        //whiteBall.GetComponent<Rigidbody>().AddForce(whiteBall.transform.forward.normalized * shootForce, ForceMode.Force);
        print("Game start :D");
    }

    void Update()
    {
        transform.LookAt(whiteBallRb.transform, Vector3.up);
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //ScreenPointToRay convierte una coordenada de la pantalla a un rayo
        //Dispara el rayo
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = new Vector3(hit.point.x, 1f, hit.point.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Arreglar el vector, seguramente con inverse matrix se arregle.
            whiteBallRb.velocity = Vector3.zero;
            whiteBallRb.transform.rotation = transform.rotation;
            whiteBallRb.AddForce(whiteBallRb.transform.forward.normalized * shootForce, ForceMode.Force);

            //whiteBallRb.transform.LookAt(transform, Vector3.up);

            //direction = transform.worldToLocalMatrix.inverse * direction;

            //whiteBallRb.transform.rotation = Quaternion.Inverse(whiteBallRb.transform.rotation);

            //whiteBallRb.AddForce(whiteBallRb.transform.forward.normalized * shootForce, ForceMode.Force);
        }


    }
}
