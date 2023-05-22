using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;
    public GameObject[] fruitsList;
    public GameObject bomb;
    [Range(0f, 0.9f)]
    public float bombProbability;
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1.5f;
    public float minAngle = -15f;
    public float maxAngle = 15f;
    public float minForce = 18f;
    public float maxForce = 22f;
    public float maxLifeTime = 5f;

    //On récupère la zone de spawn à l'activation du script
    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    //On lance la coroutine de spawn lorsque l'objet est activé
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    //On stop la coroutine de spawn lorsque l'objet est désactivé
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    //Coroutine de spawn des fruits
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        while (enabled)
        {
            GameObject fruitPrefab = fruitsList[Random.Range(0, fruitsList.Length)];

            //On détermine aléatoirement si le fruit sera une bombe ou non
            if (Random.value < bombProbability)
            {
                fruitPrefab = bomb;
            }

            //On créer un vecteur aléatoire qui définira la position de spawn du fruit
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            //On crée une valeur de rotation aléatoire pour le fruit
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            //On instancie le fruit et on le détruit lorsque sa durée de vie atteint sa limite
            GameObject fruit = Instantiate(fruitPrefab, position, rotation);
            Destroy(fruit, maxLifeTime);

            //On applique une force aléatoire au fruit
            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
