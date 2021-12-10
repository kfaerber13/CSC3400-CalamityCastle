using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatController : MonoBehaviour
{
    public TextElement attackStat;
    public TextElement healthStat;
    public TextElement dodgeStat;
    public TextElement speedStat;

    private int attack = 2;
    private int health = 2;
    private int dodge = 0;
    private int speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        attackStat.text = attack.ToString();
        healthStat.text = health.ToString();
        dodgeStat.text = dodge.ToString();
        speedStat.text = speed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getAttack()
    {
        return attack;
    }

    public int getHealth()
    {
        return health;
    }

    public int getDodge()
    {
        return dodge;
    }

    public int getSpeed()
    {
        return speed;
    }
}
