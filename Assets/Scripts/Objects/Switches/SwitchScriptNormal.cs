using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SwitchScriptNormal : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> gates;
    SpriteRenderer sprite;
    private enum DoorType { physical, spirit }
    [SerializeField]
    DoorType doorType;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            sprite.sprite = Resources.Load("Sprites/Buttons/Normal_Pressed", typeof(Sprite)) as Sprite;
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
