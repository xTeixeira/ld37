using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public enum WeaponType {Melee, Ranged};

	public string weaponName;
	public float weaponDamage;
	public WeaponType weaponType;
	public float cooldown;
	public float meleeRange;
	public GameObject projectile;
	public float projectileVelocity;
	public bool ready;

	string ownerTag;
	HitInfo hitInfo;

	public HitInfo GetHitInfo() {
		return new HitInfo(weaponDamage, gameObject.transform.parent.tag);
	}

	public bool Attack(Vector3 attackDirection) {
		if (ready) {
			StartCoroutine (AttackCooldown ());
			if (weaponType == WeaponType.Ranged)
				CreateProjectile (attackDirection);
			return true;
		}
		return false;
	}

	void CreateProjectile(Vector3 direction){
		Instantiate (projectile, transform.position, transform.rotation).
		GetComponent<Projectile>().InitProjectile(direction, projectileVelocity, GetHitInfo());
	}

	IEnumerator AttackCooldown(){
		ready = false;
		yield return new WaitForSeconds(cooldown);
		ready = true;
	}
}
