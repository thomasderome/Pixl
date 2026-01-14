using UnityEngine;
using UnityEngine.UI;

public class SelectScript : MonoBehaviour
{
    public GameObject selectionPanel;
    private Image  currentTargetImage;
    private Image defaultImage;
    [SerializeField] private Image[] loadoutSlots;
    private Sprite defaultSprite;
    
    void Start()
    {
        selectionPanel.SetActive(false);
        defaultSprite=loadoutSlots[0].sprite;
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
        selectionPanel.SetActive(false);
        Debug.Log(buttonImage.name);
        
    }

    public void ResetToDefault()
    {
        foreach (Image slot in loadoutSlots)
            slot.sprite = defaultSprite;
    }


}

