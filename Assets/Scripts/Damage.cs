using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Damage : Photon.MonoBehaviour
{   

    [SerializeField]
    private int damageamount = 4;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Slider slider;

    public int currentHealth;

    public GameObject player;

    public PhotonView pv;

    public GameManager gm;

    public VisualEffect spark;
    
    
    void Start()
    {
        pv = this.GetComponent<PhotonView>();
        currentHealth = 100;
        SetMaxHealth(currentHealth);
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    public void SetMaxHealth(int health)
    {   
        slider.maxValue = health;
        slider.value = health;
    }

    [PunRPC]
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    [PunRPC]
    public void TakeDamage(bool finish)
    {
        damageamount = 4;
        if (finish == true)
            damageamount = 10;
        anim.SetTrigger("damage");
        PlayVFX(spark, true);
        currentHealth -= damageamount;
        SetHealth(currentHealth);
        if (currentHealth <=0)
        {
            pv.RPC("Deadcontrolsref", PhotonTargets.AllBuffered);
            pv.RPC("Dead", PhotonTargets.AllBuffered);
            pv.RPC("DeadCameraRef", PhotonTargets.AllBuffered);
            if (photonView.isMine)
            {
                LoseText();
            }
            else
            {
                WinText();
            }
           
        }
    }

    [PunRPC]
    public void DeadCameraRef()
    {
        gm.DeadCamera();
    }

    [PunRPC]
    public void Dead()
    {
        player.SetActive(false);
    }

    [PunRPC]
    void Deadcontrolsref()
    {
        gm.DeadControls();
    }

    void WinText()
    {
        gm.wintext();
    }

    void LoseText()
    {
        gm.losetext();
    }

    public void PlayVFX(VisualEffect visualEffect, bool shakeCamera, float shakeAmplitude = 2, float shakeFrequency = 2, float shakeSustain = .2f)
    {
        if (visualEffect == spark)
            visualEffect.SetFloat("PosX", 5);
        visualEffect.SendEvent("OnPlay");
    }
}
