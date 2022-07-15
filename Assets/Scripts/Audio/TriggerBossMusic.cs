using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossMusic : MonoBehaviour
{
    public musicBoos music;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            switch (music)
            {
                case (musicBoos.through_space):
                    if (AudioManager.Instance.LastMusicFile != "through space")
                    {
                        GameFlowController.Instance.StartMusicGame();
                    }
                    break;
                case (musicBoos.laser_figh):
                    if (AudioManager.Instance.LastMusicFile != "laser_fight")
                    {
                        GameFlowController.Instance.StartMusicBoosLasers();
                    }
                    break;
                case (musicBoos.bullet_hell):
                    if (AudioManager.Instance.LastMusicFile != "bullet_hell")
                    {
                        GameFlowController.Instance.StartMusicBoosBulletHell();
                    }
                    break;
                case (musicBoos.nekai_theme):
                    if (AudioManager.Instance.LastMusicFile != "nekai_theme")
                    {
                        GameFlowController.Instance.StartMusicBoosNekAi();
                    }
                    break;
            }
        }
    }
}
public enum musicBoos
{
    through_space,
    laser_figh,
    bullet_hell,
    nekai_theme
}