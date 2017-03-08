
public class Player {
	private int health = 100;
	private int x = 0,y = 0;
	
	
	// Getters
	public int getHealth(){ 
		return health; 
	}
	public int getX(){ 
		return x; 
	}
	public int getY(){ 
		return y; 
	}
	
	//Setters
	public void setHealth(int newHealth){
		health = newHealth;
	}
	
	public void setX(int newX){
		x = newX;
	}
	
	public void setY(int newY){
		y = newY;
	}
}
