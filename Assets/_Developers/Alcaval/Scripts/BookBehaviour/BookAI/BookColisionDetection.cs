using System.Collections;
using System.Collections.Generic;
using TFMEsada;
using UnityEngine;

public class BookColisionDetection : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private BookBehaviourTree bbt;

    private bool _recolocate = false;
    private bool _flapIdle = false;

    float x = 0f;
    float y = 0f;

    private Rigidbody _rb;

    private void Awake() {
        _player = GameObject.Find("Player");
        _rb = gameObject.GetComponent<Rigidbody>();
        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
    }


    private void Update() {
        Vector3 targetPosition = Vector3.forward;

        if(moverse && GameStateManager.instance.CurrentGameState != GameState.Paused)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_player.transform.position.x, 4f, _player.transform.position.z), 1f * Time.deltaTime); 

        if(_flapIdle)
        {
            if(GameStateManager.instance.CurrentGameState != GameState.Paused) transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 2, transform.position.z), 2 * Time.deltaTime);
            this.transform.LookAt(targetPosition);
        }
        else if(_player != null)
        {
            if(_recolocate && GameStateManager.instance.CurrentGameState != GameState.Paused)
            {
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_player.transform.position.x, 3f, _player.transform.position.z), 1.5f * Time.deltaTime);
            }

            if(_player != null){
                targetPosition = new Vector3( _player.transform.position.x, this.transform.position.y, _player.transform.position.z);
                this.transform.LookAt(targetPosition);
            } 
        }
        else
        {
            _flapIdle = true;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        //CHOCO CONTRA EL PLAYER ESTANDO EN ATAQUE -> MUERTE DEL PLAYER
        if(other.transform.tag == "Player" && bbt.taskAttack.inAttack && !bbt.taskAttack.playerDead)
        {
            AkSoundEngine.PostEvent("libro_chocando", gameObject);
            other.gameObject.GetComponent<ControlManager>().PlayerDeath();
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.GetComponent<Rigidbody>().useGravity = false;

            bbt.gameObject.GetComponent<Animator>().SetBool("flap", true);

            _flapIdle = true;
            bbt.taskAttack.inAttack = false;
            bbt.taskAttack.playerDead = true;
        }

        //CHOCO CONTRA OTRA COSA
        if(other.transform.tag == "Ground" && bbt.taskAttack.inAttack)
        {
            AkSoundEngine.PostEvent("libro_chocando", gameObject);
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bbt.gameObject.GetComponent<Animator>().SetBool("flap", true);
            StartCoroutine(cooldownCoroutine()); 
            _recolocate = true;
            x = transform.position.x;
            y = transform.position.y;
            bbt.taskAttack.inAttack = false;
        }
    }

    public void death()
    {
        gameObject.transform.parent.gameObject.GetComponent<EnemyAudioController>().bookStop();
        AkSoundEngine.PostEvent("libro_defeated", gameObject);
        gameObject.GetComponent<Animator>().SetBool("dead", true);
        gameObject.GetComponent<Animator>().SetBool("flap", false);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * 5, ForceMode.Impulse);
        gameObject.GetComponent<BookBehaviourTree>().enabled = false;
        this.enabled = false;
    }

    IEnumerator cooldownCoroutine()
    {
        if(!_flapIdle) transform.GetComponent<Rigidbody>().useGravity = false;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

        float timer = 0f;
        while(timer < 4f) 
        {
            if(pausedCoroutines)
                yield return null;
            else
                timer += Time.deltaTime;
            yield return null;
        }
        //yield return new WaitForSeconds(4f);

        if(!_flapIdle)
        {
            bbt.taskAttack.setAttack(true);
            bbt.gameObject.GetComponent<Animator>().SetBool("flap", false);
            _recolocate = false;
            bbt.checkInAttackRange.ready = true;
        }
    }


    public void PreAttack()
    {
        StartCoroutine(preAttack());
    }

    bool moverse = false;

    bool pausedCoroutines = false;

    IEnumerator preAttack()
    {
        transform.GetComponent<Rigidbody>().useGravity = false;
        bbt.gameObject.GetComponent<Animator>().SetBool("flap", true);
        
        moverse = true;
        //while(!pauseCoroutines) yield return null;


        float timer = 0f;
        while(timer < 2f) 
        {
            if(pausedCoroutines)
                yield return null;
            else
                timer += Time.deltaTime;
            yield return null;
        }

        //yield return new WaitForSeconds(2f);
        bbt.checkInAttackRange.ready = true;
        moverse = false;
    }

    public void playAwake()
    {
        
    }

    Vector3 _pausedVelocity;
    Vector3 _pausedAngularVelocity; 

    public void pauseBook()
    {
        gameObject.transform.parent.gameObject.GetComponent<EnemyAudioController>().bookStop();
        gameObject.GetComponent<Animator>().speed = 0;
        pausedCoroutines = true;
        bbt.taskAttack.playerDead = true;
        _pausedVelocity = _rb.velocity;
        _pausedAngularVelocity = _rb.angularVelocity;
        _rb.isKinematic = true;
    }

    public void resumeBook()
    {
        print("libros" + gameObject.transform.parent.gameObject.GetComponent<EnemyAudioController>().numOfBooksAwake);
        // if(gameObject.transform.parent.gameObject.GetComponent<EnemyAudioController>().numOfBooksAwake < 0) 
        //     gameObject.transform.parent.gameObject.GetComponent<EnemyAudioController>().numOfBooksAwake = 0;
        gameObject.transform.parent.gameObject.GetComponent<EnemyAudioController>().bookAwake();
        gameObject.GetComponent<Animator>().speed = 1;
        pausedCoroutines = false;
        bbt.taskAttack.playerDead = false;
        _rb.velocity = _pausedVelocity;
        _rb.angularVelocity = _pausedAngularVelocity;
        _rb.isKinematic = false;
    }

    private void onGameStateChanged(GameState newGameState)
    {
        switch (GameStateManager.instance.CurrentGameState)
        {
            case GameState.Gameplay:
                resumeBook();
                break;
            case GameState.Paused:
                pauseBook();
                break;
            default:
                break;
        }
    }
}
