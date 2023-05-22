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

    //On r�cup�re la zone de spawn � l'activation du script
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

    //Coroutine de spawn des fruits
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        while (enabled)
        {
            GameObject fruitPrefab = fruitsList[Random.Range(0, fruitsList.Length)];

            //On d�termine al�atoirement si le fruit sera une bombe ou non
            if (Random.value < bombProbability)
            {
                fruitPrefab = bomb;
            }

            //On cr�er un vecteur al�atoire qui d�finira la position de spawn du fruit
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            //On cr�e une valeur de rotation al�atoire pour le fruit
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            //On instancie le fruit et on le d�truit lorsque sa dur�e de vie atteint sa limite
            GameObject fruit = Instantiate(fruitPrefab, position, rotation);
            Destroy(fruit, maxLifeTime);

            //On applique une force al�atoire au fruit
            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
