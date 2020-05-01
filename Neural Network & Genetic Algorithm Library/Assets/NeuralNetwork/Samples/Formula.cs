using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Formula : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    public float rotationSpeed = 10f;
    public float driftFactor = 0.75f;
    private GameObject lastCheckpoint;
    // Genetic Algorithm variables
    public bool alive = true;
    public float fitness = 0;
    public NeuralNetwork brain = new NeuralNetwork(5, 2, 3, 3);

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate ()
    {
        if (!alive) {
            gameObject.SetActive(false);
        } else {
            if (fitness < 10)
            {
                fitness += Time.deltaTime;
            }
            Think();
        }
        rb.velocity = ForwardVelocity() + SideVelocity() * driftFactor;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "OffTrackCollider") {
            alive = false;
        }
    }

    private Vector2 ForwardVelocity () {
        return transform.right * Vector2.Dot(rb.velocity, transform.right);
    }

    private Vector2 SideVelocity () {
        return transform.up * Vector2.Dot(rb.velocity, transform.up);
    }

    private void Think ()
    {
        // casting rays forward, left and right
        RaycastHit2D[] hits1 = Physics2D.RaycastAll(transform.position, transform.right);
        RaycastHit2D[] hits2 = Physics2D.RaycastAll(transform.position, transform.up);
        RaycastHit2D[] hits3 = Physics2D.RaycastAll(transform.position, -transform.up);
        RaycastHit2D[] hits4 = Physics2D.RaycastAll(transform.position, transform.up + transform.right);
        RaycastHit2D[] hits5 = Physics2D.RaycastAll(transform.position, -transform.up + transform.right);
        // calculating distances from formula to track edges
        float[] inputs = { GetEyeValue(hits1), GetEyeValue(hits2), GetEyeValue(hits3), GetEyeValue(hits4), GetEyeValue(hits5) };
        // feeding inputs and getting outputs from neural network
        float[] outputs = brain.FeedForward(inputs);
        // deciding what to do
        if (outputs[0] > outputs[1] && outputs[0] > outputs[2])
        {
            Accelerate();
        } else if (outputs[1] > outputs[0] && outputs[1] > outputs[2])
        {
            Accelerate();
            TurnRight();
        } else
        {
            Accelerate();
            TurnLeft();
        }
    }

    private float GetEyeValue (RaycastHit2D[] hits) {
        float distance = 0;
        foreach (RaycastHit2D hit in hits)
        {    
            if (hit.collider != null) {
                if (hit.collider.gameObject.tag == "OffTrackCollider") {
                    distance = StaticMath.GetDistBetweenPoints(hit.point.x, hit.point.y, transform.position.x, transform.position.y);
                    return distance;
                }
            }
        }
        return distance;
    }

    public void TurnLeft () {
        rb.angularVelocity = (1 * rotationSpeed);
    }

    public void TurnRight () {
        rb.angularVelocity = (-1 * rotationSpeed);
    }

    public void Accelerate () {
        rb.AddForce(transform.right * speed);
        rb.angularVelocity = 0;
    }

    public void Stop () {
        rb.angularVelocity = 0;
    }
}
