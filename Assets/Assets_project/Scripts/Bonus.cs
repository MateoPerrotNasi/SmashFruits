using UnityEngine;

public class Bonus : MonoBehaviour
{
    //Lorsque le bonus rentre en collision avec un objet tagué comme "Joueur", on appelle la fonction pour soigner le joueur, puis le bonus est détruit
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<LifeCounter>().IncreaseLife();
            Destroy(gameObject);
        }
    }
}
