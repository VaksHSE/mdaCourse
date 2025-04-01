using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_point : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();

    private bool active;
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            anim.SetTrigger("active");
        }
    }
}
