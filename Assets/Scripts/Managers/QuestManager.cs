////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using System.Collections.Generic;

public delegate void QuestUpdate();
public delegate void QuestBarUpdate();
public delegate void QuestItemUpdate();
public delegate void QuestGet();

public class QuestManager : Singleton<QuestManager>
{
    [Header("Dialogue")]
    public bool FreezePlayerMovement = true;

    public class QuestItemInfo
    {
        public GameObject Item;
        public int CollectAmount;

        public QuestItemInfo(GameObject I, int C)
        {
            Item = I;
            CollectAmount = C;
        }
    }

    protected QuestManager() { }

    public static event QuestUpdate OnQuestUpdate;
    public static event QuestBarUpdate OnQuestBarUpdate;
    public static event QuestItemUpdate OnQuestItemUpdate;
    public static event QuestGet OnQuestGet;
    public static bool QuestCollectionComplete = false;

    public static int AmountOfQuests = 0;
    public static int mainQuestProgress = 0;
    public List<QuestItemInfo> QuestItems;
    public static bool hasActiveMainQuest = false;
    public static bool playerHasSeenMainQuest = false;
    public AK.Wwise.RTPC QuestProgressRTPC = new AK.Wwise.RTPC();
    public static bool DialogueReady = false;

    public static string NameOfCurrentQuest;

    [HideInInspector]
    public string QuestBarInformation = "";
    [HideInInspector]
    public string QuestBarInformationKey = "";

    public bool showQuestBarInfo = false;

    #region quest audios
    [Header("Quest audio clips")]
    public AudioClip quest_completeCollection_01;
    public AudioClip quest_completeCollection_02;
    public AudioClip quest_questRoll_open;
    public AudioClip quest_questRoll_close;
    private AudioSource audiosource;
    #endregion

    void Awake()
    {
        QuestItems = new List<QuestItemInfo>();

        audiosource = PlayerManager.Instance.player.GetComponent<AudioSource>();
    }

    public static void PushQuestBarUpdate()
    {
        if (OnQuestBarUpdate != null)
        {
            OnQuestBarUpdate();
        }
        Instance.UpdateRTPC();
    }

    public static void PushQuestUpdate()
    {
        if (OnQuestUpdate != null && DialogueReady)
        {
            OnQuestUpdate();
        }
    }

    public static void PushQuestGet()
    {
        if (OnQuestGet != null)
        {
            OnQuestGet();
        }
    }

    public static void UpdateQuestItems()
    {
        if (OnQuestItemUpdate != null)
        {
            OnQuestItemUpdate();
        }
    }

    public void UpdateRTPC()
    {
        float percentage = ((float)mainQuestProgress / (float)AmountOfQuests) * 100f;
        QuestProgressRTPC.SetGlobalValue(percentage);
    }
}
