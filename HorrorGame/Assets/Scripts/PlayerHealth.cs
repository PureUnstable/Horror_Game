using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int max_health = 100;
	public int current_health;
	public Slider health_slider;
	public Image damage;
	public AudioClip se_death;
	public float flash_speed = 5f;
	public Color flash_color = new Color(1f,0f,0f,0.1f);

	Animator anim;
	AudioSource player_audio;
	bool is_dead;
	bool hurt;

	void Awake()
	{
		
		anim = GetComponent <Animator> ();
		current_health = max_health;
	}

	void Update()
	{
		if (hurt) {
			damage.color = flash_color;
		} else {
			damage.color = Color.Lerp (damage.color, Color.clear, flash_speed *Time.deltaTime);
		}

		hurt = false;

	}

	public void TakeDamage(int amount)
	{
		hurt = true;
		current_health -= amount;
		health_slider.value = current_health;
		player_audio.Play ();

		if (current_health <= 0 && !is_dead) {
			Death ();
		}
	}

	public void Death()
	{
		is_dead = true;
		anim.SetTrigger ("Die");
		player_audio.clip = se_death;
		player_audio.Play ();

	}

}