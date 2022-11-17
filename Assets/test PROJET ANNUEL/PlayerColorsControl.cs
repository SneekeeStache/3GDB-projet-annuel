using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;


public class PlayerColorsControl : MonoBehaviour
{
    [Header("Couleurs � l'�cran (Canvas)")]
    public GameObject Curseur;
    public bool ResetAnimCurseur = true;
    public List<CountElement> ColorList;
    public Material blanc;
    public Material bleu;
    public Material rose;
    
    public Material materialObjet;

    public float Color = 1f;
    public Text textColor;
    private Camera cameraFDP;

    /*[Header("Mob1 & et effets assign�s")]
    public Rigidbody projectilePrefab;
    public Transform firePoint;
    public float launchForce;

    public GameObject Mob1;
    private Animator Mob1Anim;
    private NavMeshAgent Mob1Nav;*/


    
    
    public GameObject ParticleSystemeClic;
    public Quaternion ParticleSystemeAngle;
    private Transform HitVector3;

    [Header("Menu, player et param�trage")]
    public GameObject Menu;
    public GameObject Player;

    void Start()
    {
        cameraFDP = Camera.main;
        Color = 1f;
        //Mob1Anim = Mob1.GetComponent<Animator>();
        //Mob1Nav = Mob1.GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) SceneManager.LoadScene("Test 3C");


        Ray ray = new Ray(transform.position, Vector3.forward);
        //Debug.DrawRay(transform.position, Vector3.forward, Color.blue, Mathf.Infinity);
        RaycastHit hit;

         if(Physics.Raycast(transform.position, cameraFDP.transform.forward, out hit, Mathf.Infinity))
        {
            //HitVector3.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            if (hit.transform.gameObject != null && hit.transform.gameObject.CompareTag("Mob1"))
            {
                                if (ResetAnimCurseur)
                                {
                                    Curseur.GetComponent<Animator>().Play("CurseurTake");
                                    //Debug.Log("bite");
                                    ResetAnimCurseur = false;
                                }

                                if (Input.GetMouseButtonDown(0))
                                {
                                    ResetAnimCurseur = true;
                                    Curseur.GetComponent<Animator>().Play("Idle");
                                    materialObjet = hit.transform.gameObject.GetComponent<MeshRenderer>().material;
                                    if (Color == 1f)
                                    {
                                        materialObjet.color = rose.color;
                                    }
                                    if (Color == 2f)
                                        {
                                        materialObjet.color = bleu.color;
                                    }
                                    if (Color == 3f)
                                    {
                                    materialObjet.color = blanc.color;
                                    }
                                }
                            }
                            else
                            {
                                ResetAnimCurseur = true;
                                Curseur.GetComponent<Animator>().Play("Idle");
                            }
            /*if (hit.transform.gameObject.CompareTag("Mob2") && Input.GetMouseButtonDown(1))
            {
                Mob1.GetComponent<StateMachineMob1>().Cible = hit.collider.gameObject;       
            }
            else if (Input.GetMouseButtonDown(1) && hit.transform.gameObject.CompareTag("Ground"))
            {
                Mob1.GetComponent<StateMachineMob1>().GoTo = true;
                Mob1.GetComponent<StateMachineMob1>().Cible = null;
                Mob1.GetComponent<StateMachineMob1>().GoTo = true;
                Mob1Nav.destination = hit.point;
                //Debug.Log("ass");
                //Debug.Log(hit.point);
                Instantiate(ParticleSystemeClic, hit.point, ParticleSystemeAngle);
                //new GameObject PSC = Instantiate(ParticleSystemeClic, new Vector3(hit.transform.position.x, hit.transform.position.x, hit.transform.position.x));
            }*/
        }
        

        //else ResetAnimCurseur = true;


            //float value = Input.GetAxis("mouseScrollWheel");

            if (Input.mouseScrollDelta.y >= 0f)
        {
            Color += 1;
        }
        if (Input.mouseScrollDelta.y <= 0f)
        {
            Color -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Color =1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Color = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Color = 3;
        }



        if (Color <= 0f)
        {
            Color = 3f;
        }
        if (Color >= 4f)
        {
            Color = 1f;
        }
        textColor.text = ""+Color;

        foreach (CountElement image in ColorList)
        {
            if (image.gameObject.GetComponent<CountElement>().Count == Color)
            {
                image.gameObject.SetActive(true);
            }
            else image.gameObject.SetActive(false);

        }

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            Rigidbody rb = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            rb.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);
        }*/

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Menu.activeSelf)
            {
                Menu.gameObject.SetActive(false);
                Player.GetComponent<Player>().LockCurseur = true;
                Time.timeScale = 1;
            }
            else
            {
                Menu.gameObject.SetActive(true);
                Player.GetComponent<Player>().LockCurseur = false;
                Time.timeScale = 0;
            }
        }

        /*        if (Input.GetKeyDown(KeyCode.A))
                {
                    Mob1Anim.Play("Dash", 0, 0.0f);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Mob1Anim.Play("Spin", 0, 0.0f);
                }*/
    }
    public void InstantPS()
    {
        Instantiate(ParticleSystemeClic, Player.transform.position, ParticleSystemeAngle);
    }
}
