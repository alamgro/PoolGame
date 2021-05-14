using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectoresLineas : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private float maxDistancia;
    public LayerMask Bola;
    public LayerMask BolaBlanca;
    public LayerMask SinBolaBlanca;

    public Transform[] hoyos;
    private Transform hoyoCercano;
    //public Transform posMax;

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward)); 

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistancia, BolaBlanca))//de Palo a bola blanca
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.cyan);
            lineRenderer.positionCount++; //la pelota+++++++++++2

            lineRenderer.SetPosition(0, transform.position); //inicio
            lineRenderer.SetPosition(1, hit.point);  //a pelota

            if (Physics.SphereCast(hit.transform.position, 0.42f, transform.TransformDirection(Vector3.forward), out RaycastHit hit2, maxDistancia, Bola))//De bola blanca a Bolas
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit2.distance, Color.red);
                /* lineRenderer.positionCount++;
                 lineRenderer.positionCount++;
                 lineRenderer.positionCount++;*/
                lineRenderer.positionCount = 5;
                HoyoMasCercano(hit.transform, hoyoCercano);

                lineRenderer.SetPosition(2, hit2.point);
                lineRenderer.SetPosition(3, hit2.transform.position);
                lineRenderer.SetPosition(4, hoyoCercano.position);

                CalculoVector90(hoyos[0], hit2.transform);
            }
            else
                lineRenderer.positionCount = 2;
        }
        else
            lineRenderer.positionCount = 1;
    }

    void CalculoVector90(Transform _hoyoOrigin, Transform _bolaTarget)
    {
        Debug.DrawRay(_hoyoOrigin.position, _bolaTarget.position - _hoyoOrigin.position, Color.blue);


        if ((Physics.Raycast(_hoyoOrigin.transform.position,  _bolaTarget.position - _hoyoOrigin.position, out RaycastHit hit, maxDistancia, Bola)))// de Hoyo a bola
        {
            //Debug.DrawRay(_hoyoOrigin.position, _bolaTarget.TransformDirection(Vector3.forward) * hit.distance, Color.black);


            if(Physics.Raycast(hit.transform.position, Vector3.Reflect(hit.normal, hit.normal), out RaycastHit hitBorde, maxDistancia, SinBolaBlanca)) //De bola tocada a pared
            {
                Debug.DrawRay(hit.transform.position, Vector3.Reflect(hit.normal, hit.normal)  * 35f, Color.yellow);
                //lineRenderer.SetPosition(5, hitBorde.point);
                lineRenderer.positionCount = 7;

                lineRenderer.SetPosition(5, hit.point);
                lineRenderer.SetPosition(6, hitBorde.point * 0.1f);


                /* if(Physics.Raycast(hitBorde.point,  _bolaTarget.position , out RaycastHit hitReversa, maxDistancia, Bola)) //de pared a bola
                 {
                     //es ese papi debe de apuntar a la ultima bola
                     Debug.DrawRay(hitBorde.point, _bolaTarget.position * hitReversa.distance, Color.red);

                 }*/
            }
        }
    }//void


    void HoyoMasCercano(Transform _peloActual, Transform _hoyoCercano)
    {
        float distancia = 0;
        float tmp;


        for (int i = 0; i < hoyos.Length; i++)
        {
            tmp = Vector3.Distance(_peloActual.position, hoyos[i].position);


            if(distancia <= tmp)
            {
                _hoyoCercano = hoyos[i];
            }
        }
    }
}//class
