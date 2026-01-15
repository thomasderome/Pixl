using UnityEngine;
using UnityEngine.InputSystem;

public class Gadget_Manager : MonoBehaviour
{
    public Gadget_Interface Gadget1;
    public Gadget_Interface Gadget2;
    public Gadget_Interface Gadget3;
    public Player _player;

    public void Equip(GameObject gadget, int slot)
    {
        if (slot == 1)
        {
            Gadget1 = Instantiate(gadget, transform).GetComponent<Gadget_Interface>();
            Gadget1.Init(_player);
        } else if (slot == 2)
        {
            Gadget2 = Instantiate(gadget, transform).GetComponent<Gadget_Interface>();
            Gadget2.Init(_player);
        } else if (slot == 3)
        {
            Gadget3 = Instantiate(gadget, transform).GetComponent<Gadget_Interface>();
            Gadget3.Init(_player);
        }
    }
    
    public void OnGadget1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Gadget1.Hold();
        } else if (context.canceled)
        {
            Gadget1.Trigger();
        }

    }
    public void OnGadget2(InputAction.CallbackContext context)
    {
    }
    public void OnGadget3(InputAction.CallbackContext context)
    {
    }
}