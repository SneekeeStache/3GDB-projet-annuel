using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsComponentController : MonoBehaviour
{

    public GameObject Menu;
    public Image ColorCoponent1;
    public Image ColorCoponent2;
    public Image ColorCoponent3;
    public Image ColorCoponent4;


    public GameObject CubeColor1;
    public GameObject CubeColor2;
    public GameObject CubeColor3;
    public GameObject CubeColor4;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Menu)
        {
            CubeColor1.GetComponent<MeshRenderer>().material = ColorCoponent1.gameObject.GetComponent<StockColorData>().Color;
            CubeColor2.GetComponent<MeshRenderer>().material = ColorCoponent2.gameObject.GetComponent<StockColorData>().Color;
            CubeColor3.GetComponent<MeshRenderer>().material = ColorCoponent3.gameObject.GetComponent<StockColorData>().Color;
            CubeColor4.GetComponent<MeshRenderer>().material = ColorCoponent4.gameObject.GetComponent<StockColorData>().Color;
        }
       
    }
}
