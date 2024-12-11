using UnityEngine;

public class heroiata : MonoBehaviour
{
    [Header("Configurações de Dano")]
    public int dano = 2; // Dano causado ao inimigo

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto com o qual colidimos tem a tag "Inimigo"
        if (other.CompareTag("inimigo"))
        {
            // Tenta acessar o script de vida do inimigo
            Inimigo inimigo = other.GetComponent<Inimigo>();
            if (inimigo != null)
            {
                inimigo.ReceberDano(dano); // Aplica dano ao inimigo
            }
        }
    }
}

