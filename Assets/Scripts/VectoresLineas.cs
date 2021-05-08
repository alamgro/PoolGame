using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectoresLineas : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    //private Transform posInicio;
    //private Transform posBola;
    private Transform dirBola;
    [SerializeField]
    private float maxDistancia;
    public LayerMask Bola;
    public LayerMask bolaPropia;

    public Transform[] hoyos;
    public Transform posMax;


    // Start is called before the first frame update
    void Start()
    {
       lineRenderer.positionCount = 1; //un maximo de pres puntos
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward)); 

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistancia, Bola))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.cyan);
            //Debug.Log("Did Hit");
            lineRenderer.positionCount++; //la pelota+++++++++++2

            lineRenderer.SetPosition(0, transform.position); //inicio
            lineRenderer.SetPosition(1, hit.point);  //a pelota
            //lineRenderer.positionCount--;
            Ray ray2 = new Ray(hit.transform.position, transform.TransformDirection(Vector3.forward)); 

            if (Physics.Raycast(ray2, out RaycastHit hit2, maxDistancia, Bola))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit2.distance, Color.red);
                lineRenderer.positionCount++;
                lineRenderer.positionCount++;
                lineRenderer.positionCount++;



                lineRenderer.SetPosition(2, hit2.point);
                lineRenderer.SetPosition(3, hit2.transform.position);
                lineRenderer.SetPosition(4, hoyos[0].position);

                CalculoVector90(hoyos[0], hit2.transform, posMax);
            }
            else
            {
                lineRenderer.positionCount = 2;
                
            }

        }
        else
            lineRenderer.positionCount = 1;

        
    }
    void CalculoVector90(Transform _hoyoOrigin, Transform _bolaTarget, Transform posMax )
    {
        //Vector3 temp;
       // Collider max;
        Ray ray = new Ray(_hoyoOrigin.position, _bolaTarget.TransformDirection(Vector3.forward));

        if(Physics.Raycast(ray, out RaycastHit hit, maxDistancia, Bola))
        {
            Debug.DrawRay(_hoyoOrigin.position, _bolaTarget.TransformDirection(Vector3.forward) * hit.distance, Color.cyan);

            posMax.position = hit.collider.bounds.max;
            print("ya se movio el max");

        }

    }

}
