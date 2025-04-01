using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : MonoBehaviour
{

    [SerializeField] private float offDur;
    [SerializeField] private Trap_Fire_Buttom fireButton;

    [SerializeField] Animator anim;
    private BoxCollider2D fireCollider;
    private bool isActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        fireCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        if (fireButton == null)
            Debug.LogWarning("mda");

        SetFire(true);
    }
    private void SetFire(bool act)
    {
        anim.SetBool("active", act);
        fireCollider.enabled = act;
        isActive = act;
    }
    public void SwitchFire()
    {
        if (!isActive)
            return;
        StartCoroutine(FireCourtine());
    }
    private IEnumerator FireCourtine()
    {

        SetFire(false);
        yield return new WaitForSeconds(offDur);
        SetFire(true);
    }

    public bool GetActive()
    {
        return isActive;
    }
}
