using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Photon.MonoBehaviour
{
    [SerializeField]
    private bool fight = false;

    private bool finish = false;

    private void OnTriggerEnter(Collider collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target.gameObject.CompareTag("Player") && fight==true)
        {
            DisableDamage();
            if (!target.isMine)
            {
                target.RPC("TakeDamage", PhotonTargets.AllBuffered, finish);
            }
  
        }
    }

    public void EnableDamage()
    {
        fight = true;
        finish = false;
    }

    public void DisableDamage()
    {
        fight = false;
    }

    public void FinishDamage()
    {
        fight = true;
        finish = true;
    }
}
