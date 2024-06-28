using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Traslada al jugador sin necesidad de cambios con escenas solo en el gameplay
public class LocationPortal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] private DestinationIdentifier destinationPortal;
    [SerializeField] private Transform spawnPoint;

    PlayerController player;
    public void OnPlayerTriggered(PlayerController player)
    {
        player.Character.Animator.IsMoving = false;
        this.player = player;
        StartCoroutine(Teleport());
    }

    Fader fader;
    private void Start()
    {
        fader = FindObjectOfType<Fader>();
    }

    IEnumerator Teleport()
    {
        GameManager.Instance.PauseGame(true);
        yield return fader.FadeIn(0.5f);

        var destPortal = FindObjectsOfType<LocationPortal>().First(x => x != this && x.destinationPortal == this.destinationPortal);
        player.Character.SetPositionAndSnapToTile(destPortal.SpawnPoint.position);

        yield return fader.FadeOut(0.5f);
        GameManager.Instance.PauseGame(false);
    }

    public Transform SpawnPoint => spawnPoint;

    public bool IsTriggerRepeat => false;
}
