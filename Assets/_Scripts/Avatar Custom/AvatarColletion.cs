using UnityEngine;

namespace Game
{
    public class AvatarColletion : MonoBehaviour
    {
        [Header("head")]
        [SerializeField] private SpriteRenderer _hookRender;
        [SerializeField] private Sprite _defaultHook;

        [Header("body")]
        [SerializeField] private SpriteRenderer _torsoRender;
        [SerializeField] private SpriteRenderer _pelvisRender;
        [SerializeField] private Sprite _defaultTorso;
        [SerializeField] private Sprite _defaultPelvis;

        [Header("arms")]
        [SerializeField] private SpriteRenderer _rightShoulderRender;
        [SerializeField] private SpriteRenderer _leftShoulderRender;
        [SerializeField] private Sprite _defaultRShoulder;
        [SerializeField] private Sprite _defaultLShoulder;

        [Header("legs")]
        [SerializeField] private SpriteRenderer _rightBootRender;
        [SerializeField] private SpriteRenderer _leftBootderRender;

        [Header("Weapon")]
        [SerializeField] private SpriteRenderer _lWeaponRender;
        [SerializeField] private SpriteRenderer _rWeaponRender;

        private SOGameItem _helmetEquiped;
        private SOGameItem _armorEquiped;
        private SOGameItem _weaponEquiped;

        public void SetHelmet(SOGameItem item)
        {
            _hookRender.sprite = item.Icon(1);
            _helmetEquiped = item;
        }

        public void SetWeapon(SOGameItem item)
        {
            _lWeaponRender.sprite = item.Icon(1);
            _rWeaponRender.sprite = item.Icon(1);
            _weaponEquiped = item;
        }

        public void SetShoulder(SOGameItem item)
        {
            
        }

        public void SetGlove(SOGameItem item)
        {
            
        }

        public void SetBoots(SOGameItem item)
        {

        }

        public void SetBodyArmor(SOGameItem item)
        {
            _torsoRender.sprite = item.Icon(1);
            _pelvisRender.sprite = item.Icon(2);

            _armorEquiped = item;
        }


    }
}
