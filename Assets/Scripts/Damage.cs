using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("player entered");
        //Debug.Log(collision.tag);
        if (collision.tag == "Player")  // is it important?
        {
            //Debug.Log("player entered");
            //GameManager.Instance.player.Knockback();

            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                GameManager.Instance.SubFruit();
                player.Knockback(transform.position.x);
            }
        }
    }
}
