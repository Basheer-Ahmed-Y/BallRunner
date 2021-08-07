using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BallScript : MonoBehaviour
{
    //Declarations
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool isInGround;
    [SerializeField] private int score;
    [SerializeField] private bool isFinished;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject panel;


    void FixedUpdate() 
    {
        if (!isFinished)
        {
            rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed,
                                        rb.velocity.y, Input.GetAxis("Vertical") * speed);
            if (Input.GetButton("Jump") && isInGround)
            {
                rb.AddForce(Vector3.up * jumpSpeed * 5);
                isInGround = false;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isInGround = true;
        }

        if (collision.collider.tag == "Finish")
        {
            panel.SetActive(true);
            scoreText.text = "Score : " + score.ToString();
            isFinished = true;

        }
        if (collision.collider.tag == "Respawn")
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            score += 10;
            GameObject coin = other.gameObject;
            Destroy(coin);
        }
    }


}
