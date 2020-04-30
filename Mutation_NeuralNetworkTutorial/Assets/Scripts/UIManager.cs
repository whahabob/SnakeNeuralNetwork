using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text textGeneration;
    public Text textFitness;
    Manager manager = null;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        textFitness.text = AverageFitness(manager.nets).ToString();
        textGeneration.text = manager.generationNumber.ToString();
    }

    private float AverageFitness(List<NeuralNetwork> neuralNetworks)
    {
        float avgFitness = 0f;
        if (neuralNetworks != null)
        {
            for (int i = 0; i < neuralNetworks.Count; i++)
            {
                avgFitness += neuralNetworks[i].GetFitness();
            }
            avgFitness /= neuralNetworks.Count;
        }
       
        

        return avgFitness;
    }
}
