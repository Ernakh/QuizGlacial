using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGravarQuestao : MonoBehaviour
{
    public TMP_InputField enunciado;
    public TMP_InputField altA;
    public TMP_InputField altB;
    public TMP_InputField altC;
    public TMP_InputField altD;
    public TMP_InputField altE;
    public GameObject correta;

    private char certa;

    public void Start()
    {
        var dropDown = correta.GetComponent<TMP_Dropdown>();

        DropdownItemSelected(dropDown);

        dropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropDown); });
    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int i = dropdown.value;
        certa = dropdown.options[i].text[0];
    }
    public void GravarQuestao()
    {

        QuestoesManager questoes = new QuestoesManager();
        questoes.gravar(enunciado.text, altA.text, altB.text, altC.text, altD.text, altE.text, certa);

        SceneManager.LoadScene("Questões");
    }

    public void Cancelar()
    {
        SceneManager.LoadScene("Questões");
    }
}
