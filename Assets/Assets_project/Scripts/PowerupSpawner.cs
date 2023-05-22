using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    private Collider spawnArea;
    public GameObject bonus;
    public GameObject malus;

    //On R�cup�re la zone de spawn � l'activation du script
    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    //On lance la coroutine de spawn lorsque l'objet est activ�
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    //On stop la coroutine de spawn lorsque l'objet est d�sactiv�
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    //Coroutine de spawn du bonus/malus
    private IEnumerator Spawn()
    {
        GameObject powerup;
        
        yield return new WaitForSeconds(2f);
        
        while (enabled)
        {
            //Une chance sur deux de faire spawn un bonus ou un malus
            if (Random.value < 0.5f)
            {
                powerup = bonus;
            }
            else
            {
                powerup = malus;
            }

            //On cr�e un vecteur al�atoire qui d�finiera la position de spawn du powerup
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            //On cr�e une valeur de rotation al�atoire pour le powerup
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(-30f, 30f));

            //On instancie le powerup
            GameObject fruit = Instantiate(powerup, position, rotation);
            
            //Et on le d�truit au bout de 1.5 secondes
            Destroy(fruit, 1.5f);


            yield return new WaitForSeconds(5f);
        }
    }
}
