﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentShield;
    [HideInInspector] public float currentShockAmmo;
    [HideInInspector] public float currentBlastAmmo;
    [HideInInspector] public float currentPlasmaAmmo;
    [HideInInspector] public float currentSprint;

    private float maxHealth;
    private float maxShield;
    private float maxShockAmmo;
    private float maxBlastAmmo;
    private float maxPlasmaAmmo;
    private float maxSprint;

    [SerializeField]private PlayerStats player;

    [SerializeField] private SciptableIntObj shockAmmo;
    [SerializeField] private SciptableIntObj blastAmmo;
    [SerializeField] private SciptableIntObj plasmaAmmo;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;
    [SerializeField] private Image shockAmmoBar;
    [SerializeField] private Image blastAmmoBar;
    [SerializeField] private Image plasmaAmmoBar;
    [SerializeField] private Image sprintBar;

    // Start is called before the first frame update
    private void Start()
    {

        maxHealth = player.startingHealth;
        maxShield = player.maxShield;
        maxShockAmmo = shockAmmo.startValue;
        maxBlastAmmo = blastAmmo.startValue;
        maxPlasmaAmmo = plasmaAmmo.startValue;
        maxSprint = player.maxSprint;

    }

    // Update is called once per frame
    private void Update()
    {
        currentHealth = player.health;
        currentShield = player.shield;
        currentShockAmmo = shockAmmo.value;
        currentBlastAmmo = blastAmmo.value;
        currentPlasmaAmmo = plasmaAmmo.value;
        currentSprint = player.sprint;

        healthBar.fillAmount = currentHealth / maxHealth;
        shieldBar.fillAmount = currentShield / maxShield;
        shockAmmoBar.fillAmount = currentShockAmmo / maxShockAmmo;
        blastAmmoBar.fillAmount = currentBlastAmmo / maxBlastAmmo;
        plasmaAmmoBar.fillAmount = currentPlasmaAmmo / maxPlasmaAmmo;
        sprintBar.fillAmount = currentSprint / maxSprint;
    }
}
