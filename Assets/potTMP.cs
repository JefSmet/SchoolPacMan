using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class potTMP : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    SerialCommThreaded serialCommThreaded;
    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI= GetComponent<TextMeshProUGUI>();
        serialCommThreaded= FindAnyObjectByType<SerialCommThreaded>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshProUGUI.text = "potValue: "+ serialCommThreaded.PotValue.ToString();
    }
}
