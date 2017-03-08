
public class Cell {
	
	public enum CellStates {
		TOWER, PLAYER, OTHER_PLAYER,EMPTY;
	}
	
	CellStates cellState;
	
	public Cell(CellStates cellState){
		this.cellState = cellState;
	}
	
	public void changeState(String cellContents){
		if (cellContents == "player"){
			cellState = CellStates.PLAYER;
		}
		else if (cellContents == "tower"){
			cellState = CellStates.TOWER;
		}
		else if (cellContents == "otherplayer"){
			cellState = CellStates.OTHER_PLAYER;
		}
		else
			cellState = CellStates.EMPTY;
	}

}
