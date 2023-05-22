using UnityEngine;

public class Malus : MonoBehaviour
{
    
    //Lorsque le malus rentre en collision avec un objet tagu� comme "Joueur", la probabilit� de cr�� une bombe augmente de 10% puis le malus est d�truit
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().Malus();
            Destroy(gameObject);
        }
    }
}
