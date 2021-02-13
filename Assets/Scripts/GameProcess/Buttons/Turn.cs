using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{   
    public bool isClickable { get; set; }
    
    private string ButtonText{ 
        get => this.GetComponentInChildren<Text>().text;
        set => this.GetComponentInChildren<Text>().text = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialise(bool isEnemyTurn){
        this.isClickable = !isEnemyTurn;  
        this.SetButtonText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public static void TaskOnClick(Turn button) {
    //     button.isClickable = !button.isClickable; 
    //     Debug.Log("Is button clicable:" + button.isClickable);
    //     button.SetButtonText();
    // }

    public void SetButtonText(){
        string text = this.isClickable? TurnText.PASS_TURN:TurnText.ENEMY_TURN; 
        this.ButtonText = text;

    }
}
