using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class player : MonoBehaviour
{
    [Header("Temporaire")]
    public GameObject parapluieFerme;
    public GameObject parapluieOuvert;

    [Header("Composants à récupérer")]

    public Animator ParapluieRenderer;
    public GameObject OrietationJump;
    bool Collision = false;
    public bool fermer = false;
    Rigidbody rb;
    Animator animatorPlayer;

    private Transform cameraTransform;
    public TextMeshProUGUI NombreFlapText;

    [Header("Variables changeants les controles")]

    [SerializeField] public float NombreFlap;

    public float FlapingNumber;
    [SerializeField] float ForceJump;
    [SerializeField] float TimerOrientation;
    [SerializeField] float TimerStabilisation;
    [SerializeField] float drag = 7;
    [SerializeField] float dragFermer = 0;
    [SerializeField] float ImpulseOrientationPlayer;
    [SerializeField] float forceOrientationAnimation;
    [SerializeField] Vector3 OrientationVent = Vector3.zero;


    [SerializeField] Vector3 orientationModif;
    [SerializeField] Vector3 orientationAnim;

    private float timer = 0.3f;
    private float timerReset = 0.3f;
    public bool ActiveTimer;
    private bool onGroundFMOD = true;
    [HideInInspector] public bool onGround = false;
    [HideInInspector] public Vector3 groundPosition;


    void Start()
    {
        transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
        rb = GetComponent<Rigidbody>();
        animatorPlayer = GetComponent<Animator>();
        FlapingNumber = NombreFlap;
        NombreFlapText.text = FlapingNumber.ToString();
        cameraTransform = GameObject.Find("Main Camera").transform;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (onGround)
        {
            if (onGroundFMOD) FMODUnity.RuntimeManager.PlayOneShot("event:/player/landing");
            onGroundFMOD = false;
            transform.position = groundPosition;
            if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f || Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
            {
                if (Input.GetAxisRaw("Horizontal") > 0f)
                {
                    transform.RotateAround(groundPosition, -cameraTransform.forward, forceOrientationAnimation * 20 * Time.deltaTime);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0f)
                {
                    transform.RotateAround(groundPosition, cameraTransform.forward, forceOrientationAnimation * 20 * Time.deltaTime);
                }
                if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    transform.RotateAround(groundPosition, cameraTransform.right, forceOrientationAnimation * 20 * Time.deltaTime);
                }
                else if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    transform.RotateAround(groundPosition, -cameraTransform.right, forceOrientationAnimation * 20 * Time.deltaTime);
                }
            }
        }


        //tentative d'arreter le planage selon la rotaton ca marche pas
        if (gameObject.transform.rotation.x >= 40f || gameObject.transform.rotation.x <= -40f || gameObject.transform.localRotation.z >= 40f || gameObject.transform.localRotation.z <= -40f)
        {
            Debug.Log("Le parapluie tombe");
        }

        if ((Input.GetButtonDown("Jump") && FlapingNumber <= 0f) || (Input.GetButtonDown("Jump") && fermer)) FMODUnity.RuntimeManager.PlayOneShot("event:/player/no_flap");
        if (Input.GetButtonDown("Jump") && FlapingNumber >= 1f && !fermer)
        {
            onGround = false;
            onGroundFMOD = true;
            rb.AddForce((OrietationJump.transform.position - transform.position) * ForceJump, ForceMode.Impulse);
            parapluieFerme.SetActive(true);
            parapluieOuvert.SetActive(false);
            //ParapluieRenderer.Play("Fermeture");
            FlapingNumber = FlapingNumber - 1f;
            ActiveTimer = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/player/flap");
        }



        if (Input.GetButtonDown("Fire1") && ActiveTimer == false)
        {
            fermer = !fermer;
            if (fermer)
            {
                parapluieFerme.SetActive(true);
                parapluieOuvert.SetActive(false);
                FMODUnity.RuntimeManager.PlayOneShot("event:/player/close");
            }

            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/player/open");
                parapluieFerme.SetActive(false);
                parapluieOuvert.SetActive(true);
            }
        }
        if (ActiveTimer) timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            parapluieFerme.SetActive(false);
            parapluieOuvert.SetActive(true);
            timer = timerReset;
            ActiveTimer = false;
        }
        NombreFlapText.text = FlapingNumber.ToString();

        if (Input.GetButtonDown("Fire3"))
        {

            FlapingNumber += 1f;
        }
    }

    void FixedUpdate()
    {
        orientationModif = OrientationVent + ((cameraTransform.right * Input.GetAxis("Horizontal") + cameraTransform.forward * Input.GetAxis("Vertical")) * ImpulseOrientationPlayer);
        orientationAnim = OrientationVent + cameraTransform.right * Input.GetAxis("Horizontal") + cameraTransform.forward * Input.GetAxis("Vertical");


        if (fermer)
        {
            rb.drag = dragFermer;
        }
        else
        {
            rb.drag = drag;
            //rb.AddForce(orientationModif, ForceMode.Impulse);
        }
        if (Collision)
        {
            rb.freezeRotation = false;
            //FlapingNumber = NombreFlap;
        }
        else
        {
            rb.freezeRotation = true;
            if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f || Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
            {
                if (Input.GetAxisRaw("Horizontal") > 0f)
                {
                    rb.AddTorque(-cameraTransform.forward * forceOrientationAnimation, ForceMode.Acceleration);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0f)
                {
                    rb.AddTorque(cameraTransform.forward * forceOrientationAnimation, ForceMode.Acceleration);
                }
                if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    rb.AddTorque(cameraTransform.right * forceOrientationAnimation, ForceMode.Acceleration);
                }
                else if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    rb.AddTorque(-cameraTransform.right * forceOrientationAnimation, ForceMode.Acceleration);
                }
            }
            else
            {
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Collision = true;
    }
    private void OnCollisionStay(Collision other)
    {

    }

    private void OnCollisionExit(Collision other)
    {
        Collision = false;
        //transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
    }
}
