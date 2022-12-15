using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;

[RequireComponent(typeof(ObiSolver))]
public class CupDetector : MonoBehaviour
{
    public int water = 0;
    public int milk = 0;
    public int coffee = 0;

    private HashSet<int> waterParticles = new HashSet<int>();
    private HashSet<int> milkParticles = new HashSet<int>();
    private HashSet<int> coffeeParticles = new HashSet<int>();

    public ObiSolver solver;
    public ObiEmitter[] emitters;
    private ObiCollider2D targetCollider;
    //private HashSet<int> particles = new HashSet<int>();

    private void Awake()
    {
        this.solver = GetComponent<ObiSolver>();
        this.emitters = GetComponentsInChildren<ObiEmitter>();
        this.targetCollider = GetComponent<ObiCollider2D>();
    }

    private void OnEnable()
    {
        this.solver.OnCollision += OnCollision;
    }

    private void OnDisable()
    {
        this.solver.OnCollision -= OnCollision;
    }

    private void OnCollision(object sender, ObiSolver.ObiCollisionEventArgs e)
    {
        for (int i = 0; i < e.contacts.Count; ++i)
        {
            if (e.contacts[i].distance < 0.001f)
            {

                var collider = ObiColliderWorld.GetInstance().colliderHandles[e.contacts[i].bodyB].owner;

                if (collider == targetCollider)
                {
                    var index = this.solver.simplices[e.contacts[i].bodyA];

                    var diffusion = this.solver.diffusion[index];

                    switch (diffusion)
                    {
                        case 1f: // Water
                            this.waterParticles.Add(e.contacts[i].bodyA);
                            break;
                        case 0.01f: // Milk
                            this.milkParticles.Add(e.contacts[i].bodyA);
                            break;
                        case 0.03f: // Coffee
                            this.coffeeParticles.Add(e.contacts[i].bodyA);
                            break;
                    }

                    //currentParticles.Add(e.contacts[i].bodyA);
                }
            }
        }

        //particles = currentParticles;

        this.water = this.waterParticles.Count;
        this.milk = this.milkParticles.Count;
        this.coffee = this.coffeeParticles.Count;
    }

    public void Reset()
    {
        foreach (var emitter in emitters)
        {
            emitter.KillAll();
        }

        this.waterParticles.Clear();
        this.milkParticles.Clear();
        this.coffeeParticles.Clear();

        this.water = 0;
        this.milk = 0;
        this.coffee = 0;
    }
}
