using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private CanvasGroup endGameMenu;
    [SerializeField] private TextMeshProUGUI endGameText;

    public static Player Player { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static void EndGame(bool won) => instance.IEndGame(won);
    private void IEndGame(bool won)
    {
        endGameMenu.alpha = 1;
        endGameMenu.interactable = true;
        endGameMenu.blocksRaycasts = true;
        endGameText.text = won ? "You Won!" : "Game Over";
    }
}
