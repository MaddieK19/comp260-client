import static org.lwjgl.glfw.GLFW.*; 
import static org.lwjgl.opengl.GL11.*;
import static org.lwjgl.system.MemoryUtil.*;
import java.nio.ByteBuffer;


class Game 
{
	Grid gameMap;
	public boolean running = true;
	
	private static long window;
	
	private static int width = 1200, height = 800;
    
	public static void initDisplay(){
    	if(!glfwInit()){  //should be GL_TRUE
			// Throw an error.
			System.err.println("GLFW initialization failed!");
		}
    	glfwWindowHint(GLFW_RESIZABLE, GL_TRUE);
    	window = glfwCreateWindow(width, height, "COMP260: Client", NULL, NULL);
    	
    	if(window == NULL){
			// Throw an Error
			System.err.println("Could not create our Window!");
		}
    	
    }
	
    /**
     * Main method to start the game.
     */
    public static void main(String[] args)
    {
    	initDisplay();
    	new Game().play();
    }
    
    /**
     * Create the game and initialise its internal map.
     */
    public Game() 
    {
    	gameMap = new Grid();
    }
    
    public void update(){
    	glfwPollEvents();
    }

    public void render(){
    	glfwSwapBuffers(window);
	}

    /**
     *  Main play routine.  Loops until end of play.
     */
    public void play() 
    {            
    	while (running)
    	{
    		update();
        	render();
        	
        	if(glfwWindowShouldClose(window) == true){
				running = false;
			}
        }
    }
    	
    
    
    

        

}