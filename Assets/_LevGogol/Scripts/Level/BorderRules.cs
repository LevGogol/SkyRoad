using System.Collections;

public class BorderRules
{
    private float _spawnBorderFromPlayer = -14;
    private float _destroyBorderFromPlayer = 5;

    public IEnumerator BoardCoroutine(Player player, CloudSpawner cloudSpawner)
    {
        while (true)
        {
            if (player.transform.position.y + _spawnBorderFromPlayer < cloudSpawner.DownOffset)
            {
                cloudSpawner.CreateLine();
            }

            if (player.transform.position.y + _destroyBorderFromPlayer < cloudSpawner.UpOffset)
            {
                cloudSpawner.RemoveLine();
            }

            if (player.transform.position.x < -25 || player.transform.position.x > 25)
            {
                player.ExtraDie();
                yield break;
            }

            yield return null;
        }
    }
}