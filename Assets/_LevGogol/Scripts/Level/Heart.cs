using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.GetComponent<Player>().TakeLife();

            var audio = FindObjectOfType<Audio>();//TODO refactor
            audio.PlayClipOneShot(TrackName.Heart);
            
            gameObject.SetActive(false);
        }
    }
}