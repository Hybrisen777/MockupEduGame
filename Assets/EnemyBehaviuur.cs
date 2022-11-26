using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviuur : MonoBehaviour
{
    //SO reference
    public SO_Enemy enemyScriptableObject;

    //Updated value of enemy
    public int currentNumber;

    //Display number
    public TextMeshPro displayedNumber;

    //Has the battle been won by the player?
    public bool isEnemyDead = false;

    private void Awake()
    {
        displayedNumber = transform.GetChild(0).GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set to starting number once encounter starts
        currentNumber = enemyScriptableObject.startingNumber;

        //Set the sprite to scriptable object sprite
        gameObject.GetComponent<MeshRenderer>().material = enemyScriptableObject.enemySpriteMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        //Set display to what is the actual value
        displayedNumber.SetText(currentNumber.ToString());

        //For testing
        if (Input.GetKeyDown(KeyCode.L)) {
            changeValueByCard(2f, Operation.multiplyByNumber);
        }

        //Victory
        enemyDeath();
    }


    public void enemyDeath()
    {
        if (currentNumber == enemyScriptableObject.goalNumber)
        {
            isEnemyDead = true;
        }
    }

    public void changeValueByCard(float amount, Operation operand)
    {
        //Dodawanie/odejmowanie
        if (operand == Operation.addNumber)
        {
            currentNumber =   Mathf.RoundToInt(currentNumber + amount);
        }

        //Mno�enie/dzielenie
        if (operand == Operation.multiplyByNumber)
        {
            currentNumber = Mathf.RoundToInt(currentNumber * amount);
        }

        //Pot�gowanie/pierwiastkowanie
        if (operand == Operation.raiseToPowerOfNumber)
        {
            currentNumber = Mathf.RoundToInt(Mathf.Pow(currentNumber, amount));
        }

        //Tu doda si� jak�� logik� z d�wi�kami, animacj� (np. jaki� screenshake?)
        // numberDisplay.getcomponent<anim>().blablablabl
    }

    // FUNKCJE ZACHOWA�
    // Zgodnie z sugesti� na spotkaniu k�ka, przeciwnicy powinni mie� kilka zachowa�
    // Np. podzielenie si� przez n, dodanie do siebie n, pomno�enie si� przez -1
    // ale najbardziej podstawowym (i najprostszym do zaprogramowania) zachowaniem jest atak

    public void enemyActionAttack()
    {
        ManagerSingleton.Instance.playerCurrentHealth -= enemyScriptableObject.damage;
    }

    public void halfWayThere()
    {
        currentNumber = Mathf.RoundToInt(enemyScriptableObject.goalNumber / 2);
    }
}
