using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{

    public PlayerControls my_player;

    public float power_start;
    public float power_current;
    public float power_fraction;

    public Image using_image;

    public Image[] power_images;

    public bool power_active;

    public void PowerBegin(int power_used, float power_duration)
    {
        power_start = power_duration;
    }

    // Start is called before the first frame update
    void Start()
    {
        power_images[1].enabled = false;
        power_images[2].enabled = false;
        power_images[3].enabled = false;
        power_images[4].enabled = false;
        power_images[5].enabled = false;
        power_images[6].enabled = false;
        power_images[7].enabled = false;
        power_images[8].enabled = false;
        // power_images[9].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        power_active = (my_player.ability_state == 2);

        if (!power_active)
        {
            power_fraction = 1f;
        } else
        {
            power_fraction = power_current / power_start;
        }

        power_current = my_player.ability_time;

        using_image = power_images[my_player.current_ability];
        power_images[0].enabled = false;
        power_images[1].enabled = false;
        power_images[2].enabled = false;
        power_images[3].enabled = false;
        power_images[4].enabled = false;
        power_images[5].enabled = false;
        power_images[6].enabled = false;
        power_images[7].enabled = false;
        power_images[8].enabled = false;
        // power_images[9].enabled = false;

        if (my_player.current_ability == 0)
        {
            using_image.enabled = false;
        } else
        {
            using_image.enabled = true;
        }

        using_image.fillAmount = power_fraction;

    }
}
