using System;
using UnityEngine;

public class SelectGadget : MonoBehaviour
{
    public GameObject Growth;

    private void Start()
    {
        GetComponent<Gadget_Manager>().Equip(Growth, 1);
    }
}
