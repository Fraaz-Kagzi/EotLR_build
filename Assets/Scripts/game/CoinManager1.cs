using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public static int playerCoins = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    public static void addCoins(int amount){
      playerCoins += amount;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
