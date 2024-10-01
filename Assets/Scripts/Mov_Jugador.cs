using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Jugador : MonoBehaviour
{
    [SerializeField]
    private float velocidad;

    [SerializeField]
    private float fuerzaSalto;

    private Rigidbody2D rigidbody;
    private Animator animador;
    private SpriteRenderer RenderSprite;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        RenderSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Captura controles
        var movimientoX = Input.GetAxis("Horizontal");
        var movimiento = new Vector2(movimientoX,0);
        transform.Translate(movimiento * Time.deltaTime * velocidad);
        if(Input.GetKeyDown(KeyCode.Space)){
            var impulso = new Vector2(0,fuerzaSalto);
            rigidbody.AddForce(impulso,ForceMode2D.Impulse);
        }

        // Gestiona animaciones
        if(movimientoX > 0){
            animador.SetBool("Direccion",false);
            animador.SetBool("Caminando",true);
        }else if(movimientoX < 0){
            animador.SetBool("Direccion",true);
            animador.SetBool("Caminando",true);
        }else if(movimientoX == 0){
            animador.SetBool("Caminando",false);
        }
        
        if(rigidbody.velocity.y > 0.0){
            animador.SetBool("Saltando",true);
        }else if(rigidbody.velocity.y < 0.0){
            animador.SetBool("Cayendo",true);
            animador.SetBool("Saltando",false);
        }else if(rigidbody.velocity.y == 0.0){
            animador.SetBool("Saltando",false);
            animador.SetBool("Cayendo",false);
        }
    }
}