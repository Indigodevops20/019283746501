using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class Loadingbar : MonoBehaviour
{
    public Image Barra;
    public FACEBOOK facebook;
    public TextMeshProUGUI texto;
    public float Tiempo; //tiempo que durará la carga
    public float cantidad;
    public bool Listo;
    // Start is called before the first frame update
    void OnEnable()
    {
        Barra.fillAmount = 0;
        cantidad = 0;
        Listo = false;
    }
    // Update is called once per frame
    void Update()
    {
        barra();
    }

    void barra()
    {
        if (!Listo)
        {
            if (cantidad < Tiempo)
            {
                cantidad = cantidad + Time.deltaTime;
                texto.text = $"{Convert.ToInt32(100 * Barra.fillAmount).ToString()}% Loading";

                if (!PlayFabClientAPI.IsClientLoggedIn())
                {
                    texto.text = $"{Convert.ToInt32(100 * Barra.fillAmount).ToString()}% Verifying the account...";
                    if (Barra.fillAmount >= 0.6f)
                    {
                        texto.text = $"{Convert.ToInt32(100 * Barra.fillAmount).ToString()}% Failed to verify account";
                        texto.text = $"{Convert.ToInt32(100 * Barra.fillAmount).ToString()}% :C";
                        facebook.UI.error();
                        return;
                    }
                }
                else
                {
                    texto.text = $"{Convert.ToInt32(100 * Barra.fillAmount).ToString()}% Welcome to wild ones!";
                    texto.text = $"{Convert.ToInt32(100 * Barra.fillAmount).ToString()}% Cargando...";

                }
                Barra.fillAmount = cantidad / Tiempo;
            }
        }
        else
        {
            facebook.UI.success();
            
        }
        Listo = (Barra.fillAmount == 1) ? true : false;
    }
}
