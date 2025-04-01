using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float cooldown = 1;
    
    [SerializeField] private Transform[] wayPoint;

    [SerializeField] private Vector3[] wayPointPos;
    public int wayPointIndex = 1;
    public int moveDir = 1;
    private bool canMove = true;
    private Animator anim;
    private SpriteRenderer sr;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        UpdateWaypointsInfo();

        transform.position = wayPointPos[0];
    }

    private void UpdateWaypointsInfo()
    {
        wayPointPos = new Vector3[wayPoint.Length];
        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPointPos[i] = wayPoint[i].position;
        }
    }

    private void Update()
    {
        anim.SetBool("active", canMove);
        if (canMove == false)
            return;
        transform.position = Vector2.MoveTowards(transform.position, wayPointPos[wayPointIndex], moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPointPos[wayPointIndex]) < 0.1f)
        {
            //wayPointIndex = wayPointIndex % wayPoint.Length;
            if (wayPointIndex == wayPointPos.Length - 1 || wayPointIndex == 0)
            {
                //wayPointIndex = 0;
                moveDir *=  -1;
                StartCoroutine(StopMovement(cooldown));
            }

            wayPointIndex += moveDir;

        }
    }


    private IEnumerator StopMovement(float delay)
    {
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;
        sr.flipX = !sr.flipX;
    }
}
