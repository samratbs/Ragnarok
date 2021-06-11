using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightingControls : Photon.MonoBehaviour
{   
    [SerializeField]
    private Animator anim;

    private Button Attackbtn;
    private Button Finishbtn;
    private Button Dodgebtn;
    public PhotonView photonview;

    public Attack damageToggle;


    void Start()
    {
        if (photonview.isMine)
        {
            Attackbtn = GameObject.FindGameObjectWithTag("AttackBtn").GetComponent<Button>();
            Finishbtn = GameObject.FindGameObjectWithTag("FinishBtn").GetComponent<Button>();
            Dodgebtn = GameObject.FindGameObjectWithTag("DodgeBtn").GetComponent<Button>();

            Attackbtn.onClick.AddListener(Attack);
            Finishbtn.onClick.AddListener(Finish);
            Dodgebtn.onClick.AddListener(Roll);
        }
    }

    public void Attack()
    {
        if (photonview.isMine)
            damageToggle.EnableDamage();
            anim.SetTrigger("attack");
    }
    public void Finish()
    {
        if (photonview.isMine)
            damageToggle.FinishDamage();
            anim.SetTrigger("finish");    

    }
    public void Roll()
    {
        if (photonview.isMine)
            damageToggle.DisableDamage();
            anim.SetTrigger("roll");
    }

}
