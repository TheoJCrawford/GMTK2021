using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritSwitch : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> gates;
    SpriteRenderer sprite;
    private enum DoorType { physical, spirit}
    [SerializeField]
    DoorType doorType;
    private enum ButtonType { physical, spirit}
    [SerializeField]
    ButtonType buttonType;
    

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Spirit"))
        {

            sprite.sprite = Resources.Load("Sprites/Buttons/Spirit_Pressed", typeof(Sprite)) as Sprite;
            foreach (GameObject gate in gates)
            {
                BoxCollider2D gateCollider = gate.GetComponent<BoxCollider2D>();
                SpriteRenderer spriteRenderer = gate.GetComponent<SpriteRenderer>();
                if (doorType == DoorType.physical)
                {
                    spriteRenderer.sprite = Resources.Load("Sprites/Gates/Gate_Normal_Open", typeof(Sprite)) as Sprite;
                }
                else
                {
                    spriteRenderer.sprite = Resources.Load("Sprites/Gates/Gate_Spirit_Open", typeof(Sprite)) as Sprite;
                }
                gateCollider.enabled = false;
            }
            Debug.Log("Gate Disabled");
        }
    }
}
