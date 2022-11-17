using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockColorData : MonoBehaviour
{
    public Color colorDefaut;
    public Material Color;

    void Start()
    {
        colorDefaut = gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
