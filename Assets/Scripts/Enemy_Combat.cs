using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Player"))
       {
            collision.gameObject.GetComponent<Player_Health>().change_Health(-1);
       }
   }
}
