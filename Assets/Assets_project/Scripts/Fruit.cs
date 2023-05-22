using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;
    public AudioSource sliceSound;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticle;

    //Lorsque le script est lancé, on récupère le corps du fruit, son détecteur de collision et ses particules
    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticle = GetComponentInChildren<ParticleSystem>();
    }

    //Lorsque le fruit est découpé, on augmente le score de 1 point, on passe le fruit en mode "tranché", on désactive son détecteur de collision et on active ses particules
    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore();

        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;
        sliceSound.Play();
        juiceParticle.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    //Lorsque le fruit rentre en collision avec le joueur, on appelle la fonction Slice
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
