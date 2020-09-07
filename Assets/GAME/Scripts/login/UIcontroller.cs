using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontroller : MonoBehaviour
{
   

    public GameObject LoginPanel;
    public GameObject CustomPlayer;
    public GameObject InitPanel;
    public GameObject Client;

    //SI EL USUARIO SE LOGUEO CORRECTAMENTE ENTONCES QUE LO LLEVE A CREAR SU NOMBRE DE USUARIO
    public void LoginSucces()
    {
        LoginPanel.SetActive(false);
        CustomPlayer.SetActive(true);
    }

    //SI EL USUARIO YA CREO SU NOMBRE DE USUARIO ENTONCES QUE VAYA A LA SALA DE INICO
    public void CustomPlayerSuccess()
    {
        LoginPanel.SetActive(false);
        CustomPlayer.SetActive(false);
        InitPanel.SetActive(true);
    }

    //SI EL USUARIO YA ESTA LOGEADO Y CON NOMBRE CREADO ENTONCES QUE LO LLEVE A LA SALA DE INICIO

    public void LoggedIn()
    {
        LoginPanel.SetActive(false);
        InitPanel.SetActive(true);
        Debug.Log("welcome!");
    }

    // EL USUARIO NO SE PUDO LOGUEAR
    public void LoginError()
    {
        Debug.Log("Try Again!");
    }

    //SI EL USUARIO YA ESTA EN LA SALA DE INICIO ENTONCES QUE LE PERMITA IR AL CLIENTE
    public void InitClient()
    {
        InitPanel.SetActive(false);
        Client.SetActive(true);

    }

    //EL USUARIO CIERRA SESION SIN ESTAR EN EL CLIENTE
    public void LogoutFB()
    {
        FB.LogOut();
        InitPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
}
