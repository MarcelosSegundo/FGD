using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar cenas

public class GameOverController : MonoBehaviour
{
    public GameObject GameOverScreen; // Painel de Game Over (UI)
    
    // Função chamada quando o herói morre
    public void GameOver()
    {
        // Ativa a tela de Game Over
        GameOverScreen.SetActive(true);

        // Pausa o jogo
        Time.timeScale = 0f;
    }

    // Função para reiniciar o jogo (carregar a cena atual novamente)
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reseta a escala do tempo para continuar o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Carrega a cena atual novamente
    }

    // Função para sair do jogo (se estiver rodando no PC)
    public void QuitGame()
    {
        Debug.Log("Saindo do Jogo...");
        Application.Quit(); // Finaliza o aplicativo
    }
}
