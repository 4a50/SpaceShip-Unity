using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoveController : MonoBehaviour
{
  // Start is called before the first frame update
  
  public float YAxis;
  public float XAxis;
  public bool SpaceKey;
  public bool Ckey;
  [SerializeField] Thrust thrust;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    CheckMove();
    }
  void CheckMove()
  {
    YAxis = Input.GetAxis("Horizontal");
    XAxis = Input.GetAxis("Vertical");
    SpaceKey = Input.GetKey(KeyCode.Space);
    Ckey = Input.GetKey(KeyCode.C);
    if (SpaceKey)
    {
      thrust.ThrustAdjustment("y", .1f);
    }
    else if (Ckey)
    {
      thrust.ThrustAdjustment("y", -.1f);
    }
    else { thrust.StabilizeThust(); }
  }
}
