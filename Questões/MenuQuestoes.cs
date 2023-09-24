using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuQuestoes : MonoBehaviour
{
    public GameObject content;
    public GameObject parent;
    public GameObject desempenho;
    public void IncluirQuestao()
    {
        if (!File.Exists(Application.streamingAssetsPath + "/lock.data"))
        {
            SceneManager.LoadScene("GravarQuestão");
        }
    }

    public void ConsultarQuestoes()
    {
        if (!File.Exists(Application.streamingAssetsPath + "/lock.data"))
        {
            Criptografia crip = new Criptografia();

            string jsonAtual = crip.DecryptData(File.ReadAllText(Application.streamingAssetsPath + "/questoes.json"), "DOC2021FABRICIO");

            QuestoesList listaAtual = JsonUtility.FromJson<QuestoesList>(jsonAtual);

            //limpa a lista em tela
            Transform[] childs = new Transform[parent.transform.childCount];
            for (int c = 0; c < parent.transform.childCount; c++)
            {
                childs[c] = parent.transform.GetChild(c);
            }

            for (int c = 0; c < childs.Length; c++)
            {
                childs[c].transform.parent = null;
                GameObject.Destroy(childs[c]);
            }
            //fim da "limpa a lista em tela"

            int i = 0;

            foreach (Questao item in listaAtual.lista)
            {
                GameObject go = Instantiate(content);
                go.name = "btn" + item.numero;
                go.transform.SetParent(parent.transform);
                go.transform.position = new Vector3(parent.transform.position.x + 240, parent.transform.position.y - 160 - (17 * i), 0);

                ((Button)(go.transform.Find("Button").GetComponent<Button>())).onClick.AddListener(delegate { Remover(item.numero); });
                ((TextMeshProUGUI)(go.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>())).text = item.enunciado;

                i++;
            }
        }
    }

    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Remover(int numero)
    {
        if (!File.Exists(Application.streamingAssetsPath + "/lock.data"))
        {

            Criptografia crip = new Criptografia();

            string jsonAtual = crip.DecryptData(File.ReadAllText(Application.streamingAssetsPath + "/questoes.json"), "DOC2021FABRICIO");

            QuestoesList listaAtual = JsonUtility.FromJson<QuestoesList>(jsonAtual);

            foreach (Questao item in listaAtual.lista)
            {
                if (item.numero == numero)
                {
                    listaAtual.lista.Remove(item);
                    break;
                }
            }

            int i = 1;

            foreach (Questao item in listaAtual.lista)
            {
                item.numero = i;
                i++;
            }

            File.Delete(Application.streamingAssetsPath + "/questoes.json");

            string json = JsonUtility.ToJson(listaAtual);
            using (StreamWriter file = new StreamWriter(Path.Combine(Application.streamingAssetsPath, "questoes.json")))
            {
                file.WriteLine(crip.EncryptData(json, "DOC2021FABRICIO"));
            }

            ConsultarQuestoes();
        }
    }

    public void Exportar()
    {
        Application.OpenURL(Path.Combine(Application.streamingAssetsPath, "questoes.json"));
    }

    public void Importar()
    {
        Criptografia crip = new Criptografia();
        string jsonAtual = crip.DecryptData(File.ReadAllText(Application.streamingAssetsPath + "/questoes.json"), "DOC2021FABRICIO");

        using (StreamWriter file = new StreamWriter(Path.Combine(Application.streamingAssetsPath, "questoes_bkp.json")))
        {
            file.WriteLine(crip.EncryptData(jsonAtual, "DOC2021FABRICIO"));
        }

        File.Delete(Application.streamingAssetsPath + "/questoes.json");

        using (StreamWriter file = new StreamWriter(Path.Combine(Application.streamingAssetsPath, "questoes.json")))
        {
            file.WriteLine("");
        }

        Application.OpenURL(Path.Combine(Application.streamingAssetsPath, "questoes.json"));
    }

    public void Finalizar()
    {
        using (StreamWriter file = new StreamWriter(Path.Combine(Application.streamingAssetsPath, "lock.data")))
        {
            file.WriteLine(DateTime.Now);
        }

        SceneManager.LoadScene("Menu");
    }

    public void Reset()
    {
        File.Delete(Application.streamingAssetsPath + "/questoes.json");
        File.Delete(Application.streamingAssetsPath + "/lock.data");
        Directory.Delete(Application.streamingAssetsPath + "/scores", true);
        SceneManager.LoadScene("Menu");
    }

    public void Desempenhos()
    {
        //EditorUtility.RevealInFinder(Application.streamingAssetsPath + "/scores");
        //limpa a lista em tela
        Transform[] childs = new Transform[parent.transform.childCount];
        for (int c = 0; c < parent.transform.childCount; c++)
        {
            childs[c] = parent.transform.GetChild(c);
        }

        for (int c = 0; c < childs.Length; c++)
        {
            childs[c].transform.parent = null;
            GameObject.Destroy(childs[c]);
        }
        //fim da "limpa a lista em tela"

        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath + "/scores");
        FileInfo[] files = directoryInfo.GetFiles();

        Criptografia crip = new Criptografia();

        int count = 1;
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Extension == ".txt")
            {
                string[] linhas;
                string txt = File.ReadAllText(Application.streamingAssetsPath + "/scores/" + files[i].Name);
                linhas = txt.Split(
                    new string[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                );

                GameObject go = Instantiate(desempenho);
                go.name = "desempenho" + count;
                go.transform.SetParent(parent.transform);
                go.transform.position = new Vector3(parent.transform.position.x + 240, parent.transform.position.y - 100 - (65 * count), 0);

                ((TextMeshProUGUI)(go.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>())).text = 
                   crip.DecryptData(linhas[0], "DOC2021FABRICIO") + Environment.NewLine + 
                   crip.DecryptData(linhas[1], "DOC2021FABRICIO") + Environment.NewLine +
                   crip.DecryptData(linhas[2], "DOC2021FABRICIO") + Environment.NewLine +
                   crip.DecryptData(linhas[3], "DOC2021FABRICIO");

                count++;
            }
        }

    }
}
