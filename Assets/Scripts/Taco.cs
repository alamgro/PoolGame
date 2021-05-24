using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[SelectionBase]
public class Taco : MonoBehaviour
{
    public Rigidbody whiteBallRb; //Rigidbody de la bola blanca
    public Transform tacoPos; //Transform del taco
    public GameObject predictionBall;
    public float shootForce; //Fuerza de tiro
    public float forceMultiplier;
    public TextMeshProUGUI fuerzaTiroUI;
    public GameObject controlsPanel;
    private Camera camTopDown; //Main Camera
    private Camera cam3D;
    
    private void Awake()
    {
        camTopDown = Camera.main;
        cam3D = GameObject.FindGameObjectWithTag("Cam3D").GetComponent<Camera>();
    }
    void Start()
    {

        fuerzaTiroUI.text = "Fuerza de tiro: " + (int)shootForce + "N";
        //whiteBall.GetComponent<Rigidbody>().AddForce(whiteBall.transform.forward.normalized * shootForce, ForceMode.Force);
        // print("Game started :D");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // print("Stop white ball");
            //whiteBallRb.velocity = Vector3.zero;
            whiteBallRb.isKinematic = true;
            whiteBallRb.isKinematic = false;
        }

        //Si la bola blanca est? detenida, entonces puede interactuar con ella
        if (whiteBallRb.velocity.magnitude <= 0.01f)
        {
            tacoPos.gameObject.SetActive(true);

            tacoPos.transform.LookAt(whiteBallRb.transform, Vector3.up); //Mirar con el taco hacia la bola blanca

            ForceAdjustment(); //Detectar el ajuste de la fuerza con el Scroll del mouse

            if (Input.GetMouseButtonDown(0))
            {
                whiteBallRb.transform.rotation = tacoPos.transform.rotation; //rotar la bola en la misma direcci?n que apunta el taco
                whiteBallRb.velocity = Vector3.zero; //Ponemos el velocity de la bola blanca en 0 cada que tiramos
                whiteBallRb.AddForce(whiteBallRb.transform.forward.normalized * shootForce * whiteBallRb.mass, ForceMode.Impulse); //Dar impulso a la bola
            }
        }
        else
        {
            //Desactiva el taco y la bola de predicción si la bola blanca está en movimiento
            tacoPos.gameObject.SetActive(false);
            predictionBall.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (controlsPanel.activeInHierarchy)
                controlsPanel.SetActive(false);
            else
                controlsPanel.SetActive(true);
        }

    }

    private void LateUpdate()
    {
        #region DETECT CAM - 3D OR 2D
        if (GameManager.Manager.isCam3DActive)
        {
            //Mover el taco en entorno 3D
            MoveTaco(cam3D.transform.position);
        }
        else
        {
            //Mover el taco en entorno 2D - TopDown
            MoveTaco(camTopDown.ScreenToWorldPoint(Input.mousePosition));
        }
        #endregion
    }

    //Ajustar la fuerza de la bola con la rueda del mouse
    private void ForceAdjustment()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            shootForce += (Input.GetAxis("Mouse ScrollWheel") * forceMultiplier);
            if (shootForce < 1.0f)
                shootForce = 1.0f;
            else if (shootForce > 100.0f)
                shootForce = 100.0f;
            fuerzaTiroUI.text = "Fuerza de tiro: " + (int)shootForce + "N";
        }
    }

    private void MoveTaco(Vector3 _targetPos)
    {
        Vector3 allowedPos = new Vector3(_targetPos.x, 1f, _targetPos.z) - whiteBallRb.transform.position;
        //allowedPos = Vector3.ClampMagnitude(allowedPos, 5f);
        allowedPos = ClampMagnitude(allowedPos, 4.5f, 7f);
        tacoPos.transform.position = whiteBallRb.transform.position + allowedPos;
    }

    public static Vector3 ClampMagnitude(Vector3 _vectorToClamp, float minMagnitude, float maxMagnitude)
    {
        float vecMagnitude = _vectorToClamp.magnitude;
        if (vecMagnitude < minMagnitude)
        {
            Vector3 vecNormalized = _vectorToClamp / vecMagnitude; //equivalent to _vectorToClamp.normalized, but slightly faster in this case
            return vecNormalized * minMagnitude;
        }
        else if (vecMagnitude > maxMagnitude)
        {
            Vector3 vecNormalized = _vectorToClamp / vecMagnitude; //equivalent to _vectorToClamp.normalized, but slightly faster in this case
            return vecNormalized * maxMagnitude;
        }

        // No need to clamp at all
        return _vectorToClamp;
    }
}
