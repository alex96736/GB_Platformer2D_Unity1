using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    [SerializeField] private Sprite OpenDoor;
    [SerializeField] private Sprite CloseDoor;
    private bool OpenCheck = false;
    private int PlayerLayer = 12;
    private SpriteRenderer DoorSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        DoorSpriteRenderer = GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
    if (collision.gameObject.layer == PlayerLayer) {
      if (collision.gameObject.GetComponent<Player>().Keys == 3)
      {
        DoorSpriteRenderer.sprite = OpenDoor;
        OpenCheck = true;
      }
    }
  }
}
