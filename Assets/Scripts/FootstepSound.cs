using StarterAssets;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] floorFootstepSounds;
    public AudioClip[] tableFootstepSounds;

    public float minSpeedForFootstepSounds = 1f;
    public float maxSpeedForFootstepSounds = 10f;
    public float minPitch = 1.5f;
    public float maxPitch = 3f;

    private AudioSource audioSource;
    private CharacterController characterController;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController.isGrounded && characterController.velocity.magnitude > minSpeedForFootstepSounds)
        {
            float speedPercent = Mathf.Clamp01((characterController.velocity.magnitude - minSpeedForFootstepSounds) / (maxSpeedForFootstepSounds - minSpeedForFootstepSounds));
            float pitch = Mathf.Lerp(minPitch, maxPitch, speedPercent);
            audioSource.pitch = pitch;

            PlayFootstepSound();
        }
    }

    private void PlayFootstepSound()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        RaycastHit hit;
        Physics.Raycast(transform.position+Vector3.up, Vector3.down, out hit);

        if (hit.collider != null)
        {
            string tag = hit.collider.gameObject.tag;

            switch (tag)
            {
                case "Floor":
                    audioSource.clip = floorFootstepSounds[Random.Range(0, floorFootstepSounds.Length)];
                    break;
                case "Table":
                    audioSource.clip = tableFootstepSounds[Random.Range(0, tableFootstepSounds.Length)];
                    break;                
            }

            audioSource.Play();
        }
    }
}
