using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkEditor : MonoBehaviour
{
    private NeuralNetwork nn;
    public NetworkVisualizer nv;
    public Slider sliderInputs;
    public Slider sliderHiddenLayers;
    public Slider sliderHiddenNeurons;
    public Slider sliderOutputs;
    private int inputs = 1;
    private int hiddenLayers = 1;
    private int hiddenNeurons = 1;
    private int outputs = 1;

    void Start()
    {
        Draw();
    }

    public void ChangeInputs ()
    {
        inputs = (int)sliderInputs.value;
        Draw();
    }

    public void ChangeHiddenLayers ()
    {
        hiddenLayers = (int)sliderHiddenLayers.value;
        Draw();
    }

    public void ChangeHiddenNeurons ()
    {
        hiddenNeurons = (int)sliderHiddenNeurons.value;
        Draw();
    }

    public void ChangeOutputs ()
    {
        outputs = (int)sliderOutputs.value;
        Draw();
    }

    void Draw ()
    {
        nn = new NeuralNetwork(inputs, hiddenLayers, hiddenNeurons, outputs);
        nv.DrawNetwork(nn, 500, 3, Color.red, Color.green, Color.cyan, Color.white);
    }
}
