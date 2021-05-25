using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region SINGLETON MANAGER
    private static GameManager _instance;
    public static GameManager Manager { get { return _instance; } }
    #endregion
    public string escena;
    public GameObject cam3D;
    public GameObject cam3DController;

    public bool isCam3DActive = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (cam3D)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCam3DActive = !cam3D.activeInHierarchy;
                cam3D.SetActive(isCam3DActive);
                //Cursor.visible = !isCam3DActive; //Si la cámara 3D está desactivada, se activa el cursor

                if (isCam3DActive)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
        

        /*
        //Esto es para poder mover la cámara 3D solo si presionamos click derecho
        if (Input.GetMouseButtonDown(1))
        {
            cam3DController.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            cam3DController.SetActive(false);
        }*/
    }

    //Stop a ball or gameobject with Rigidbody from moving
    public void StopBall(Rigidbody _rb)
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.Sleep();
    }

    public void CambioEscena() 
    {
        SceneManager.LoadScene(escena);
    }

    

   

}
