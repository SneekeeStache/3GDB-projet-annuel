using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector2 PosInitiale;
    private bool dDestroy;
    private bool StopA = true;

    public Material materialCible; 

    public GameObject ColorBaseCible;

    public void Start()
    {
        PosInitiale = gameObject.transform.position;
    }

    //Les actions li�s � la souris
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
        //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/Sfx_Bouton_Clicker");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }


    //r�cup�rer l'�l�ment qu'on veut remplacer
    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("oui");
        if (collision.gameObject.tag == "ElementBase")
        {
            Debug.Log("ouioui");
            //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/Sfx_Construire_Drapeau");
            dDestroy = true;
            //Debug.Log("true");
            collision.gameObject.GetComponent<Image>().color = gameObject.GetComponent<Image>().color;
            ColorBaseCible = collision.gameObject;
        } //mettre l'image sur le pilier de base

    }
    
    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "ElementBase" && StopA)
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/Sfx_Construire_Porte");
            dDestroy = false;
            //Debug.Log("false");
            collision.gameObject.GetComponent<Image>().color = collision.gameObject.GetComponent<StockColorData>().colorDefaut;

        } //retirer l'image si pas mise
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //Debug.Log("OnEndDragn");
        if(dDestroy)
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/Sfx_Construire_Toit");
            ColorBaseCible.GetComponent<StockColorData>().Color = materialCible;
            //StopA = false;
            //Destroy(gameObject);
        }
        else gameObject.transform.position = PosInitiale;
        //liste des actions lorsqu'on met l'image sur le pilier
        
    }



}
