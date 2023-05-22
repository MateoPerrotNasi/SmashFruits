using UnityEngine;

public class Bomb : MonoBehaviour
{
    //Lorsque la bombe rentre en collision avec un objet tagué comme "Joueur", on appelle la fonction d'explosion de bombe, puis la bombe est détruite
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().Explosion();
            Destroy(gameObject);
        }
    }
}
