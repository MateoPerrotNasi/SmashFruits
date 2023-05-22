using System.Runtime.CompilerServices;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private bool isSlicing;
    private TrailRenderer bladeTrail;

    public Vector3 direction { get; private set; }
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    //Lorsque le script est initialis�, on r�cup�re la cam�ra, le d�tecteur de collision et la trail du joueur
    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    //Lorsque l'objet est d�sactiv� on lance la fonction StopSlicing
    private void OnDisable()
    {
        StopSlicing();
    }

    //Lorsque le joueur appuie sur le bouton gauche de la souris, on lance la fonction StartSlicing, s'il arr�te on la stop et sinon
    private void Update()
     {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();

        }
        else if (isSlicing)
        {
            ContinueSlicing();
        }
     }

    //Lorsque le joueur appuie sur le bouton gauche de la souris, on active le d�tecteur de collision et la trail du joueur
    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        isSlicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    //On d�sactive le d�tecteur de collision et la trail du joueur
    private void StopSlicing()
    {
        isSlicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
   
    }

    //On bouge le d�tecteur de collision sur la position de la souris et on applique une force au joueur
    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }
}
