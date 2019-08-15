using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    [SerializeField] private Sprite OpenDoor;
    public Sprite CloseDoor;
    public Sprite CloseDoor2;
    private bool OpenCheck = false;
    public bool ButtonPressed = false;
    private int PlayerLayer = 12;
    private SpriteRenderer DoorSpriteRenderer;
    private AudioSource SoundPlayer;

    [SerializeField] private AudioClip LockDoorSound;
    [SerializeField] private AudioClip OpenDoorSound;

    void Start()
    {
      SoundPlayer = gameObject.GetComponent<AudioSource>();
      DoorSpriteRenderer = GetComponent<SpriteRenderer> ();
      DoorSpriteRenderer.sprite = CloseDoor2;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
    // проверка взаимодействия игрока и двери, она открывается, если найдены все три ключа и активирована кнопка 
    if (collision.gameObject.layer == PlayerLayer) {
      if (collision.gameObject.GetComponent<Player>().Keys == 3 && ButtonPressed == true)
      {
        DoorSpriteRenderer.sprite = OpenDoor;
        if(OpenCheck == false)
        {
          SoundPlayer.PlayOneShot(OpenDoorSound);
        }
        OpenCheck = true;
      }
      else
      {
        SoundPlayer.PlayOneShot(LockDoorSound);
      }
    }
  }
}
