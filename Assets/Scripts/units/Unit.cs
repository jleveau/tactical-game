using UnityEngine;
using System;
using System.IO;

public class Unit : MonoBehaviour
{
    public static int DIRECTION_LEFT = 0;
    public static int DIRECTION_RIGHT = 1;
    public static int DIRECTION_TOP = 2;
    public static int DIRECTION_BOTTOM = 3;


    [NonSerialized]
    public Vector3Int tile_position;

    public Profile profile;
    public string profile_json;

    //0 : right, 1 bottom, 2 down, 3 left
    public int direction;

    public Profile Profile
    {
        get
        {
            return profile;
        }
    }


    void Start()
    {
        this.direction = DIRECTION_LEFT;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void turnTowardDirection(Vector3 pos)
    {
        if (this.transform.position.x > pos.x)
        {
            this.updateDirection(DIRECTION_LEFT);
        }
        else if (this.transform.position.x < pos.x)
        {
            this.updateDirection(DIRECTION_RIGHT);
        }
        else if (this.transform.position.y > pos.y)
        {
            this.updateDirection(DIRECTION_TOP);
        }
        else if (this.transform.position.y < pos.y)
        {
            this.updateDirection(DIRECTION_BOTTOM);
        }
    }

    public void updateDirection(int new_direction)
    {
        Vector3 new_orientation = this.transform.localScale;
        if (this.direction == DIRECTION_LEFT && new_direction == DIRECTION_RIGHT
            || this.direction == DIRECTION_RIGHT && new_direction == DIRECTION_LEFT)
        {
            new_orientation = new Vector3(this.transform.localScale.x * -1,
                                                    this.transform.localScale.y,
                                                    this.transform.localScale.z);
        }
        if (this.direction == DIRECTION_TOP && new_direction == DIRECTION_BOTTOM
            || this.direction == DIRECTION_BOTTOM && new_direction == DIRECTION_TOP)
        {
            new_orientation = new Vector3(this.transform.localScale.x,
                                                    this.transform.localScale.y * -1,
                                                    this.transform.localScale.z);
        }
        this.transform.localScale = new_orientation;
        this.direction = new_direction;
    }

    public void new_turn_update()
    {
        profile.UpdateTurnChange();
    }

    public void inflictDamage(int damages, Unit target)
    {
        target.receiveDamage(damages);
    }

    public void receiveDamage(int damage)
    {
        profile.health_points.value -= damage;
    }

}
