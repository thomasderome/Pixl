using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectScript : MonoBehaviour
{
    public GameObject selectionPanel;
    private Image  currentTargetImage;
    public Image defaultImage;
    [SerializeField] private Image[] loadoutSlots;
    private Sprite defaultSprite;
    private GameObject[] selectedGadgets = new GameObject[3];
    [SerializeField] private GameObject[] allGadgets;
    private GameObject randomButton;
    
    void Start()
    {
        selectionPanel.SetActive(false);
        defaultSprite=loadoutSlots[0].sprite;
        randomButton = defaultImage.gameObject;
        for (int i = 0; i < selectedGadgets.Length; i++)
        {
            selectedGadgets[i] = randomButton;
            loadoutSlots[i].sprite = randomButton.GetComponent<Image>().sprite;
        }
    }
    
    public void OpenSelect()
    {
        selectionPanel.SetActive(true);
    }
    
    public void CloseSelect()
    {
        selectionPanel.SetActive(false);
    }
    
    public void SetCurrentTarget(Image target)
    {
        currentTargetImage = target;
    }
    
    public void SelectImage(Button button)
    {
        if (currentTargetImage == null)
            return;

        Image buttonImage = button.GetComponent<Image>();
        currentTargetImage.sprite = buttonImage.sprite;
        int slotIndex = System.Array.IndexOf(loadoutSlots, currentTargetImage);
        selectedGadgets[slotIndex]=button.gameObject;
        selectionPanel.SetActive(false);
    }

    public List<GameObject> ConfirmSelection()
    {
        List<GameObject> Gadget_sel = new List<GameObject>();
        for (int i = 0; i < selectedGadgets.Length; i++)
        {
            GameObject gadget = selectedGadgets[i];
            if (gadget.name == "Random") gadget = allGadgets[Random.Range(0, allGadgets.Length)];
            else
            {
                foreach (var _gadget in allGadgets)
                {
                    if (gadget.name == _gadget.name)
                    {
                        gadget = _gadget;
                        break;
                    }
                }
                if (gadget.GetComponent<Button>()) gadget = allGadgets[Random.Range(0, allGadgets.Length)];
            }
            Gadget_sel.Add(gadget);
        }

        return Gadget_sel;
    }
    
    public void ResetToDefault()
    {
        foreach (Image slot in loadoutSlots)
            slot.sprite = defaultSprite;
    }


}

