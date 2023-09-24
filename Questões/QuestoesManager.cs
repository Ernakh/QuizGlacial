using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestoesManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gravar(string enunciado, string altA, string altB, string altC, string altD, string altE, char certa)
    {
        Criptografia crip = new Criptografia();

        Questao quest = new Questao();
        quest.numero = 1;//dinâmico, alterado logo mais
        quest.enunciado = enunciado;
        quest.alternativaA = altA;
        quest.alternativaB = altB;
        quest.alternativaC = altC;
        quest.alternativaD = altD;
        quest.alternativaE = altE;
        quest.alternativaCorreta = certa;

        string json = "";

        string filePath = Path.Combine(Application.streamingAssetsPath, "questoes.json");

        print(filePath);

        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        QuestoesList listaAtual = new QuestoesList();

        if (File.Exists(Application.streamingAssetsPath + "/questoes.json"))
        {
            string jsonAtual = crip.DecryptData(File.ReadAllText(Application.streamingAssetsPath + "/questoes.json"), "DOC2021FABRICIO");

            listaAtual = JsonUtility.FromJson<QuestoesList>(jsonAtual);

        }

        if (listaAtual.lista != null)
        {
            quest.numero = (listaAtual.lista.Count + 1);
        }
        else
        {
            listaAtual.lista = new List<Questao>();
        }

        listaAtual.lista.Add(quest);

        json = JsonUtility.ToJson(listaAtual);

        using (StreamWriter file = new StreamWriter(filePath))
        {
            file.WriteLine(crip.EncryptData(json, "DOC2021FABRICIO"));
        }

    }

    public void ler()
    {
        try
        {
            Criptografia crip = new Criptografia();

            string json = crip.DecryptData(File.ReadAllText(Application.streamingAssetsPath + "/questoes.json"), "DOC2021FABRICIO");

            QuestoesList lista = JsonUtility.FromJson<QuestoesList>(json);

            print(lista.lista.Count);
        }
        catch (Exception ex)
        {
            print("Não encontrado");
        }
    }

    public void lerDiretorio()
    {
        DirectoryInfo di = new DirectoryInfo(Application.streamingAssetsPath);

        foreach (FileInfo file in di.GetFiles())
        {
            print(file.Name);
        }
    }

    public void apagar()
    {
        File.Delete(Application.streamingAssetsPath + "/questoes.json");
    }
}
