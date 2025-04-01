using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinema : MonoBehaviour
{
    private CinemachineVirtualCamera cinema;
    public static Cinema instance;
    private void Awake()
    {
        instance = this;
        cinema = GetComponent<CinemachineVirtualCamera>();
    }

    public void linkCinema(Transform player)
    {
        cinema.Follow = player;
        cinema.LookAt = player;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
