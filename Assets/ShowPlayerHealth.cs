using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPlayerHealth : MonoBehaviour
{   public TMP_Text text;
    public PlayerHealth health;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        text.text = "Health: " + health.health.ToString();
    }
}
