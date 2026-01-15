using UnityEngine;
using UnityEngine.UI;

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

    public void ConfirmSelection()
    {
        string finalLoadout = "";
        for (int i = 0; i < selectedGadgets.Length; i++)
        {
            GameObject gadget = selectedGadgets[i];
            int slot = i + 1;
            if (gadget.name == "Random")
            {
                GameObject randomPick = allGadgets[Random.Range(0, allGadgets.Length)];
                gadget = randomPick;
            }
            finalLoadout+= $"Slot {i+1}: {gadget.name} ";
        }
        Debug.Log(finalLoadout);
        
    }
    
    public void ResetToDefault()
    {
        foreach (Image slot in loadoutSlots)
            slot.sprite = defaultSprite;
    }


}

