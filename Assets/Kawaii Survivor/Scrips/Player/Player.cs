using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerHealth), typeof(PlayerLevel))]
public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Components")]
    [SerializeField] private CircleCollider2D collider;
    [SerializeField] private SpriteRenderer playerRenderer;
    private PlayerHealth playerHealth;
    private PlayerLevel playerLevel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        playerHealth = GetComponent<PlayerHealth>();
        playerLevel = GetComponent<PlayerLevel>();

        CharacterSelectionManager.onCharacterSelected += CharacterSelectionCallback;
    }

    private void CharacterSelectionCallback(CharacterDataSO characterData)
    {
        playerRenderer.sprite = characterData.Sprite;
    }

    private void OnDestroy()
    {
        CharacterSelectionManager.onCharacterSelected -= CharacterSelectionCallback; 
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }
    public Vector2 GetCenter()
    {
        return (Vector2)transform.position + collider.offset;
    }

    public bool HasLeveledUp()
    {
        return playerLevel.HasLeveledUp();
    }
}
