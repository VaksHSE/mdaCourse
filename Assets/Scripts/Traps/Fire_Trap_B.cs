using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire_Buttom : MonoBehaviour
{
    // Start is called before the first frame update
    //private Trap_Fire trapFire;
    [SerializeField] private Trap_Fire trapFire;
    [SerializeField] Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //trapFire = GetComponentInParent<Trap_Fire>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            anim.SetTrigger("activate");
            //if(trapFire.GetActive())
            trapFire.SwitchFire();
        }
    }
}
