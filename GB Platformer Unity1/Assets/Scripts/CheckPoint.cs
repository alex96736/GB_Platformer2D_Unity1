﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Sprite TrueCheckPoint;
    [SerializeField] private Sprite FalseCheckPoint;
    private bool IsCheck = false;
    private int PlayerLayer = 12;
    private SpriteRenderer checkpointSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
    if (collision.gameObject.layer == PlayerLayer) {
      checkpointSpriteRenderer.sprite = TrueCheckPoint;
      IsCheck = true;
    }
  }
}