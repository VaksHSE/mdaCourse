using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();

    [SerializeField] private bool canbereactivate;
    private bool active;
    private void Start()
    {
        //canbereactivate = GameManager.Instance.canbereactivate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (active)
                return;
            //Debug.Log("player entered");
            //GameManager.Instance.player.Knockback();

            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                AudioManager.instance.PlaySound(9);
                ActivateCheckpoint();
            }
        }
    }

    private void ActivateCheckpoint()
    {
        active = true;
        anim.SetTrigger("active");
        GameManager.Instance.UpdatePositionRespawn(gameObject.transform);
        
    }
}
