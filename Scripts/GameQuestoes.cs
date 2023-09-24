using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameQuestoes : MonoBehaviour
{
    private static Questao[] lista;
    private static int[] listaEscolhidas;
    private int questoesCount;//questões contidas no json
    void Start()
    {
        Criptografia crip = new Criptografia();

        listaEscolhidas = new int[10];//sempre 10 questões no jogo
        
        for (int i = 0; i < 10; i++)
        {
            listaEscolhidas[i] = -1;
        }

        string json = crip.DecryptData(File.ReadAllText(Application.streamingAssetsPath + "/questoes.json"), "DOC2021FABRICIO");

        QuestoesList qLista = JsonUtility.FromJson<QuestoesList>(json);
        lista = qLista.lista.ToArray();
        questoesCount = lista.Length;
    }

    void Update()
    {
        
    }

    public Questao retornaQuestaoAleatoria()
    {
        questoesCount = lista.Length;

        int valor = Random.Range(0, (questoesCount));

        while (System.Array.Exists<int>(listaEscolhidas, e => e == valor))
        {
            valor = Random.Range(0, (questoesCount));
        }

        int flag = 0;
        for (int i = 0; i < 10; i++)
        {
            if (listaEscolhidas[i] == -1)
            {
                listaEscolhidas[i] = valor;
                flag = i;
                break;
            }
        }

        return lista[valor];
    }
}
