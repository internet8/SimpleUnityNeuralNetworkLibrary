using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour
{
    public int population = 0;
    private int generation = 0;
    public GameObject formula;
    private float bestFitness = 0;
    private NeuralNetwork bestNetwork = null;
    private GameObject[] formulas;
    private int formulasAlive;
    public NetworkVisualizer visualizer;

    void Start()
    {
        // spawning first gen of cars
        StartGeneration(null);
    }

    void Update()
    {
        // finding the number of formulas
        formulasAlive = GameObject.FindGameObjectsWithTag("Formula").Length;
        // if no formulas were found, then we need to breed new formulas
        if (formulasAlive == 0)
        {
            Breed();
        }
    }

    private void Breed ()
    {
        List<NeuralNetwork> newGeneration = new List<NeuralNetwork>();
        List<NeuralNetwork> selectionPool = new List<NeuralNetwork>();
        foreach (GameObject formula in formulas)
        {
            Formula f = formula.GetComponent<Formula>();
            if (f.fitness > bestFitness)
            {
                bestFitness = f.fitness;
                bestNetwork = f.brain;
            }
            for (int i = 0; i < Mathf.Pow((int)f.fitness+1, 2); i++)
            {
                selectionPool.Add(f.brain);
            }
        }
        NeuralNetwork parent1;
        NeuralNetwork parent2;
        for (int i = 0; i < population; i++)
        {
            parent1 = selectionPool[Random.Range(0, selectionPool.Count)];
            parent2 = selectionPool[Random.Range(0, selectionPool.Count)];
            newGeneration.Add(NeuralNetwork.Crossover(parent1, parent2, 5));
        }
        DestroyOldFormulas();
        StartGeneration(newGeneration);
    }

    private void StartGeneration (List<NeuralNetwork> newGeneration)
    {
        generation++;
        formulas = new GameObject[population];
        for (int i = 0; i < population; i++)
        {
            GameObject newFormula = Instantiate(formula, transform.position, Quaternion.identity);
            if (newGeneration != null)
            {
                Formula f = newFormula.GetComponent<Formula>();
                if (i < population / 10)
                {
                    f.brain = bestNetwork;
                    
                } else
                {
                    f.brain = newGeneration[i];
                }
            }
            formulas[i] = newFormula;
        }
        if (newGeneration != null)
        {
            // visualizing best network
            visualizer.DrawNetwork(bestNetwork, 200, 10, Color.cyan, Color.blue, Color.red, new Color(1, 1, 1, 0.3f));
        }
    }

    private void DestroyOldFormulas ()
    {
        foreach (GameObject formula in formulas)
        {
            Destroy(formula);
        }
    }
}
