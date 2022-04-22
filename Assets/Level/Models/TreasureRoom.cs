using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRoom : MonoBehaviour
{
	SpriteRenderer spriteRenderer;
	private void Start()
	{
		 spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name.Contains("Player"))
		{
			 this.spriteRenderer.enabled = false;
		}
	}


	private void OnTriggerExit2D(Collider2D collision)
	{
		this.spriteRenderer.enabled = true;
	}
   
}
