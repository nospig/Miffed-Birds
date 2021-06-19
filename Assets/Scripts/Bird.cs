using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdWasLaunched = false;
    private float _timeSittingAround = 0f;
    
    [SerializeField] private float _launchPower = 500f;
    [SerializeField] private float _stillResetTime = 2f;
    [SerializeField] private float _outOfBounds = 20f;
    [SerializeField] private float _maxDragDistance;

    private void Awake()
    {
        _initialPosition = transform.position;
        GetComponent<LineRenderer>().enabled = false;
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);

        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if(Mathf.Abs(transform.position.y) > _outOfBounds || Mathf.Abs(transform.position.x) > _outOfBounds || _timeSittingAround > _stillResetTime)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        if (!_birdWasLaunched)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<LineRenderer>().enabled = true;
        }
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 forceVector = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(forceVector * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        GetComponent<LineRenderer>().enabled = false;
        _birdWasLaunched = true;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newDirectionTo = _initialPosition - newPosition;

        if(newDirectionTo.magnitude > _maxDragDistance)
        {
            newDirectionTo.Normalize();
            newDirectionTo = newDirectionTo * _maxDragDistance;
            newPosition = new Vector3(_initialPosition.x - newDirectionTo.x, _initialPosition.y - newDirectionTo.y);
        }

        if (newDirectionTo.x >= 0f)
        {
            transform.position = new Vector3(newPosition.x, newPosition.y);
        }
    }
}
