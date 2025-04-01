using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject pickupVFX;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("player entered");
        //Debug.Log(collision.tag);
        if (collision.tag == "Player")  // is it important?
        {
            //Debug.Log("player entered");
            GameManager.Instance.PickFruit();
            AudioManager.instance.PlaySound(0, true);
            //anim.SetTrigger("picked");
            GameObject newFx = Instantiate(pickupVFX, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private void GG()
    {
        Destroy(gameObject);
    }

}
