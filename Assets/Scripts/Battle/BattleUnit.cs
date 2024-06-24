using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BattleUnit : MonoBehaviour
{
    //[SerializeField] private PokemonBase _base;
    //[SerializeField] private int level;
    [SerializeField] private bool isPLayerUnit;
    [SerializeField] private BattleHUD hud;
    private Pokemon pokemon;

    public Pokemon Pokemon { get => pokemon; set => pokemon = value; }
    public bool IsPLayerUnit { get => isPLayerUnit; set => isPLayerUnit = value; }
    public BattleHUD Hud { get => hud; set => hud = value; }

    private Image image;
    private Vector3 originalPosition;
    private Color originalColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPosition = image.transform.localPosition;
        originalColor = image.color;
    }

    public void Setup(Pokemon pkmn) // ten en cuenta que esto ha cambiado #CAP 17
    {
        pokemon = pkmn;
        if (IsPLayerUnit)
            image.sprite = pokemon.Base.BackSprite;
        else
            image.sprite = pokemon.Base.FrontSprite;

        hud.SetData(pokemon);

        image.color = originalColor;
        PlayEnterAnimation();
    }

    public void PlayEnterAnimation() { 
        if(IsPLayerUnit)
            image.transform.localPosition = new Vector3(-500f,originalPosition.y);
        else 
            image.transform.localPosition = new Vector3(500f,originalPosition.y);

        image.transform.DOLocalMoveX(originalPosition.x, 1f);
    }

    public void PlayAttackAnimation() {

        var sequence = DOTween.Sequence();
        if (IsPLayerUnit)
            sequence.Append(image.transform.DOLocalMoveX(originalPosition.x + 50f, 0.25f));
        else
            sequence.Append(image.transform.DOLocalMoveX(originalPosition.x - 50f, 0.25f));

        sequence.Append(image.transform.DOLocalMoveX(originalPosition.x,0.25f));
    }

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray,0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayFaintAnimation() {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPosition.y-150f,0.5f));
        sequence.Join(image.DOFade(0f,0.5f));
    }
}
