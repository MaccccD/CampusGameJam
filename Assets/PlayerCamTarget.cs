using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamTarget : MonoBehaviour
{
    [Header("PLAYER:")]
    public bool followPlayer = true;
    public Vector3 offsetFromPlayer;
    [Header("TARGET:")]
    public Transform target;                    // This will override the player if not null.
    public Vector3 offsetFromTarget;


    private GameObject _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (followPlayer && !target)
            transform.position = _player.transform.position + offsetFromPlayer;
        else if (!followPlayer && target)
            transform.position = target.transform.position + offsetFromTarget;
        else if (followPlayer && target)
            transform.position = target.transform.position + offsetFromTarget;
        else
            transform.position = Vector3.zero + offsetFromTarget;
    }
}
