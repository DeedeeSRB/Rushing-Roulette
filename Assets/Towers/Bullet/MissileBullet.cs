using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBullet : Bullet 
{
    
   
   //set radius
    public float explosionRadius = 100f; 

    
    
    public override void HitTarget() {
        
        Explode();
        Destroy(gameObject);
        
    }

    
    void Explode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.name);
            if (collider.tag == "Enemy")
            {
                Debug.Log("no");
                Damage(collider.transform);
            
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


}
