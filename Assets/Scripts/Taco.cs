using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taco : MonoBehaviour
{
    public GameObject whiteBall;
    public Transform targetPos;
    public float shootForce;

    private Camera cam;

    IEnumerator Start()
    {
        cam = Camera.main;
        yield return new WaitForSeconds(1f);
        whiteBall.GetComponent<Rigidbody>().AddForce(whiteBall.transform.forward.normalized * shootForce, ForceMode.Force);
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //ScreenPointToRay convierte una coordenada de la pantalla a un rayo
        //Dispara el rayo
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = new Vector3(hit.point.x, 1f, hit.point.z);
            print("ESTOY ENTRANDO WEEEEE");
        }

        transform.LookAt(whiteBall.transform, Vector3.up);
        
        //transform.RotateAround(whiteBall.transform.position, Vector3.up, 20 * Time.deltaTime);

    }
}
