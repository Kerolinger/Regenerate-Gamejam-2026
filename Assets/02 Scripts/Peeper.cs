using UnityEngine;

[CreateAssetMenu(fileName = "Peeper", menuName = "Scriptable Objects/Peeper")]
public class Peeper : ScriptableObject
{
    [Header("Basic Information")]
    [SerializeField] private string name;
    [SerializeField] private Sprite defaultSprite;
    [Space]
    [SerializeField] private Ingredients ingredient = Ingredients.none;
    [SerializeField] private Ingredients[] ingredientDislikes;
    [SerializeField] private Skills skillOffer = Skills.none;
    [SerializeField] private Skills skillNeeded = Skills.none;

    [Header("Narrative Bits - Stage 01")]
    [SerializeField] [TextArea (2,2)] private string st01_ingredientText;
    [SerializeField][TextArea(2, 2)] private string st01_skillOfferText;
    [SerializeField][TextArea(2, 2)] private string st01_skillNeedText;

    [Header("Narrative Bits - Stage 03")]
    [SerializeField][TextArea(2, 2)] private string st03_soupResultText;
    [SerializeField][TextArea(2, 2)] private string st03_skillOfferText;
    [SerializeField][TextArea(2, 2)] private string st03_skillNeedText;

    public enum Ingredients { none, ingredient01, ingredient02, ingredient03, ingredient04, ingredient05, ingredient06};
    public enum Skills { none, skill01, skill02, skill03, skill04, skill05, skill06};
}
