﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attractionPipe : MonoBehaviour
{
    public GameObject FunnelPoint;
    [SerializeField] private float magnitude;
    public GameObject pipe;
    public TeamColor color;

    private void Start()
    {
        pipe = this.transform.parent.gameObject;
        color = pipe.GetComponent<Pipe>().color;
    }

    void OnTriggerStay (Collider col)
    {
        
        Debug.Log(col);
        if (col.tag == "Attractor")
        {
            Debug.Log("It's a boy");
            Player player = col.GetComponent<BaseObject>().player;
            Rigidbody colRigidbody = col.GetComponent<Rigidbody>();
            if (player == null) return;
            if (player.color == color)
            {
                if (colRigidbody != null)
                {
                    Debug.Log("I'm pulling!!!!!!!!!!!!!!");
                    Vector3 directionDraw = FunnelPoint.transform.position - colRigidbody.position;
                    Vector3 drawForce = directionDraw.normalized * magnitude;
                    colRigidbody.AddForce(drawForce);
                }
            }
            else
            {
                Debug.Log("Help, I'm not being succed");
            }
        }
        
    }
}
