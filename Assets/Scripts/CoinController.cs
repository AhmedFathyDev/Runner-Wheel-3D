using UnityEngine;

public class CoinController : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(50 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
            ++PlayerManager.NumberOfCoins;
            Destroy(gameObject);
        }
    }
}
