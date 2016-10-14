public class API {

	static API instance;

	API() {
	}

	public static API Instance {
		get {
			if(instance == null) {
				instance = new API();
			}
			return instance;
		}
	}

	public string indexURL = "http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/index.php";

	public string commonURL = "http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/common.php";

}