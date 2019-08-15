using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Sprite Button_NotPressed;
    [SerializeField] private Sprite Button_Pressed;
    
    [SerializeField] private GameObject FinalDoor;
    [SerializeField] private AudioClip OpenDoorSound;
    private int BoxLayer = 17;
    private SpriteRenderer ButtonSpriteRenderer;

    private AudioSource SoundPlayer;

    void Start()
    {
        ButtonSpriteRenderer = GetComponent<SpriteRenderer> ();
        ButtonSpriteRenderer.sprite = Button_NotPressed;
        SoundPlayer = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
      // если Box находится на кнопке, то меняем ее состояние и состояние финальной двери
        if (collision.gameObject.layer == BoxLayer) {
          ButtonSpriteRenderer.sprite = Button_Pressed;
          FinalDoor.GetComponent<SpriteRenderer>().sprite = FinalDoor.GetComponent<FinalDoor>().CloseDoor;
          FinalDoor.GetComponent<FinalDoor>().ButtonPressed = true;
          SoundPlayer.PlayOneShot(OpenDoorSound, 0.3f);
        }
    }
     void OnTriggerExit2D(Collider2D collision){
       // если Box был убран с кнопки, то меняем ее состояние и состояние финальной двери
        if (collision.gameObject.layer == BoxLayer) {
          ButtonSpriteRenderer.sprite = Button_NotPressed;
          FinalDoor.GetComponent<SpriteRenderer>().sprite = FinalDoor.GetComponent<FinalDoor>().CloseDoor2;
          FinalDoor.GetComponent<FinalDoor>().ButtonPressed = false;
          SoundPlayer.PlayOneShot(OpenDoorSound, 0.3f);
        }
    }
}
