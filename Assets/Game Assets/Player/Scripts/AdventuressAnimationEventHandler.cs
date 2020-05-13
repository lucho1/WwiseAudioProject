////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using System.Collections;

public class AdventuressAnimationEventHandler : MonoBehaviour
{

    public AudioClip FootStepOne;
    public AudioClip FootStepTwo;

    [Header("Pickup Sounds")]
    public AudioClip[] pickupsounds;

    [Header("Wwise")]
    public AK.Wwise.Event Swing = new AK.Wwise.Event();
    public AK.Wwise.Event GetItem = new AK.Wwise.Event();
    public AK.Wwise.Trigger GetItemStinger = new AK.Wwise.Trigger();

    [Header("Object Links")]
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private GameObject runParticles;

    private PlayerFoot foot_L;
    private PlayerFoot foot_R;

    AudioSource audioSource;
    private int colMaterial= 0;
    #region private variables
    private bool hasPausedMovement;
    private readonly int canShootMagicHash = Animator.StringToHash("CanShootMagic");
    private readonly int isAttackingHash = Animator.StringToHash("IsAttacking");
    #endregion


    [Header("Weapon Sounds")]
    public AudioClip[] daggersounds;
    public AudioClip[] swordsounds;
    public AudioClip[] hammersounds;
    public AudioClip[] pickaxesounds;
    public AudioClip[] axesounds;
    public AudioClip[] firstattack;
    public AudioClip[] secondattack;
    public AudioClip[] thirdattack;


    private void Awake()
    {
        GameObject L = GameObject.Find("toe_left");
        GameObject R = GameObject.Find("toe_right");
        if (L != null)
        {
            foot_L = L.GetComponent<PlayerFoot>();
        }
        else {
            print("Left foot missing");
        }
        if (R != null)
        {
            foot_R = R.GetComponent<PlayerFoot>();
        }
        else
        {
            print("Right foot missing");
        }
        audioSource = GetComponent<AudioSource>();

    }


    void enableWeaponCollider()
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.equippedWeaponInfo != null)
        {
            PlayerManager.Instance.equippedWeaponInfo.EnableHitbox();
        }
    }

    void disableWeaponCollider()
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.equippedWeaponInfo != null)
        {
            PlayerManager.Instance.equippedWeaponInfo.DisableHitbox();
        }

    }

    void ScreenShake()
    {
        PlayerManager.Instance.cameraScript.CamShake(new PlayerCamera.CameraShake(0.4f, 0.7f));
    }

    bool onCooldown = false;
    public enum FootSide { left, right };
    public void TakeFootstep(FootSide side)
    {
        if (foot_L != null && foot_R != null) {
            if (!PlayerManager.Instance.inAir && !onCooldown)
            {
                Vector3 particlePosition;
                if (side == FootSide.left)
                {
                    {
                        particlePosition = foot_L.transform.position;
                        FootstepParticles(particlePosition);
                        audioSource.PlayOneShot(FootStepOne, 0.7F);
                    }
                }
                else
                {
                  
                    {

                        particlePosition = foot_R.transform.position;
                        FootstepParticles(particlePosition);
                        audioSource.PlayOneShot(FootStepTwo, 0.7F);
                    }
                }
            }
        }
    }

    void FootstepParticles(Vector3 particlePosition) {
        GameObject p = Instantiate(runParticles, particlePosition + Vector3.up * 0.1f, Quaternion.identity) as GameObject;
        p.transform.parent = SceneStructure.Instance.TemporaryInstantiations.transform;
        Destroy(p, 5f);
        StartCoroutine(FootstepCooldown());
    }

    IEnumerator FootstepCooldown()
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(0.2f);
        onCooldown = false;
    }

    void ReadyToShootMagic()
    {
        PlayerManager.Instance.playerAnimator.SetBool(canShootMagicHash, true);
    }

    public enum AttackState { NotAttacking, Attacking };
    void SetIsAttacking(AttackState state)
    {
        if (state == AttackState.NotAttacking)
        {
            playerAnimator.SetBool(isAttackingHash, false);
        }
        else
        {
            playerAnimator.SetBool(isAttackingHash, true);
        }
    }

    public void Weapon_SwingEvent()
    {
        // PLAY SOUND
        Weapon W = PlayerManager.Instance.equippedWeaponInfo;
        W.WeaponTypeSwitch.SetValue(PlayerManager.Instance.weaponSlot);
        Swing.Post(PlayerManager.Instance.weaponSlot);

        AnimatorStateInfo currentAnimation = PlayerManager.Instance.playerAnimator.GetCurrentAnimatorStateInfo(0);
        if (currentAnimation.IsName("Player_RightSwing"))
        {
            audioSource.PlayOneShot(firstattack[Random.Range(0, 3)], 0.75f);
        }
        else if (currentAnimation.IsName("Player_LeftSwing"))
        {
            audioSource.PlayOneShot(secondattack[Random.Range(0, 3)], 0.75f);
        }
        else if (currentAnimation.IsName("Player_TopSwing"))
        {
            audioSource.PlayOneShot(thirdattack[Random.Range(0, 3)], 0.75f);

        }
    }

    public void PauseMovement()
    {
        if (!hasPausedMovement)
        {
            hasPausedMovement = true;
            PlayerManager.Instance.motor.SlowMovement();
        }
    }

    public void ResumeMovement()
    {
        if (hasPausedMovement)
        {
            hasPausedMovement = false;
            PlayerManager.Instance.motor.UnslowMovement();
        }
    }

    public void FreezeMotor()
    {
        StartCoroutine(PickupEvent());
    }

    private IEnumerator PickupEvent()
    {
        PlayerManager.Instance.PauseMovement(gameObject);
        yield return new WaitForSeconds(2f);
        PlayerManager.Instance.ResumeMovement(gameObject);
    }

    public void PickUpItem()
    {
        PlayerManager.Instance.PickUpEvent();
        GetItem.Post(this.gameObject);
        GetItemStinger.Post(GameManager.Instance.MusicGameObject);
    }

    public void PickUpItem(int itemtype)
    {
        PlayerManager.Instance.PickUpEvent();
        audioSource.PlayOneShot(pickupsounds[itemtype]);
    }

    public void WeaponSound()
    {
        Weapon EquippedWeapon = PlayerManager.Instance.equippedWeaponInfo;
        EquippedWeapon.WeaponImpact.Post(EquippedWeapon.transform.parent.gameObject);

    }

    public void WeaponSound(int material)
    {
        Weapon EquippedWeapon = PlayerManager.Instance.equippedWeaponInfo;

        if (EquippedWeapon.weaponType == WeaponTypes.Dagger)
        {
            if (daggersounds[material] != null)
                audioSource.PlayOneShot(daggersounds[material], 0.7F);
        }
        if (EquippedWeapon.weaponType == WeaponTypes.Sword)
        {
            if (swordsounds[material] != null)
                audioSource.PlayOneShot(swordsounds[material], 0.7F);
        }
        if (EquippedWeapon.weaponType == WeaponTypes.Axe)
        {
            if (axesounds[material] != null)
                audioSource.PlayOneShot(axesounds[material], 0.7F);
        }
        if (EquippedWeapon.weaponType == WeaponTypes.Hammer)
        {
            if (hammersounds[material] != null)
                audioSource.PlayOneShot(hammersounds[material], 0.7F);
        }
        if (EquippedWeapon.weaponType == WeaponTypes.PickAxe)
        {
            if (pickaxesounds[material] != null)
                audioSource.PlayOneShot(pickaxesounds[material], 0.7F);
        }
        
    }
}
