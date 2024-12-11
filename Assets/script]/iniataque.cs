using UnityEngine;

public class iniataque : MonoBehaviour
{
    [Header("Configurações de Dano")]
    public int dano = 2; // Dano causado ao inimigo

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto com o qual colidimos tem a tag "Inimigo"
        if (other.CompareTag("Player"))
        {
            // Tenta acessar o script de vida do inimigo
            PlayerController heroi = other.GetComponent<PlayerController>();
            if (heroi != null)
            {
                heroi.ReceberDano(dano); // Aplica dano ao inimigo
            }
        }
    }
}
