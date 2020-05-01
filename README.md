<h1>Simple Unity Neural Network Library</h1>
<p>Very simple multilayer neural network with backpropagation and genetic algorithm.</p>

<h2>Features</h2>
<ul>
    <li>Multilayer networks.</li>
    <li>Backptopagation method.</li>
    <li>Methods for genetic algorithms.</li>
    <li>Network visualizer.</li>
    <li>Simple matrix class.</li>
    <li>Useful static math methods.</li>
    <li>Three example scenes.</li>
</ul>

<h2>How to use</h2>
<p>If you don't care about the example scenes, then copy the NetworkScripts folder to your Assets folder.</p>
<p>This line will generate a neural network with two inputs, one hidden layer (three neurons in hidden layers) and one output.</p>
<pre lang="csharp">
NeuralNetwork nn = new NeuralNetwork(2, 1, 3, 1);
</pre>
<p>You can train your network like this:</p>
<pre lang="csharp">
// Training method from XOR example

// supervised learning
void Train()
{
&nbsp;&nbsp;&nbsp;&nbsp;// training 5k epochs
&nbsp;&nbsp;&nbsp;&nbsp;float[] inputs = new float[2];
&nbsp;&nbsp;&nbsp;&nbsp;float[] targets = new float[1];
&nbsp;&nbsp;&nbsp;&nbsp;for (int i = 0; i < 5000; i++)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;// randomizing data
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;switch (Random.Range(0, 4))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;case 0:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;inputs[0] = 0;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;inputs[1] = 0;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;targets[0] = 0;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;break;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;case 1:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;inputs[0] = 0;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;inputs[1] = 1;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;targets[0] = 1;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;break;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;case 2:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;inputs[0] = 1;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;inputs[1] = 0;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;targets[0] = 1;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;break;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;default:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;inputs[0] = 1;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;inputs[1] = 1;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;targets[0] = 0;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;break;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nn.TrainNetwork(inputs, targets);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;// printing network predictions after training
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Log("[0, 0] -> " + nn.FeedForward(new float[] { 0, 0 })[0]);
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Log("[0, 1] -> " + nn.FeedForward(new float[] { 0, 1 })[0]);
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Log("[1, 0] -> " + nn.FeedForward(new float[] { 1, 0 })[0]);
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Log("[1, 1] -> " + nn.FeedForward(new float[] { 1, 1 })[0]);
}
</pre>

<p>If you want to visualize your network, you can use:</p>
<pre lang="csharp">
// public void DrawNetwork | Attributes: (NeuralNetwork network, int size, int layerGap, Color neuronColor, Color connectionStrong, Color connectionWeak, Color background)
public NetworkVisualizer visualizer;
visualizer.DrawNetwork(nn, 400, 5, Color.cyan, Color.red, Color.blue, new Color(1, 1, 1, 0.3f));
</pre>

<p>Some useful extra methods:</p>
<pre lang="csharp">
// public static NeuralNetwork Crossover | Attributes: (NeuralNetwork nn1, NeuralNetwork nn2, float mutationPercent)
NeuralNetwork parent1;
NeuralNetwork parent2;
NeuralNetwork child = NeuralNetwork.Crossover(parent1, parent2, 5);
<br>
// public static float GetDistBetweenPoints | Attributes: (float x1, float y1, float x2, float y2)
float distance = StaticMath.GetDistBetweenPoints(hit.point.x, hit.point.y, transform.position.x, transform.position.y);
<br>
// public static float GetAngleBetweenPoints | Attributes: (float x1, float y1, float x2, float y2)
float angle = StaticMath.GetAngleBetweenPoints(hit.point.x, hit.point.y, transform.position.x, transform.position.y);
<br>
// public static float Remap | Attributes: (float value, float from1, float to1, float from2, float to2)
float newValue = StaticMath.Remap(50, 0, 100, 0, 1); // newValue = 0.5f
</pre>

<h2>Screenshots</h2>
<img src="screenshots/or.png" alt="XOR">
<img src="screenshots/f1.png" alt="Formula">