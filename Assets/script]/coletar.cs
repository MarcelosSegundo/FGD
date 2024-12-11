using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletar : MonoBehaviour
{
    public int vida = 10;  // Inicializa vida com valor 10
    public int dano = 10;  // Inicializa dano com valor 10
    
    private int coinCount = 0;  // Contador de moedas coletadas

    // Método que é chamado quando o personagem entra em um Trigger (moeda)
    void OnTriggerEnter2D(Collider2D col)
    {
        // Se o objeto colidido for a moeda
        if (col.CompareTag("Coin"))
        {
            // Aumenta o contador de moedas
            coinCount++;
            Debug.Log("Moedas coletadas: " + coinCount);

            // Destroi a moeda
            Destroy(col.gameObject);
        }
        
        // Se o objeto colidido for o Player (como um inimigo ou objeto de dano)
        if (col.CompareTag("Player"))
        {
            // Chama a função tomarDano com valor de 10
            tomarDano(10);
        }
    }

    // Função para diminuir vida
    void tomarDano(int dano)
    {
        if (dano >= vida)
        {
            // Aqui você pode colocar algum comportamento quando o dano for maior ou igual à vida, como "Game Over"
            vida = 0;
            Debug.Log("Você morreu!");
        }
        else
        {
            vida -= dano;  // Subtrai o dano da vida
            Debug.Log("Vida atual: " + vida);
        }
    }
}
