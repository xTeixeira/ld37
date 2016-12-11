[System.Serializable]
public class HitInfo {
	public float damage;
	public string ownerTag;

	public HitInfo(float damage, string ownerTag){
		this.damage = damage;
		this.ownerTag = ownerTag;
	}
}
