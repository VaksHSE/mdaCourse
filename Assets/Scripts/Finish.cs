using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();

    //private bool active;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if (active)
            //    return;
            //Debug.Log("player entered");
            //GameManager.Instance.player.Knockback();

            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                AudioManager.instance.PlaySound(7);
                anim.SetTrigger("active");
                Debug.Log("WIN");
                GameManager.Instance.LevelFinished();
            }
        }
    }

    
}
