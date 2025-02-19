using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> players;
    public Slider powerBar;

    public GameObject currentPlayer;
    public Material lava;
    public Material diffuse;

    public int currentIndex;

    public HingeJoint door;
    public GameObject interactButton;
    public GameObject shapeShiftButton;

    [SerializeField] GameObject failed;
    [SerializeField] CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = players[0];
        
    }
    private void Update()
    {

        SwitchPlayers();
    }

    void SwitchPlayers()
    {
        if (players.Count > 1 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            BasicMovement currentMovement = players[currentIndex].GetComponent<BasicMovement>();
            currentMovement.enabled = false;

            // Move to the next player in the list
            currentIndex = (currentIndex + 1) % players.Count;

            // Enable the new current player's movement script
            BasicMovement newMovement = players[currentIndex].GetComponent<BasicMovement>();
            newMovement.enabled = true;
            cam.Follow = newMovement.gameObject.transform;
            
        }
        
    }
   public IEnumerator ChangeCamera()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("call");
        BasicMovement currentMovement = players[currentIndex].GetComponent<BasicMovement>();
        currentMovement.gameObject.SetActive(false);

        // Move to the next player in the list
        currentIndex = (currentIndex + 1) % players.Count;

        // Enable the new current player's movement script
        BasicMovement newMovement = players[currentIndex].GetComponent<BasicMovement>();
        newMovement.enabled = true;
        cam.Follow = newMovement.gameObject.transform;
    }

    public IEnumerator ReloadScene()
    {
        failed.SetActive(true);
        BasicMovement currentMovement = players[currentIndex].GetComponent<BasicMovement>();
        currentMovement.enabled = false;
        yield return new WaitForSeconds(3);
       
        Scene currentscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentscene.buildIndex);
    }
    
    
}

