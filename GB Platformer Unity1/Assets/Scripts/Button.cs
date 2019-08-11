using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Sprite Button_NotPressed;
    [SerializeField] private Sprite Button_Pressed;
    
    [SerializeField] private GameObject FinalDoor;
    private int BoxLayer = 17;
    private SpriteRenderer ButtonSpriteRenderer;

    void Start()
    {
        ButtonSpriteRenderer = GetComponent<SpriteRenderer> ();
        ButtonSpriteRenderer.sprite = Button_NotPressed;
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
        }
    }
     void OnTriggerExit2D(Collider2D collision){
       // если Box был убран с кнопки, то меняем ее состояние и состояние финальной двери
        if (collision.gameObject.layer == BoxLayer) {
          ButtonSpriteRenderer.sprite = Button_NotPressed;
          FinalDoor.GetComponent<SpriteRenderer>().sprite = FinalDoor.GetComponent<FinalDoor>().CloseDoor2;
          FinalDoor.GetComponent<FinalDoor>().ButtonPressed = false;
        }
    }
}
