using System.Collections;
using UnityEngine;

namespace Complete {
	public class TankShooting: MonoBehaviour {
		public int m_PlayerNumber = 1;              // Used to identify the different players.
		public Rigidbody m_Shell;                   // Prefab of the shell.
		public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
		public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
		public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
		public static float m_MinLaunchForce = 15f; // The force given to the shell if the fire button is not held.
		public static float m_MaxLaunchForce = 30f; // The force given to the shell if the fire button is held for the max charge time.

		public float waitTime = 1;

		public bool shooting = false;

		public IEnumerator Fire(float speed) {
			this.shooting = true;

			yield return new WaitForSeconds(this.waitTime);

			// Create an instance of the shell and store a reference to it's rigidbody.
			Rigidbody shellInstance =
				Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

			// Set the shell's velocity to the launch force in the fire position's forward direction.
			shellInstance.velocity = speed * m_FireTransform.forward;

			// Change the clip to the firing clip and play it.
			m_ShootingAudio.clip = m_FireClip;
			m_ShootingAudio.Play();

			this.shooting = false;
		}
	}
}