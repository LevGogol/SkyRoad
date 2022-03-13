using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.GetComponent<Player>().Life++;

            var soundManager = FindObjectOfType<SoundManager>();//TODO refactor
            soundManager.PlayClip(Clip.Heart);
            
            gameObject.SetActive(false);
        }
    }
}