using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{
    int hp = 3;

    [SerializeField]
    GameObject[] shipSprites;

    [SerializeField]
    GameObject destroyedExplosion;

    [SerializeField]
    Slider hpBar;

    private void LateUpdate()
    {
        hpBar.transform.parent.rotation = Quaternion.identity;
    }

    public void TakeDamage()
    {
        hp--;

        switch (hp)
        {
            case 2:
                shipSprites[0].SetActive(false);
                shipSprites[1].SetActive(true);
                hpBar.value = hp;
                break;

            case 1:
                shipSprites[1].SetActive(false);
                shipSprites[2].SetActive(true);
                hpBar.value = hp;
                break;

            case 0:
                shipSprites[2].SetActive(false);
                shipSprites[3].SetActive(true);
                hpBar.gameObject.SetActive(false);
                StartCoroutine("Sink");
                break;

            default:
                break;
        }
    }

    IEnumerator Sink()
    {
        Instantiate(destroyedExplosion, transform.position, transform.rotation);

        if(gameObject.TryGetComponent<PlayerScript>(out PlayerScript ps))
        {
            HudController.HC.GameEnd();
            ps.Sink();
        }
        else if (gameObject.TryGetComponent<EnemyChaser>(out EnemyChaser ec))
        {
            HudController.HC.AddScore(5);
            ec.Sink();
        }
        else if (gameObject.TryGetComponent<EnemyShooter>(out EnemyShooter es))
        {
            HudController.HC.AddScore(3);
            es.Sink();
        }

        SpriteRenderer rend = shipSprites[3].GetComponent<SpriteRenderer>();
        float factor = 1f;
        while (factor >= 0)
        {
            factor -= Time.deltaTime / 5;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, factor);
            yield return null;
        }

        Destroy(gameObject);

        yield return null;
    }
}
