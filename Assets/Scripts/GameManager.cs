using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Drink
{
    None,
    Espresso,
    Americano,
    Latte,
}

public enum Status
{
    Idle,
    Working,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject mainVirtualCamera;
    public GameObject customer;

    public Transform aPoint;
    public Transform bPoint;

    public int score = 0;
    public float time = 60;
    public Status status = Status.Idle;
    public CupDetector cup;

    private CustomerController currentCustomer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        CreateCustomer();
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
        }
    }

    private void CreateCustomer()
    {
        currentCustomer = Instantiate(customer, aPoint.position, aPoint.rotation).GetComponent<CustomerController>();
        currentCustomer.target = bPoint;

        switch (Random.Range(1, 4))
        {
            case 1:
                currentCustomer.drink = Drink.Espresso;
                break;
            case 2:
                currentCustomer.drink = Drink.Americano;
                break;
            case 3:
                currentCustomer.drink = Drink.Latte;
                break;
        }
    }

    public void Accept()
    {
        if (time <= 0)
        {
            return;
        }

        if (status != Status.Idle)
        {
            return;
        }

        status = Status.Working;
        mainVirtualCamera.SetActive(false);
        currentCustomer.status = CustomerStatus.Waiting;
    }

    public void Reset()
    {
        status = Status.Idle;
        mainVirtualCamera.SetActive(true);

        cup.Reset();

        Destroy(currentCustomer.gameObject);

        CreateCustomer();
    }

    public void Submit()
    {
        if (time <= 0)
        {
            return;
        }

        if (status != Status.Working)
        {
            return;
        }

        var drink = CheckDrink();

        if (drink == Drink.None)
        {
            score -= 10;
        }
        else if (drink == currentCustomer.drink)
        {
            score += 20;
        }

        Reset();
    }

    private Drink CheckDrink()
    {
        if (CheckRecipe(0, 0, 600))
        {
            return Drink.Espresso;
        }
        else if (CheckRecipe(600, 0, 600))
        {
            return Drink.Americano;
        }
        else if (CheckRecipe(0, 600, 600))
        {
            return Drink.Latte;
        }

        return Drink.None;
    }

    private bool CheckRecipe(int water, int milk, int coffee)
    {
        if (CheckTargetRange(cup.water, water) && CheckTargetRange(cup.milk, milk) && CheckTargetRange(cup.coffee, coffee))
        {
            return true;
        }

        return false;
    }

    private bool CheckTargetRange(int value, int target, int range = 100)
    {
        if ((target == 0 && value == 0) || (target - range <= value && value <= target + range))
        {
            return true;
        }

        return false;
    }
}
