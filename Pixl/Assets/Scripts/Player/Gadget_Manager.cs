using UnityEngine;
using UnityEngine.InputSystem;

public class Gadget_Manager : MonoBehaviour
{
    private Gadget_Interface Gadget1;
    private GameObject Gadget1_game;
    
    private Gadget_Interface Gadget2;
    private GameObject Gadget2_game;
    
    private Gadget_Interface Gadget3;
    private  GameObject Gadget3_game;
    
    public Player _player;

    private bool hide_gadget = false;
    public void Equip(GameObject gadget, int slot)
    {
        GameObject Temp;
        if (slot == 1)
        {
            Temp = Instantiate(gadget);
            Temp.transform.SetParent(transform);
            Temp.transform.localPosition = new Vector3(-0.5f, 0.5f, 0);

            Gadget1_game = Temp;
            Gadget1 = Temp.GetComponent<Gadget_Interface>();
            Gadget1.Init(_player);
        } else if (slot == 2)
        {
            Temp = Instantiate(gadget);
            Temp.transform.SetParent(transform);
            Temp.transform.localPosition = new Vector3(-0.13f, 0.65f, 0);
            
            Gadget2_game = Temp;
            Gadget2 = Temp.GetComponent<Gadget_Interface>();
            Gadget2.Init(_player);
        } else if (slot == 3)
        {
            Temp = Instantiate(gadget);
            Temp.transform.SetParent(transform);
            Temp.transform.localPosition = new Vector3(0.3f, 0.54f, 0);
            
            Gadget3_game = Temp;
            Gadget3 = Temp.GetComponent<Gadget_Interface>();
            Gadget3.Init(_player);
        }
    }
    
    public void OnGadget1(InputAction.CallbackContext context)
    {
        if (context.started && !hide_gadget)
        {
            Gadget1.Hold();
        } else if (context.canceled && !hide_gadget)
        {
            Gadget1.Trigger();
        }
    }
    public void OnGadget2(InputAction.CallbackContext context)
    {
        if (context.started && !hide_gadget)
        {
            Gadget2.Hold();
        } else if (context.canceled && !hide_gadget)
        {
            Gadget2.Trigger();
        }
    }
    public void OnGadget3(InputAction.CallbackContext context)
    {
        if (context.started && !hide_gadget)
        {
            Gadget3.Hold();
        } else if (context.canceled && !hide_gadget)
        {
            Gadget3.Trigger();
        }
    }

    public void Set_gadget(bool type, bool disable = true)
    {
        if (disable) hide_gadget = type;
        if (type)
        {
            Gadget1_game.GetComponent<SpriteRenderer>().enabled = false;
            Gadget2_game.GetComponent<SpriteRenderer>().enabled = false;
            Gadget3_game.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            Gadget1_game.GetComponent<SpriteRenderer>().enabled = true;
            Gadget2_game.GetComponent<SpriteRenderer>().enabled = true;
            Gadget3_game.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}