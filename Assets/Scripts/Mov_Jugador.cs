using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Jugador : MonoBehaviour
{
    [SerializeField]
    private float velocidad;

    [SerializeField]
    private float fuerzaSalto;

    [SerializeField]
    private int limiteSaltos = 2;  // Límite de saltos (2 para doble salto)

    private int saltosRestantes;  // Contador de saltos disponibles
    private Rigidbody2D rigidbody;
    private Animator animador;
    private SpriteRenderer RenderSprite;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        RenderSprite = GetComponent<SpriteRenderer>();
        saltosRestantes = limiteSaltos;  // Inicialmente puede saltar hasta el límite
    }

    // Update is called once per frame
    void Update()
    {
        // Captura controles
        var movimientoX = Input.GetAxis("Horizontal");
        var movimiento = new Vector2(movimientoX, 0);
        transform.Translate(movimiento * Time.deltaTime * velocidad);

        // Verifica si el jugador puede saltar (si tiene saltos restantes)
        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            var impulso = new Vector2(0, fuerzaSalto);
            rigidbody.AddForce(impulso, ForceMode2D.Impulse);
            saltosRestantes--;  // Reduce el número de saltos disponibles
        }

        // Gestiona animaciones
        if (movimientoX > 0)
        {
            animador.SetBool("Direccion", false);
            animador.SetBool("Caminando", true);
        }
        else if (movimientoX < 0)
        {
            animador.SetBool("Direccion", true);
            animador.SetBool("Caminando", true);
        }
        else if (movimientoX == 0)
        {
            animador.SetBool("Caminando", false);
        }

        // Verifica el estado de salto y caída
        if (rigidbody.velocity.y > 0.0f)
        {
            animador.SetBool("Saltando", true);
        }
        else if (rigidbody.velocity.y < 0.0f)
        {
            animador.SetBool("Cayendo", true);
            animador.SetBool("Saltando", false);
        }
        else if (rigidbody.velocity.y == 0.0f)
        {
            animador.SetBool("Saltando", false);
            animador.SetBool("Cayendo", false);
            saltosRestantes = limiteSaltos;  // Restablece los saltos al tocar el suelo
        }
    }
}
