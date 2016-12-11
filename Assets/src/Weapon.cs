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
	public bool ready;

	HitInfo hitInfo;

	public HitInfo GetHitInfo() {
		return new HitInfo(weaponDamage);
	}

	public bool Attack() {
		if (ready) {
			StartCoroutine (AttackCooldown ());
			return true;
		}
		return false;
	}

	IEnumerator AttackCooldown(){
		ready = false;
		yield return new WaitForSeconds(cooldown);
		ready = true;
	}
}
