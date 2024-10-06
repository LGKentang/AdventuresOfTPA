using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected GameObject gameObj { get; set; }
    protected Rigidbody rb { get; private set; }
    protected CapsuleCollider cc { get; private set; }
    
    public abstract void SetGameObjectAttributes();

    public void GatherComponent(Rigidbody rb, CapsuleCollider cc) {
        this.rb = gameObj.AddComponent<Rigidbody>();
        this.rb.mass = rb.mass;
        this.rb.drag = rb.drag;
        this.rb.angularDrag = rb.angularDrag;

        this.cc = gameObj.AddComponent<CapsuleCollider>();
        this.cc.center = cc.center;
        this.cc.height = cc.height;
        this.cc.radius = cc.radius;
    }

}
