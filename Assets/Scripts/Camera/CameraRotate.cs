using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraRotate : MonoBehaviour
{
    [Header("souris")]
    [SerializeField] private float mouseSFDP = 50f;
    [SerializeField] private float minC = -70f, maxC = 80f;

    [Header("Components")]
    public Transform ParapluieTransform;
    public GameObject cameraFDP;
    public GameObject JumpOrientation;

    private CharacterController charController;
    private float xRotation = 0f;
    private Vector3 playerVelo;

    [Header("Réglages camera")]
    public float CameraRotationAutoHaut = 0.5f;
    public float CameraRotationAutobas = 1f;

    public GameObject rotationReference;

    public float upRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 Direction = JumpOrientation.transform.position - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward,Direction);
        if (Input.GetKeyDown(KeyCode.F5)) SceneManager.LoadScene("test");

        //fait bouger le cam automatiquement en fonction du parapluie
        if (JumpOrientation.transform.position.y - gameObject.transform.position.y >= 2 && (transform.rotation.eulerAngles.x <= 280f || transform.rotation.eulerAngles.x >= 330f))
        {
            xRotation -= (JumpOrientation.transform.position.y - gameObject.transform.position.y) * CameraRotationAutoHaut / 10;
        }
        if (JumpOrientation.transform.position.y - gameObject.transform.position.y <= -0.1f && (transform.rotation.eulerAngles.x <= 10f || transform.rotation.eulerAngles.x >= 80f))
        {
            xRotation += (JumpOrientation.transform.position.y - gameObject.transform.position.y) * -CameraRotationAutobas / 10;
        }
        else if (JumpOrientation.transform.position.y - gameObject.transform.position.y <= -1f)
        {
            xRotation += (JumpOrientation.transform.position.y - gameObject.transform.position.y) * -CameraRotationAutobas / 10;
        }
        Debug.Log(transform.rotation.eulerAngles.x);
        if (Input.GetAxisRaw("Mouse X")> 0 || Input.GetAxisRaw("Mouse X")< 0)
        {
            if (Input.GetAxisRaw("Mouse Y")> 0 || Input.GetAxisRaw("Mouse Y")< 0)
            {
                if ((JumpOrientation.transform.position.z - gameObject.transform.position.z >= 0.1 || JumpOrientation.transform.position.x - gameObject.transform.position.x >= 0.1 || JumpOrientation.transform.position.z - gameObject.transform.position.z <= -0.1 || JumpOrientation.transform.position.x - gameObject.transform.position.x <= -0.1))
                {
                    //transform.LookAt(JumpOrientation.transform, Vector3.up);
                }
            }
        }





        //fait bouger le cam avec input
        float mouseX = Input.GetAxis("Mouse X") * mouseSFDP * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSFDP * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minC, maxC);

        transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y, 0);
        transform.Rotate(Vector3.up * mouseX);
        transform.DOMove(ParapluieTransform.position, 01f);
    }
}
