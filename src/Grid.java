
public class Grid {
	
	private Cell[][] cells = new Cell[100][100];

	public Grid() {
		for (int x = 0; x < cells.length; x++) {
			for (int y = 0; y < cells[0].length; y++) {
				cells[x][y] = new Cell(Cell.CellStates.EMPTY);
			}
			//System.out.println(x);
		}
	}

	

	protected void setCell(int x, int y, String content) {
		getCells()[x][y].changeState(content);
	}

	protected Cell[][] getCells() {
		return cells;
	}

}
