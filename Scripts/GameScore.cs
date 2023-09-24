using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [SerializeField]
    private int acertos = 0;
    [SerializeField]
    private int erros = 0;
    public float time = 0;

    public int getAcertos()
    {
        return acertos;
    }

    public int getErros()
    {
        return erros;
    }

    public void addAcertos()
    {
        acertos++;
    }

    public void addErros()
    {
        erros++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
}
