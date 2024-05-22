using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Game
{
    public class AvatarColletion : MonoBehaviour
    {
        [Header("head")]
        [SerializeField] private SpriteRenderer _hookRender;

        [Header("body")]
        [SerializeField] private SpriteRenderer _torsoRender;
        [SerializeField] private SpriteRenderer _pelvisRender;

        [Header("arms")]
        [SerializeField] private SpriteRenderer _rightShoulderRender;
        [SerializeField] private SpriteRenderer _leftShoulderRender;

        [Header("legs")]
        [SerializeField] private SpriteRenderer _rightBootRender;
        [SerializeField] private SpriteRenderer _leftBootderRender;

        [Header("Weapon")]
        [SerializeField] private SpriteRenderer _weaponRender;

        public void SetHook(Sprite value)
        {
            _hookRender.sprite = value;
        }

        public void SetWeapon(Sprite value)
        {
            _weaponRender.sprite = value;
        }

        public void SetShoulder(Sprite left, Sprite rigth)
        {
            _leftShoulderRender.sprite = left;
            _rightShoulderRender.sprite = rigth;
        }

        public void SetGlove(Sprite rhand, Sprite lhand, Sprite rwrist, Sprite lwrist)
        {
            
        }

        public void SetBoots(Sprite rboot, Sprite lboot)
        {

        }

        public void SetBodyArmor(Sprite torso, Sprite pelvis)
        {
            _torsoRender.sprite = torso;
            _pelvisRender.sprite = pelvis;
        }
    }
}
