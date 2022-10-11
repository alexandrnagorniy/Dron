using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour
{
    public string key;
    private Text myText;
    // Start is called before the first frame update
    void Awake()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = LocalizationController.Instance.GetLanguageText(key);
    }
}
