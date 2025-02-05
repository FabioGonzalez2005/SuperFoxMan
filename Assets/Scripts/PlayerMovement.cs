using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float jumpForce = 7f; // Fuerza del salto
    private bool facingRight = true; // Controla la dirección del personaje
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    private Rigidbody rb; // Referencia al Rigidbody para aplicar fuerzas

    private float minX = -5.6f; // Límite izquierdo
    private float maxX = 6.57f; // Límite derecho

    void Start()
    {
        // Obtener el SpriteRenderer y Rigidbody del objeto
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float move = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            move = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = 1f;
        }

        // Mueve al personaje
        transform.Translate(Vector3.right * move * speed * Time.deltaTime);

        // Aplicar los límites en X
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, -3.14f);

        // Asegurar que la rotación sea 0 en todos los ejes
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // Voltear el sprite si cambia de dirección
        if ((move > 0 && !facingRight) || (move < 0 && facingRight))
        {
            Flip();
        }

        // Comprobar si la tecla W está presionada y el personaje está en el suelo
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Aplicar una fuerza hacia arriba al Rigidbody para hacer el salto
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Flip()
    {
        facingRight = !facingRight;

        // Volteamos el sprite cambiando el valor de la escala en el eje X
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
