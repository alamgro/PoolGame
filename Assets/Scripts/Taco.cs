using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[SelectionBase]
public class Taco : MonoBehaviour
{
    public Rigidbody whiteBallRb; //Rigidbody de la bola blanca
    public Transform tacoPos; //Transform del taco
    public float shootForce; //Fuerza de tiro
    public float forceMultiplier;
    public TextMeshProUGUI fuerzaTiroUI;
    public GameObject Panel;
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

            ForceAdjustment();

            if (Input.GetMouseButtonDown(0))
            {
                whiteBallRb.velocity = Vector3.zero; //Ponemos el velocity de la bola blanca en 0 cada que tiramos
                whiteBallRb.transform.rotation = tacoPos.transform.rotation; //rotar la bola en la misma direcci?n que apunta el taco
                whiteBallRb.AddForce(whiteBallRb.transform.forward.normalized * shootForce * whiteBallRb.mass, ForceMode.Impulse); //Dar impulso a la bola
            }
        }
        else
        {
            //Desactiva el taco si la bola blanca est? en movimiento
            tacoPos.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {

            if (Panel.activeInHierarchy)
                Panel.SetActive(false);
            else
                Panel.SetActive(true);
        }

    }

    private void LateUpdate()
    {
        if (GameManager.Manager.isCam3DActive)
        {
            MoveTacoOn3D();
        }
        else
        {
            MoveTacoTopDown();
        }
    }

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
            print(shootForce);
        }
    }

    private void MoveTacoTopDown()
    {
        Ray ray = camTopDown.ScreenPointToRay(Input.mousePosition); //ScreenPointToRay convierte una coordenada de la pantalla a un rayo
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            /*Esto no funciona porque hace el clamp en un cuadrado de 5x5, en vez de un área circular de radio 5
            float clampedX, clampedZ;
            clampedX = Mathf.Clamp(hit.point.x, whiteBallRb.transform.position.x - 5f, whiteBallRb.transform.position.x + 5f);
            clampedZ = Mathf.Clamp(hit.point.z, whiteBallRb.transform.position.z - 5f, whiteBallRb.transform.position.z + 5f);
            transform.position = new Vector3(clampedX, 1f, clampedZ); //Mueve el taco a la posici?n del mouse, pero Y siempre es fija
            */

            //Aquí se calcula un vector que limita el movimiento del taco en un área circular
            Vector3 allowedPos = new Vector3(hit.point.x, 1f, hit.point.z) - whiteBallRb.transform.position;
            allowedPos = Vector3.ClampMagnitude(allowedPos, 5f);
            tacoPos.transform.position = whiteBallRb.transform.position + allowedPos;

            //transform.position = new Vector3(hit.point.x, 1f, hit.point.z); //Mueve el taco a la posici?n del mouse, pero Y siempre es fija
        }
    }

    private void MoveTacoOn3D()
    {
        Vector3 allowedPos = new Vector3(cam3D.transform.position.x, 1f, cam3D.transform.position.z) - whiteBallRb.transform.position;
        allowedPos = Vector3.ClampMagnitude(allowedPos, 5f);
        tacoPos.transform.position = whiteBallRb.transform.position + allowedPos;
    }
}
