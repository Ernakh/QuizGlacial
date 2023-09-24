using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject manager;

    public TMP_Text acertos;
    public TMP_Text erros;
    public TMP_Text tempo;

    void Start()
    {
        acertos.text = "Acertos: " + manager.GetComponent<GameScore>().getAcertos();
        erros.text = "Erros: " + manager.GetComponent<GameScore>().getErros();
        tempo.text = "Tempo: " + manager.GetComponent<GameScore>().time.ToString().Substring(0, 6) + " segundos";

        if (!Directory.Exists(Application.streamingAssetsPath + "//scores"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "//scores");
        }

        Criptografia crip = new Criptografia();

        using (StreamWriter file = new StreamWriter(Application.streamingAssetsPath + "//scores//" + DateTime.Now.ToString().Replace(':','-').Replace('/', '-').Replace('.', '-') + ".txt"))
        {
            file.WriteLine(crip.EncryptData("Finalizado em: " + DateTime.Now, "DOC2021FABRICIO"));
            file.WriteLine(crip.EncryptData(acertos.text, "DOC2021FABRICIO"));
            file.WriteLine(crip.EncryptData(erros.text, "DOC2021FABRICIO"));
            file.WriteLine(crip.EncryptData(tempo.text, "DOC2021FABRICIO"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
