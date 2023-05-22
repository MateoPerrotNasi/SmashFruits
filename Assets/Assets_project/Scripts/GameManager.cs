using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public Image hitImage;
    public TMP_Text restartText;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject difficultyMenu;
    public GameObject pauseButton;

    private int score;
    private Blade blade;
    private Spawner fruitSpawner;
    private PowerupSpawner powerupSpawner;
    
    //On instancie les variables du joueur, du spawner de fruit et du spawner de powerup
    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        blade.enabled = false;
        fruitSpawner = FindObjectOfType<Spawner>();
        fruitSpawner.enabled = false;
        powerupSpawner = FindObjectOfType<PowerupSpawner>();
        powerupSpawner.enabled = false;
    }

    //Au lancement du script, on lance le menu de difficulté du jeu tout en rétablissant le jeu à son état initial
    private void Start()
    {
        difficultyMenu.SetActive(true);
        Time.timeScale = 1f;
        score = 0;
        scoreText.text = score.ToString();
        ClearScene();
    }

    //A chaque nouveau tick, on vérifie si le joueur a appuyé sur la touche Echap, si c'est le cas on met le jeu en pause
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    //On supprime tous les fruits et toutes les bombes encore existantes sur la scène
    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

    //On augmente le score de 1 point et on met à jour le texte du score
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    //Fonction appellée lorsqu'une bombe explose, retire 5 point de score et appelle la fonction pour baisser les pvs du joueur
    public void Explosion()
    {
        score -= 5;
        scoreText.text = score.ToString();

        FindObjectOfType<LifeCounter>().DecreaseLife();

    }

    //Fonction appellée lorsque le joueur n'a plus aucun pv, on désactive le joueur, le spawner de fruit et le spawner de powerup puis on lance le menu de fin de jeu
    public void EndGame()
    {
        blade.enabled = false;
        fruitSpawner.enabled = false;
        powerupSpawner.enabled = false;
        gameOverMenu.SetActive(true);
    }

    //On rétablit le jeu à son état initial et on affiche un écran blanc
    public void Restart()
    {
        blade.enabled = false;
        fruitSpawner.enabled = false;
        powerupSpawner.enabled = false;
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        scoreText.enabled = false;
        float elapsed = 0f;
        float duration = 0.5f;
        restartText.enabled = true;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            hitImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;
        }

        StartCoroutine(ResetAfterDelay());
    }

    //On rétablit l'écran après avoir redonné au joueur ses pv puis on appelle la fonction de démarrage du jeu
    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f);

        FindObjectOfType<LifeCounter>().SetLife();
        Start();

        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            hitImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        restartText.enabled = false;
    }
    
    //On sort le jeu de l'état de pause
    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    //Activation du jeu en difficulté facile (peu de bombe)
    public void DifficultyEasy()
    {
        difficultyMenu.SetActive(false);
        pauseButton.SetActive(true);
        blade.enabled = true;
        fruitSpawner.enabled = true;
        fruitSpawner.bombProbability = 0.2f;
        powerupSpawner.enabled = true;
        scoreText.enabled = true;
    }
    
    //Activation du jeu en difficulté facile (nombre de bombe moyen)
    public void DifficultyNormal()
    {
        difficultyMenu.SetActive(false);
        pauseButton.SetActive(true);
        blade.enabled = true;
        fruitSpawner.enabled = true;
        fruitSpawner.bombProbability = 0.4f;
        powerupSpawner.enabled = true;
        scoreText.enabled = true;
    }

    //Activation du jeu en difficulté facile (nombre de bombe élevé)
    public void DifficultyHard()
    {
        difficultyMenu.SetActive(false);
        pauseButton.SetActive(true);
        blade.enabled = true;
        fruitSpawner.enabled = true;
        fruitSpawner.bombProbability = 0.6f;
        powerupSpawner.enabled = true;
        scoreText.enabled = true;
    }
}