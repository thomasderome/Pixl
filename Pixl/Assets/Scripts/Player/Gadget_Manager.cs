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
        GameObject Temp;
        if (slot == 1)
        {
            Temp = Instantiate(gadget);
            Temp.transform.SetParent(transform);
            Temp.transform.localPosition = new Vector3(-0.5f, 0.5f, 0);
            
            Gadget1 = Temp.GetComponent<Gadget_Interface>();
            Gadget1.Init(_player);
        } else if (slot == 2)
        {
            Temp = Instantiate(gadget);
            Temp.transform.SetParent(transform);
            Temp.transform.localPosition = new Vector3(-0.13f, 0.65f, 0);
            
            Gadget2 = Temp.GetComponent<Gadget_Interface>();
            Gadget2.Init(_player);
        } else if (slot == 3)
        {
            Temp = Instantiate(gadget);
            Temp.transform.SetParent(transform);
            Temp.transform.localPosition = new Vector3(0.3f, 0.54f, 0);
            
            Gadget3 = Temp.GetComponent<Gadget_Interface>();
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
        if (context.started)
        {
            Gadget1.Hold();
        } else if (context.canceled)
        {
            Gadget1.Trigger();
        }
    }
    public void OnGadget3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Gadget1.Hold();
        } else if (context.canceled)
        {
            Gadget1.Trigger();
        }
    }
}