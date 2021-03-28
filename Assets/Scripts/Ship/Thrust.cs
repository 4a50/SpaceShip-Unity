using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour
{
  ConstantForce constForce;
  Rigidbody rb;
  [SerializeField] bool stablizedThrustDisabled = false;
  [SerializeField] bool engageLiftOff = false;
  [SerializeField] float liftOffAltitude = 6f;
  [SerializeField] float liftOffMagnitude = .2f;
  [SerializeField] private float stableForce = -0.01f;  

  private float grav = Mathf.Abs(Physics.gravity.y);
  



    void Start()
    {
    constForce = GetComponent<ConstantForce>();
    rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (engageLiftOff) { InitialLiftOff(); }
    }
  public void ThrustAdjustment(string direction, float increment = 0)
  { 
      Vector3 addThrust = new Vector3();
      switch (direction.ToLower())
      {

        case "y":
          addThrust.y += increment;
          break;
      }
      constForce.force += addThrust;    
  }
  public void StabilizeThust()
  {
    if (!stablizedThrustDisabled)
    {
      Debug.Log($"Gravity {Physics.gravity}");
      Vector3 velocity = rb.velocity;
      constForce.force = new Vector3(0, grav, 0);
      if (rb.velocity.y > 0)
      {
        Debug.Log("Positive Stabilized Force Initiated");
        rb.AddForce(new Vector3(0, stableForce, 0));
      }
      else if (rb.velocity.y < 0)
      {
        Debug.Log("Negative Stabilized Force Initiated");
        rb.AddForce(new Vector3(0, (-1 * stableForce), 0));
      }
      else { rb.AddForce(0, 0, 0); }
    }
  }
    public void InitialLiftOff()
    {
    Vector3 curPosit = this.gameObject.transform.position;
    Debug.Log($"curPosition: {curPosit}");
    if (curPosit.y < liftOffAltitude)
    {
      stablizedThrustDisabled = true;
      constForce.force = new Vector3(0, liftOffMagnitude + 9.8f, 0);
    }
    else
    {
      engageLiftOff = false;
      stablizedThrustDisabled = false;
    }
    }
}
