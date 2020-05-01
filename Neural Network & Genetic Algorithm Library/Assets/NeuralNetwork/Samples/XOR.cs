using UnityEngine;
using System.Collections;

/*
Using Neural Network to Solve Exclusive OR
[0, 0] -> 0
[0, 1] -> 1
[1, 0] -> 1
[1, 1] -> 0
 */
public class XOR : MonoBehaviour
{
    // new neural network (2 inputs, 4 hidden neurons, 1 output)
    NeuralNetwork nn = new NeuralNetwork(2, 1, 3, 1);
    // add a gameobject with NetworkVisualizer and Sprite renderer
    public NetworkVisualizer visualizer;

    void Start()
    {
        // setting learning rate
        nn.learningRate = 0.5f;
        // starting training
        Train();
        // visualizing xor on the graph
        VisualizeXOR();
        // visualizing network structure
        visualizer.DrawNetwork(nn, 400, 5, Color.cyan, Color.red, Color.blue, new Color(1, 1, 1, 0.3f));
    }

    // supervised learning
    void Train ()
    {
        // training 5k epochs
        float[] inputs = new float[2];
        float[] targets = new float[1];
        for (int i = 0; i < 5000; i++)
        {
            // randomizing data
            switch (Random.Range(0, 4))
            {
                case 0:
                    inputs[0] = 0;
                    inputs[1] = 0;
                    targets[0] = 0;
                    break;
                case 1:
                    inputs[0] = 0;
                    inputs[1] = 1;
                    targets[0] = 1;
                    break;
                case 2:
                    inputs[0] = 1;
                    inputs[1] = 0;
                    targets[0] = 1;
                    break;
                default:
                    inputs[0] = 1;
                    inputs[1] = 1;
                    targets[0] = 0;
                    break;
            }
            nn.TrainNetwork(inputs, targets);
        }
        // printing network predictions after training
        Debug.Log("[0, 0] -> " + nn.FeedForward(new float[] { 0, 0 })[0]);
        Debug.Log("[0, 1] -> " + nn.FeedForward(new float[] { 0, 1 })[0]);
        Debug.Log("[1, 0] -> " + nn.FeedForward(new float[] { 1, 0 })[0]);
        Debug.Log("[1, 1] -> " + nn.FeedForward(new float[] { 1, 1 })[0]);
    }

    void VisualizeXOR ()
    {
        Texture2D texture = new Texture2D(1080, 1080);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 1080, 1080), Vector2.zero);
        GetComponent<SpriteRenderer>().sprite = sprite;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                float[] inputs = { (float)x / texture.width, (float)y / texture.height };
                float pixelValue = nn.FeedForward(inputs)[0];
                Color pixelColour;
                pixelColour = new Color(pixelValue, pixelValue, pixelValue, 1);
                texture.SetPixel(x, y, pixelColour);
            }
        }
        texture.Apply();
    }
}

