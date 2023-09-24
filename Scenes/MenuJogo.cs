using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJogo : MonoBehaviour
{
    public GameObject menu;
    public GameObject instrucoes;
    public GameObject creditos;
    public AudioSource audio;
    public void IniciaJogo()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void CarregaTelaQuestões()
    {
        SceneManager.LoadScene("Questões");
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Creditos()
    {
        creditos.SetActive(true);
        instrucoes.SetActive(false);
        menu.SetActive(false);
    }

    public void Voltar()
    {
        audio.volume = 1;
        menu.SetActive(true);
        instrucoes.SetActive(false);
        creditos.SetActive(false);
    }

    public void Instrucoes()
    {
        audio.volume = 0;
        instrucoes.SetActive(true);
        menu.SetActive(false);
        creditos.SetActive(false);
    }
}
