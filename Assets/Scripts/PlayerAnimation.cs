using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        Debug.Log(1223);
    }
    public void RinishRespawn()
    {
        Debug.Log(123);
        player.RespawnFinished(true);
        
    }
}
