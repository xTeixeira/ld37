using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public enum WeaponType {Melee, Ranged};

	public string weaponName;
	public float weaponDamage;
	public WeaponType weaponType;
	public float cooldown;
	public float range;

	HitInfo hitInfo;

	public HitInfo GetHitInfo(){
		return new HitInfo(weaponDamage);
	}
}
