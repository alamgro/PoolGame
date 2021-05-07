using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectoresLineas : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    //private Transform posInicio;
    private Transform posBola;
    private Transform dirBola;
    [SerializeField]
    private float maxDistancia;
    public LayerMask Bola;
    public LayerMask bolaPropia;


    // Start is called before the first frame update
    void Start()
    {
       lineRenderer.positionCount = 4; //un maximo de pres puntos
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward)); 

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if ((Physics.Raycast(ray, out hit, maxDistancia, Bola)))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.cyan);
            //Debug.Log("Did Hit");
            lineRenderer.positionCount = 3; //un maximo de pres puntos

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            //lineRenderer.positionCount--;
            Ray ray2 = new Ray(hit.transform.position, transform.TransformDirection(Vector3.forward)); 
            RaycastHit hit2;

            if ((Physics.Raycast(ray2, out hit2, maxDistancia, Bola)))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit2.distance, Color.red);
                //lineRenderer.positionCount++;

                lineRenderer.SetPosition(2, hit2.point);

            }

        }

    }


}
