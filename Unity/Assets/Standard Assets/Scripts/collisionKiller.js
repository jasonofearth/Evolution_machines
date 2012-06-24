function OnCollisionEnter (collision : Collision) {
	if (Time.fixedTime <= 1) {
		if(collision.gameObject.name != "Plane") {
			Destroy (gameObject);
		}
	}
}