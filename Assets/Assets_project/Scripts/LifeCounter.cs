using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public GameObject[] life;
    private int lifeCount = 3;

    //Retire un point de vie au joueur, s'il n'en a plus le jeu s'arrête
    public void DecreaseLife()
    {
        lifeCount--;
        life[lifeCount].SetActive(false);
        
        if (lifeCount <= 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
    
    //Donne au joueur ses 3 points de vies
    public void SetLife()
    {
        lifeCount = 3;
        foreach (GameObject life in life)
        {
            life.SetActive(true);
        }
    }

    //Ajoute un point de vie au joueur, s'il en a déjà 3 le point de vie n'est pas ajouté
    public void IncreaseLife()
    {
        if (lifeCount < 3)
        {
            life[lifeCount].SetActive(true);
            lifeCount++;
        }
    }
}
