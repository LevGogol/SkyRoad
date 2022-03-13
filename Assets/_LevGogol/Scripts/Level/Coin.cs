using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            MoneyStorage.Count += ScoreStorage.Count / 10;

            var soundManager = FindObjectOfType<SoundManager>();
            soundManager.PlayClip(Clip.Coin, 0.5f); //todo refactor

            var screens = FindObjectOfType<Screens>();
            screens.Get<LevelScreen>().PlayCoinAnimation(transform.position);

            gameObject.SetActive(false);
        }
    }
}