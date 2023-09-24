using Dypsloom.DypThePenguin.Scripts.Interactions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour
{
    public Canvas canvas;

    private float tempo = 0.5f;
    private float tempoQuestao = 0.1f;

    private Questao questao;
    public string questaoNumero;//apenas para debug
    public bool respondida = false;//apenas para debug

    public GameObject box1;
    public GameObject box2;
    public GameObject particulas;

    public Interactable interactable;

    public TextMeshProUGUI enunciado;
    public TextMeshProUGUI AltA;
    public TextMeshProUGUI AltB;
    public TextMeshProUGUI AltC;
    public TextMeshProUGUI AltD;
    public TextMeshProUGUI AltE;

    public AudioClip audioAbrir;
    public AudioClip audioAcertar;
    public AudioClip audioErrar;

    void Start()
    {
        canvas.enabled = false;
        interactable = this.GetComponent<Interactable>();

        StartCoroutine("contagemQuestao");
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    print("colidiu");
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.enabled = false;
        }
    }

    public void teveInteracao()
    {
        StartCoroutine("contagemRegressiva");
    }

    IEnumerator contagemRegressiva()
    {
        yield return new WaitForSeconds(tempo);
        canvas.enabled = true;
        AudioSource audio = this.GetComponent<AudioSource>();
        audio.PlayOneShot(audioAbrir);
    }

    IEnumerator contagemQuestao()
    {
        yield return new WaitForSeconds(tempo);
        GameQuestoes gq = new GameQuestoes();
        questao = gq.retornaQuestaoAleatoria();

        enunciado.text = questao.enunciado;
        AltA.text = "a) " + questao.alternativaA;
        AltB.text = "b) " + questao.alternativaB;
        AltC.text = "c) " + questao.alternativaC;
        AltD.text = "d) " + questao.alternativaD;
        AltE.text = "e) " + questao.alternativaE;

        questaoNumero = questao.numero.ToString();//apenas para debug

        tempoQuestao += 0.1f;
    }

    private void checkResposta(char escolha)
    {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject())
        {
            GameScore gscore = GameObject.Find("Managers").GetComponent<GameScore>();

            if (questao.alternativaCorreta == escolha)
            {
                gscore.addAcertos();
                AudioSource audio = this.GetComponent<AudioSource>();
                audio.PlayOneShot(audioAcertar);
            }
            else
            {
                gscore.addErros();
                AudioSource audio = this.GetComponent<AudioSource>();
                audio.PlayOneShot(audioErrar);
            }

            respondida = true; //apenas para debug
            canvas.enabled = false;
            box2.SetActive(true);
            box1.SetActive(false);
            particulas.SetActive(false);
            interactable.enabled = false;
            interactable.SetIsInteractable(false);

            Destroy(this);
        }
    }

    public void altAClick()
    {
        checkResposta('A');
    }

    public void altBClick()
    {
        checkResposta('B');
    }

    public void altCClick()
    {
        checkResposta('C');
    }

    public void altDClick()
    {
        checkResposta('D');
    }

    public void altEClick()
    {
        checkResposta('E');
    }
}
