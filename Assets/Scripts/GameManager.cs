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
    public string Escena;
    public GameObject Panel;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Alex");
        }
    }

    public void CambioEscena() 
    {
        SceneManager.LoadScene(Escena);
    }
    public void Panelloco() 
    {
            if (Panel.activeInHierarchy)
                Panel.SetActive(false);
          
    }
    public void panel2() 
    {
          if(Panel.activeInHierarchy == false)
            Panel.SetActive(true);
    }
    

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
    
}
