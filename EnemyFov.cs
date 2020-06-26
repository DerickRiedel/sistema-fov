using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFov : MonoBehaviour
{
    Vector3 anguloX,anguloY;

    bool playerVisto = false;

    [Range(10, 90)]
    public int campoDeVisao, anguloVertical;

    [Range(3, 15)]
    public int espaçoEntreLinhas, camadas;

    [Range(3,20)]
    public float distancia;

    float distanciaDebug;

    [Range(0.01f, 1f)]
    public float velocidadeDeChecagem = 0.3f;

    float tempoDeChecagem = 0;

    public GameObject zombie;

    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempoDeChecagem += Time.deltaTime;
        //SE CASO O PLAYER FOR VISTO DESABILITAR A FUNÇÃO
        if(playerVisto == false && tempoDeChecagem > velocidadeDeChecagem)
        {
            CampoDeVisao();
            //Debug.Log("Checado");
            tempoDeChecagem = 0;   
        }
    }

    public void PlayerVisto()
    {
        playerVisto = true;
    }

    //MÉTODO PARA MONTAR AS LINHAS
    public void CampoDeVisao()
    {
        //LAÇO PARA PERCORRER TODO O LEQUE
        for (int x = -campoDeVisao; x < campoDeVisao; x += espaçoEntreLinhas)
        {
            //CADA ANGULO VERTICAL
            //for (int y = -anguloVertical; y < anguloVertical; y += camadas)
            //{
            //    anguloY = Quaternion.AngleAxis(y, transform.right) * transform.forward;

            //    Physics.Raycast(transform.position, anguloY, out hit, 1000);

            //    if (distanciaDebug > distancia)
            //    {
            //        distanciaDebug = distancia;
            //    }
            //    //VERIFICAR SE O PLAYER ESTÁ DENTRO DO LEQUE E SE ESTÁ DENTRO DA DISTANCIA PERMITIDA  ->    vvvvvvvvv
            //    if (hit.transform.CompareTag("Player") && Vector3.Distance(transform.position, hit.point) < distancia)
            //    {
            //        zombie.SendMessage("ativo");
            //        Debug.Log("Player visto");
            //        Debug.DrawRay(transform.position, anguloY * distanciaDebug, Color.red, velocidadeDeChecagem);
            //        playerVisto = true;
            //    }
            //    else
            //    {
            //        Debug.DrawRay(transform.position, anguloY * distanciaDebug, Color.white, velocidadeDeChecagem);
            //    }
            //}
            //CADA ANGULO DENTRO DO LEQUE
            anguloX = Quaternion.AngleAxis(x, transform.up) * transform.forward;

            //LANÇAR O RAYCAST
            Physics.Raycast(transform.position, anguloX, out hit, 1000);

            //DISTANCIA DA LINHA PARA MOSTRAR NO DEBUG
            distanciaDebug = Vector3.Distance(transform.position, hit.point);
            if(distanciaDebug > distancia)
            {
                distanciaDebug = distancia;
            }
            //VERIFICAR SE O PLAYER ESTÁ DENTRO DO LEQUE E SE ESTÁ DENTRO DA DISTANCIA PERMITIDA  ->    vvvvvvvvv
            if (hit.transform.CompareTag("Player") && Vector3.Distance(transform.position, hit.point) < distancia)
            {
                zombie.SendMessage("ativo");
                Debug.Log("Player visto");
                Debug.DrawRay(transform.position, anguloX * distanciaDebug, Color.red, velocidadeDeChecagem);
                playerVisto = true;
            }
            else
            {
                Debug.DrawRay(transform.position, anguloX * distanciaDebug, Color.white, velocidadeDeChecagem);
            }
        }
    }
}
