using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour
{
    [Header("tiro da Arma")]
    public Transform PosicaoBala;
    public GameObject bullet;
      

    [Header("configs da arma")]
    public int bullets = 6;
    public int maxBullets = 6;
    public int ammo = 30;

    [Header("configs do Tiro")]
    public float range = 90;
    public float timeToShoot = 0.1f;
    public float timeToReload = 5f;
    public float damage = 30;

    [Header("tiro-canvas")]
    public TMP_Text textomunicao;

    float initialTimeToShoot;
    float initialTimeToReload;

    [Header("atirou quantaas vezes?")]
    int bulletFired;

    [Header("pode-atirar?")]
    bool canShoot;

    [Header("está-recarregando?")]
    bool reloading;

    // Start is called before the first frame update
    void Start()
    {
        initialTimeToReload = timeToReload;
        initialTimeToShoot = timeToShoot;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot && !reloading)
        {

            GameObject bulletInstance = Instantiate(bullet, PosicaoBala.position, PosicaoBala.rotation);
          

            bullets--;
            textomunicao.text = bullets.ToString();
            canShoot = false;
            bulletFired++;

        }

        //quando canshoot fica false, é pqr vc atirou, ai ele ativa o tempo de inicio pra poder atirar DNV
        //(FIRE-rate)
        if (!canShoot && bullets > 0) 
        
        {
            timeToShoot -= Time.deltaTime;

            if (timeToShoot <= 0)

            {
                timeToShoot = initialTimeToShoot;
                canShoot = true;
            
            
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && bullets < maxBullets && ammo > 0)
        { 
        
            reloading = true;

        }

        if (reloading) 
        {

            timeToReload -= Time.deltaTime;

            if (timeToReload <= 0)
            {
                int currentNeed = bulletFired;

                if (currentNeed < ammo)
                {

                    bullets = 6;
                    ammo = ammo - bulletFired;
                    bulletFired = bulletFired - bulletFired;
                }
                else 
                {

                    bullets = currentNeed;
                    ammo = 0;
                    bulletFired = bulletFired - bulletFired;
                }

                timeToReload = initialTimeToReload;
                reloading = false;
                textomunicao.text = bullets.ToString();

            }

        }
    }

   
}
