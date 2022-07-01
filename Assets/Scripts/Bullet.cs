using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform firePoint;
	[SerializeField] GameObject player;
	[SerializeField] GameObject player1;
	//public int damage = 40;
	//public GameObject impactEffect;
	//public LineRenderer lineRenderer;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			StartCoroutine(Shoot());
		}
	}

	IEnumerator Shoot ()
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
		if (hitInfo)
		{
			if(hitInfo.transform.name == "PlayerPhysics2D"){
            	Debug.Log(hitInfo.transform.name);
				player.SetActive(false);
				player1.SetActive(false);
				
			}
			// Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
			// if (enemy != null)
			// {
			// 	enemy.TakeDamage(damage);
			// }

			// Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

			// lineRenderer.SetPosition(0, firePoint.position);
			// lineRenderer.SetPosition(1, hitInfo.point);
		} 
        else
		// {
		// 	lineRenderer.SetPosition(0, firePoint.position);
		// 	lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
		// }

		// lineRenderer.enabled = true;

		yield return 0;

		//lineRenderer.enabled = false;
	}
}
