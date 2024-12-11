using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameOverController GameOverController;
    private Animator animator;
    private Rigidbody2D rb;
    public float jumpForce = 5f;
    public float moveSpeed = 3f; 
    private float horizontalInput;
    public Transform heroiT;
    private int coinCount;
    public TMP_Text textCoins;
    [Header("Configurações de Vida")]
    public int vidaMaxima = 3; // Vida máxima do inimigo
    private int vidaAtual;
    public Image[] imagensVida;

    // Novas variáveis para o controle de pulo
    private int jumpCount = 0; // Conta o número de pulos
    public int maxJumpCount = 2; // Número máximo de pulos permitidos

    // Raycast para verificar se o herói está tocando o chão
    public float groundRayLength = 0.1f; // Distância do Raycast
    public LayerMask groundLayer; // Camada que representa o chão

    void Start()
    {
        vidaAtual = vidaMaxima;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Verificar se o herói está no chão
        bool isGrounded = IsGrounded();

        // Se o herói estiver no chão, reseta o contador de pulos
        if (isGrounded)
        {
            jumpCount = 0; // Pode pular novamente
        }

        // Movimento horizontal
        horizontalInput = Input.GetAxis("Horizontal"); // -1 para esquerda, 1 para direita
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("pular"); // Ativa a animação de pulo
            jumpCount++; // Incrementa o contador de pulos
        }

        // Atualiza a animação de movimento
        if (Mathf.Abs(horizontalInput) > 0)
        {
            animator.SetInteger("estado", 1); // Estado movendo
        }
        else
        {
            animator.SetInteger("estado", 0); // Estado parado
        }

        // Inverte o personagem dependendo da direção
        if (horizontalInput > 0 && heroiT.localScale.x < 0 || horizontalInput < 0 && heroiT.localScale.x > 0)
        {
            Flip();
        }

        // Ataque
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("ataque1"); // Ativa a animação de ataque
        }
    }
    public void ReceberDano(int dano)
    {
        vidaAtual -= dano; // Reduz a vida pelo dano recebido
        Debug.Log("Heroi recebeu dano: " + dano + ". Vida restante: " + vidaAtual);
        AtualizarImagensVida();
        if (vidaAtual <= 0)
        {
            Morrer(); // Chama o método de morte se a vida for menor ou igual a 0
        }
    }
    private void AtualizarImagensVida()
    {
        // Atualiza as imagens de vida com base no valor de vidaAtual
        for (int i = 0; i < imagensVida.Length; i++)
        {
            if (i < vidaAtual)
            {
                imagensVida[i].enabled = true; // Mostra a imagem de vida se o índice for menor que a vida atual
            }
            else
            {
                imagensVida[i].enabled = false; // Esconde a imagem de vida se o índice for maior ou igual à vida atual
            }
        }
    }

    private void Morrer()
    {
        // Verifique se gameOverController não está nulo
        if (GameOverController != null)
        {
            GameOverController.GameOver();  // Ativa o painel de GameOver
        }
        else
        {
            Debug.LogError("gameOverController é nulo!");
        }
        
        // Outras ações quando o personagem morre, como destruir o GameObject
        Destroy(gameObject);  // Destroi o GameObject do herói (caso queira removê-lo)
    }

    void Flip()
    {
        Vector3 scala = heroiT.localScale;
        scala.x *= -1;
        heroiT.localScale = scala;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        // Se o objeto colidido for a moeda
        if (col.CompareTag("moeda"))
        {
            // Aumenta o contador de moedas
            coinCount++;
            textCoins.text = "Moedas coletadas: " + coinCount;
            Debug.Log("Moedas coletadas: " + coinCount);

            // Destroi a moeda
            Destroy(col.gameObject);
        }
        
    }
    // Função para verificar se o personagem está tocando o chão usando Raycast
    bool IsGrounded()
    {
        // Raycast para verificar se o personagem está tocando o chão
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRayLength, groundLayer);

        // Se o Raycast atingir um objeto que esteja na camada de chão, o herói está no chão
        return hit.collider != null;
    }
}
