using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite doorOpenVertical;
    [SerializeField] private Sprite doorCloseVertical;
    [SerializeField] private Sprite doorOpenHorizontal;
    [SerializeField] private Sprite doorCloseHorizontal;

    [SerializeField] private bool Opened = false;
    [SerializeField] private DoorOrientation doorOrientation = DoorOrientation.Vertical;

    private SpriteRenderer _doorSpriteCurrent;


    public void OpenDoor()
    {
        Opened = !Opened;
        GetComponent<Collider2D>().enabled = false;  

        DoorRender();
    }

    private void DoorRender()
    {
        if (Opened && doorOrientation == DoorOrientation.Vertical)
        {
            _doorSpriteCurrent.sprite = doorOpenVertical;
        }
        else if (!Opened && doorOrientation == DoorOrientation.Vertical)
        {
            _doorSpriteCurrent.sprite = doorCloseVertical;
        }
        else if (Opened && doorOrientation == DoorOrientation.Horizontal)
        {
            _doorSpriteCurrent.sprite = doorOpenHorizontal;
        }
        else if (!Opened && doorOrientation == DoorOrientation.Horizontal)
        {
            _doorSpriteCurrent.sprite = doorCloseHorizontal;
        }
    }

    private void Start()
    {
        DoorRender();
    }

    private void OnValidate()
    {
        if(_doorSpriteCurrent == null)
        {
            _doorSpriteCurrent = gameObject.GetComponentInChildren<SpriteRenderer>();
        }
    }
}

enum DoorOrientation
{
    Vertical,
    Horizontal
}
