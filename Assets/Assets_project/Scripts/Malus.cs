using UnityEngine;

public class Malus : MonoBehaviour
{
    //Lorsque le malus rentre en collision avec un objet tagué comme "Joueur", la probabilité de créé une bombe augmente de 10% puis le malus est détruit
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float bombProb = FindObjectOfType<Spawner>().bombProbability;
            if (bombProb < 0.8)
            {
                FindObjectOfType<Spawner>().bombProbability += 0.1f;
            }
            Destroy(gameObject);
        }
    }
}
