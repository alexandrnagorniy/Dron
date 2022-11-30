using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public static PopupController Instance;
    public GameObject popup;
    public Text topText;
    public Text middleText;
    public GameObject[] buttonParents;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(string topValue, string midValue, int button) 
    {
        topText.text = topValue;
        middleText.text = midValue;

        popup.SetActive(true);

        if (buttonParents.Length - 1 <= button)
        {
            if (buttonParents[button] != null)
            {
                buttonParents[button].SetActive(true);
            }
        }
    }

    public void Close() 
    {
        foreach (var item in buttonParents)
        {
            item.SetActive(false);
        }
        popup.SetActive(false);
    }
}