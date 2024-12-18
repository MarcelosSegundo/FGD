using UnityEngine;

public class VictoryController : MonoBehaviour
{
    public GameObject VictoryScreen; // Painel de Vitória (UI)



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (collision.CompareTag("Player"))
        {
            ShowVictoryScreen();
        }
    }

    public void ShowVictoryScreen()
    {
        // Ativa a tela de vitória
        VictoryScreen.SetActive(true);

        // Pausa o jogo
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Reinicia o jogo
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Sai do jogo
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
